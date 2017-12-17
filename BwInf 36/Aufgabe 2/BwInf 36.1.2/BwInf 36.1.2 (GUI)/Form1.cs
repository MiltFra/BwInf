using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BwInf_36._1._2__GUI_
{
    // DISCLAIMER: This commetary of the code at hand might not be serious enough or well stuctured enough
    // if you are looking for that you might want to contact me at vrelda@outlook.de or look into the documenation
    public partial class Form1 : Form
    {
        Offer[] BadOffers = new Offer[4] { new Offer(new List<string>(), -1), new Offer(new List<string>(), -1), new Offer(new List<string>(), -1), new Offer(new List<string>(), -1) };

        // that's where the magic begins, with one string in special format
        private void calculate(string input)
        {
            // we need to get all the data from the string
            (List<int> ages, double[] differenceNeeded, int couponCount) data = getData(input);
            // plain ages aren't any good, so we gotta count how many are in which age group
            (int children, int adults, int smallChildren) ageDistribution = getAgeDistribution(data.ages);
            // here is the actual calculation
            Offer[] minimalPrices = getOffers(data.couponCount, ageDistribution.adults, ageDistribution.children, ageDistribution.smallChildren);
            // here we gotta compare the four dates with their biases
            int best = 0;
            for (int i = 0; i < 4; i++)
            {
                if (minimalPrices[i].Price + Convert.ToDouble(data.differenceNeeded[i]) < minimalPrices[best].Price + Convert.ToDouble(data.differenceNeeded[best]))
                {
                    // new best
                    best = i;
                }
            }
            // return the best... done.
            returnResult(minimalPrices[best], best);
        }

        // here we split the string up... nothing special
        private (List<int> ages, double[] differenceNeeded, int couponCount) getData(string input)
        {
            string[] args = input.Split(' ');

            List<List<string>> splitArgs = new List<List<string>>();
            foreach (string s in args)
            {
                string[] splitS = s.Split(',');
                List<string> splitArg = new List<string>();
                foreach (string split in splitS)
                {
                    splitArg.Add(split);
                }
                splitArgs.Add(splitArg);
            }
            double[] differenceNeeded = new double[4];
            for (int i = 0; i < 4; i++)
            {
                differenceNeeded[i] = Convert.ToDouble(args[i]);
            }

            int couponCount = Convert.ToInt32(args[4]);

            List<int> ages = new List<int>();
            foreach (string s in splitArgs[5])
            {
                ages.Add(Convert.ToInt32(s));
            }
            return (ages, differenceNeeded, couponCount);
        }
        // here we count how many of each age there are... nothing special either
        private (int children, int adults, int smallChildren) getAgeDistribution(List<int> ages)
        {
            int children = 0;
            int adults = 0;
            int smallChildren = 0;
            foreach (int age in ages)
            {
                if (age > 16) { adults++; }
                else if (age < 4) { smallChildren++; }
                else { children++; }
            }
            return (children, adults, smallChildren);
        }
        // getting solutions for every date type
        private Offer[] getOffers(int couponCount, int adults, int children, int smallChildren)
        {
            // insufficient company
            if (adults == 0 && smallChildren > 0) { return BadOffers; }
            Offer[] minimalPrices = new Offer[4];
            // count of all the persons
            int ageCount = children + adults + smallChildren;
            // cycling throught the 4 age types
            for (int i = 0; i < 4; i++)
            {
                // basic is without coupons
                Offer currentPrice = getPrice(smallChildren, children, adults, i);
                // only during school times coupons are allowed
                if (i < 2)
                {
                    // if there are at least as many coupons as persons, we can just spend them all
                    if (couponCount >= ageCount) { currentPrice = OnlyCoupons(ageCount); }
                    else
                    {
                        // now we have to cycle through the coupon combinations... 
                        //every single one has to be checked, it's not even guranteed that using all of them will give an advantage
                        for (int childrenCoupons = 0; childrenCoupons <= couponCount && childrenCoupons <= children; childrenCoupons++)
                        {
                            for (int adultCoupons = 0; adultCoupons <= couponCount - childrenCoupons && adultCoupons <= adults; adultCoupons++)
                            {
                                // calculation with coupons in mind, we checked already that smallchildren are accompanied by adults, we can ignore them now
                                Offer tempPrice = getPrice(0, children - childrenCoupons, adults - adultCoupons, i);
                                // the single coupons have to be added
                                for (int j = 0; j < childrenCoupons + adultCoupons; j++)
                                {
                                    tempPrice.Tickets.Add("Einzelgutschein");
                                }
                                // if there's at leas one left, take it for the whole group
                                if (childrenCoupons + adultCoupons < couponCount)
                                {
                                    tempPrice.Price *= 0.9;
                                    tempPrice.Tickets.Add("Gruppengutschein");
                                }
                                // if that version is better than all the previous, make it the new best
                                if (tempPrice.Price < currentPrice.Price)
                                {
                                    currentPrice = tempPrice.Clone();
                                    tempPrice = null;
                                }
                            }
                        }
                    }
                }
                // set the current best to the minimal of the current date
                minimalPrices[i] = currentPrice;
                currentPrice = null;
            }
            // return it all
            return minimalPrices;
        }
        private Offer OnlyCoupons(int ageCount)
        {
            List<string> coupons = new List<string>();
            for (int j = 0; j < ageCount; j++)
            {
                coupons.Add("Einzelgutschein");
            }
            return new Offer(coupons, 0);
        }

        // just the return function
        public void returnResult(Offer offer, int day)
        {
            // only non-negative offers are allowed, else there has been a problem, in our case it's the company of smallchildren
            if (offer.Price >= 0)
            {
                lbl_Total.Text = "Gesamt: " + offer.Price.ToString("N2").PadLeft(2);
                switch (day)
                {
                    case 0:
                        lbl_Total.Text += "\n\nSchule, \nWoche".PadLeft(20);
                        break;
                    case 1:
                        lbl_Total.Text += "\n\nSchule, \nWochenende".PadLeft(20);
                        break;
                    case 2:
                        lbl_Total.Text += "\n\nFerien, \nWoche".PadLeft(20);
                        break;
                    case 3:
                        lbl_Total.Text += "\n\nFerien, \nWochenende".PadLeft(20);
                        break;
                }
                lb_output.Items.Clear();
                foreach (string s in offer.Tickets)
                {
                    string toPrint = "";
                    switch (s)
                    {
                        case "Erwachsen":
                            toPrint = "Erwachsen".PadRight(16);
                            break;
                        case "Kind":
                            toPrint = "Kind".PadRight(16);
                            break;
                        case "Familie":
                            toPrint = "Familienkarte".PadRight(16);
                            break;
                        case "Tag":
                            toPrint = "Tageskarte".PadRight(16);
                            break;
                        default:
                            toPrint = s.PadRight(16);
                            break;
                    }
                    lb_output.Items.Add(toPrint);
                }
            }
            else
            {
                lb_output.Items.Clear();
                lbl_Total.Text = "Unzureichende\nBegleitung";
            }
        }

        public static Offer getPrice(List<int> ages, int day) //0: week & school, 1: weekend & school, 2: week & holidays, 3: weekend & holidays
        {
            int children = 0;
            int adults = 0;
            int smallChildren = 0;
            foreach (int age in ages)
            {
                if (age > 16) { adults++; }
                else if (age < 4) { smallChildren++; }
                else { children++; }
            }
            return getPrice(smallChildren, children, adults, day);
        }
        public static Offer getPrice(int smallChildren, int children, int adults, int day)
        {
            // checks wether this offer is possible based on the small children, which aren't considered later on
            if (smallChildren == 0 || adults > 0)
            {
                // minimal prices
                Offer best = OnlySinglePrices(children, adults);
                // up 'til 6 ppl you can get the fixed prices
                if (adults + children <= 6)
                {
                    // if 0 = mod (2, day) then it's week
                    if (day % 2 == 0) { best = FixedPricesDuringWeek(children, adults); }
                    else { best = FixedPricesDuringWeekEnd(children, adults); }
                }
                else
                {
                    // create the first "best" offer
                    Offer contestant = new Offer(new List<string>(), double.MaxValue);
                    // during week
                    if (day % 2 == 0) { contestant = CombinedPricesDuringWeek(children, adults); }
                    // during weekend
                    else { contestant = CombinedPricesDuringWeekEnd(children, adults); }
                    // if the contestant is better than the best, best is the contestant
                    if (best.Price > contestant.Price) { best = contestant; }
                }
                // return best
                return best;
            }
            else { return new Offer(new List<string>(), -1); }
        }
        // adding single prices for all the children and adults... MAGIC :D
        private static Offer OnlySinglePrices(int children, int adults)
        {
            List<string> total = new List<string>();
            for (int i = 0; i < adults; i++)
            {
                total.Add("Erwachsen");
            }
            for (int i = 0; i < children; i++)
            {
                total.Add("Kind");
            }
            return new Offer(total, children * 2.50 + adults * 3.50);
        }

        static List<((int children, int adults) input, Offer output)> calculatedPricesWE = new List<((int children, int adults) input, Offer output)>();
        // you will have to take this function for what it is, it's nothing special really.
        // you can calculate that stuff yourself if you want ^^
        private static Offer FixedPricesDuringWeekEnd(int children, int adults)
        {
            if ((adults >= 2 && children >= 2) || (adults >= 1 && children >= 3))
            {
                if (adults == 4 && children == 2)
                {
                    return new Offer(new List<string>() { "Familie", "Erwachsen", "Erwachsen" }, 15);
                }
                else if (adults == 3 && children == 3)
                {
                    return new Offer(new List<string>() { "Familie", "Erwachsen", "Kind" }, 14);
                }
                else if (adults == 3 && children == 2)
                {
                    return new Offer(new List<string>() { "Familie", "Erwachsen" }, 11.50);
                }
                else if (adults == 2 && children == 4)
                {
                    return new Offer(new List<string>() { "Familie", "Kind", "Kind" }, 13);
                }
                else if (adults == 2 && children == 3)
                {
                    return new Offer(new List<string>() { "Familie", "Kind" }, 10.50);
                }
                else if (adults == 2 && children == 2)
                {
                    return new Offer(new List<string>() { "Familie" }, 8);
                }
                else if (adults == 1 && children == 5)
                {
                    return new Offer(new List<string>() { "Familie", "Kind", "Kind" }, 13);
                }
                else if (adults == 1 && children == 4)
                {
                    return new Offer(new List<string>() { "Familie", "Kind" }, 10.50);
                }
                else if (adults == 1 && children == 3)
                {
                    return new Offer(new List<string>() { "Familie" }, 8);
                }
                else
                {
                    throw new Exception();
                }
            }
            else
            {
                return OnlySinglePrices(children, adults);
            }
        }
        private static Offer CombinedPricesDuringWeekEnd(int children, int adults)
        {
            // if this combination has been calculated already, return that. no need for recalculating
            foreach (var v in calculatedPricesWE)
            {
                if (v.input.adults == adults && v.input.children == children)
                {
                    return v.output.Clone();
                }
            }
            // best is all single prices
            Offer best = OnlySinglePrices(children, adults);
            // only combinations needed, if count is bigger than 6
            if (children + adults > 6)
            {
                // cycling part size, max 1 - 6
                for (int i = 1; i <= 6; i++)
                {
                    // cycling through the possible children in the part
                    for (int j = 0; j <= i && j <= children; j++)
                    {
                        if (adults - (i - j) >= 0)
                        {
                            // part 1 is the small one
                            Offer part1 = FixedPricesDuringWeekEnd(j, i - j);
                            // part 2 is the rest, trying to be solved by recursion
                            Offer part2 = CombinedPricesDuringWeekEnd(children - j, adults - (i - j));

                            // if the combination is cheaper than the best option so far, it becomes the best option
                            if (part1.Price + part2.Price < best.Price)
                            {
                                // need to be combined
                                part1.Tickets.AddRange(part2.Tickets);
                                best = new Offer(part1.Tickets, part1.Price + part2.Price);
                            }
                        }
                    }
                }
            }
            else { best = FixedPricesDuringWeekEnd(children, adults); }
            // before it's returned, the calculation has to be stored, that's how we save time
            calculatedPricesWE.Add(((children, adults), best.Clone()));
            return best;
        }

        static List<((int children, int adults) input, Offer output)> calculatedPricesW = new List<((int children, int adults) input, Offer output)>();
        // you will have to take this function for what it is, it's nothing special really.
        // you can calculate that stuff yourself if you want ^^
        private static Offer FixedPricesDuringWeek(int children, int adults)
        {
            Offer total = OnlySinglePrices(children, adults);
            total.Price *= 0.8;
            if (adults + children == 4)
            {
                if (children == 0 || children == 1)
                {
                    total = new Offer(new List<string>() { "Tag" }, 11);
                }
                else if (children == 2 || children == 3)
                {
                    total = new Offer(new List<string>() { "Familie" }, 8);
                }
                else
                {
                    total = new Offer(new List<string>() { "Kind", "Kind", "Kind", "Kind" }, 8);
                }
            }
            else if (adults + children == 5)
            {
                if (children >= 3 && adults >= 1)
                {
                    total = new Offer(new List<string>() { "Familie", "Kind" }, 10);
                }
                else
                {
                    total = new Offer(new List<string>() { "Tag" }, 11);
                }
            }
            else if (adults + children == 6)
            {
                total = new Offer(new List<string>() { "Tag" }, 11);
            }
            return total;
        }
        private static Offer CombinedPricesDuringWeek(int children, int adults)
        {
            // check if that paritcular combination of children and adults has been calculated yet.
            // if yes, return a COPY (we don't want our previous results to be changed later)
            foreach (var v in calculatedPricesW)
            {
                if (v.input.adults == adults && v.input.children == children)
                {
                    return v.output.Clone();
                }
            }
            // if it hasn't been calculated yet, we need to do it now
            // only single prices is the worst option, but it's an option... so we take this as our starting point
            Offer best = OnlySinglePrices(children, adults);
            // if there are not more than 6 persons, we can take the static values
            if (children + adults > 6)
            {
                // if not, we have to cycle through the total person counts from 1 to 6
                for (int i = 1; i <= 6 && i <= children + adults; i++)
                {
                    // and how many of them are children / adults
                    for (int j = 0; j <= i && j <= children; j++)
                    {
                        // if (for some reason) the count of the persons that aren't children
                        // is bigger than the adult count, it's impossible to calculate...
                        if (adults - (i - j) >= 0)
                        {
                            // now we have to calculate the fixed part with the current combination
                            Offer part1 = FixedPricesDuringWeek(j, i - j);
                            // and the rest will be put into the current method as well
                            Offer part2 = CombinedPricesDuringWeek(children - j, adults - (i - j));
                            // if the combination of the two is better than the previous best, 
                            // we take that one as our new best
                            if (part1.Price + part2.Price < best.Price)
                            {
                                // put the tickets into one single list
                                part1.Tickets.AddRange(part2.Tickets);
                                // and create the offer with those values
                                best = new Offer(part1.Tickets, part1.Price + part2.Price);
                            }
                        }
                    }
                }
            }
            // for person counts of 6 or less, we can take the static values
            else { best = FixedPricesDuringWeek(children, adults); }
            // now that we calculated something, we have to store a copy of it 
            // so that we remember it the next time we have to calculate the same thing
            calculatedPricesW.Add(((children, adults), new Offer(best.Tickets, best.Price)));
            // and we have to return it
            return best;
        }

        public Form1() { InitializeComponent(); }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // just random GUI stuff

        private void addPerson(int age)
        {
            if (age > 16)
            {
                lb_GroupMembers.Items.Add("Erwachsener");
                lbl_AdultCount.Text = (Convert.ToInt32(lbl_AdultCount.Text) + 1).ToString().PadLeft(2);
            }
            else if (age < 4)
            {
                lb_GroupMembers.Items.Add("Kleinkind");
                lbl_SmallChildCount.Text = (Convert.ToInt32(lbl_SmallChildCount.Text) + 1).ToString().PadLeft(2);
            }
            else
            {
                lb_GroupMembers.Items.Add("Kind");
                lbl_ChildCount.Text = (Convert.ToInt32(lbl_ChildCount.Text) + 1).ToString().PadLeft(2);
            }
            bt_undo.Enabled = true;
        }
        private void cb_SchoolWeek_CheckedChanged(object sender, EventArgs e)
        {
            if (tb_SchoolWeek.Enabled)
            {
                tb_SchoolWeek.Clear();
                tb_SchoolWeek.BackColor = Control.DefaultBackColor;
                tb_SchoolWeek.Enabled = false;
            }
            else
            {
                tb_SchoolWeek.Clear();
                tb_SchoolWeek.BackColor = Color.White;
                tb_SchoolWeek.Enabled = true;
            }
        }
        private void cb_SchoolWE_CheckedChanged(object sender, EventArgs e)
        {
            if (tb_SchoolWE.Enabled)
            {
                tb_SchoolWE.Clear();
                tb_SchoolWE.BackColor = SystemColors.Control;
                tb_SchoolWE.Enabled = false;
            }
            else
            {
                tb_SchoolWE.Clear();
                tb_SchoolWE.BackColor = Color.White;
                tb_SchoolWE.Enabled = true;
            }
        }
        private void cb_HolidayWeek_CheckedChanged(object sender, EventArgs e)
        {
            if (tb_HolidayWeek.Enabled)
            {
                tb_HolidayWeek.Clear();
                tb_HolidayWeek.BackColor = SystemColors.Control;
                tb_HolidayWeek.Enabled = false;
            }
            else
            {
                tb_HolidayWeek.Clear();
                tb_HolidayWeek.BackColor = Color.White;
                tb_HolidayWeek.Enabled = true;
            }
        }
        private void cb_HolidayWE_CheckedChanged(object sender, EventArgs e)
        {
            if (tb_HolidayWE.Enabled)
            {
                tb_HolidayWE.Clear();
                tb_HolidayWE.BackColor = SystemColors.Control;
                tb_HolidayWE.Enabled = false;
            }
            else
            {
                tb_HolidayWE.Clear();
                tb_HolidayWE.BackColor = Color.White;
                tb_HolidayWE.Enabled = true;
            }
        }
        private void bt_addAdult_Click(object sender, EventArgs e)
        {
            addPerson(18);
        }
        private void bt_addChild_Click(object sender, EventArgs e)
        {
            addPerson(14);
        }
        private void bt_AddSmallChild_Click(object sender, EventArgs e)
        {
            addPerson(1);
        }
        private void bt_addPerson_Click(object sender, EventArgs e)
        {
            addPerson(Convert.ToInt32(tb_Age.Text));
            tb_Age.Text = "";
        }
        private void bt_undo_Click(object sender, EventArgs e)
        {
            string toRemove = lb_GroupMembers.Items[lb_GroupMembers.Items.Count - 1].ToString();
            lb_GroupMembers.Items.RemoveAt(lb_GroupMembers.Items.Count - 1);
            if (toRemove.ToString() == "Erwachsener")
            {
                lbl_AdultCount.Text = (Convert.ToInt32(lbl_AdultCount.Text) - 1).ToString().PadLeft(2);
            }
            else if (toRemove.ToString() == "Kind")
            {
                lbl_ChildCount.Text = (Convert.ToInt32(lbl_ChildCount.Text) - 1).ToString().PadLeft(2);
            }
            else if (toRemove.ToString() == "Kleinkind")
            {
                lbl_SmallChildCount.Text = (Convert.ToInt32(lbl_SmallChildCount.Text) - 1).ToString().PadLeft(2);
            }
            if (lb_GroupMembers.Items.Count == 0)
            {
                bt_undo.Enabled = false;
            }
        }
        private void tb_Age_TextChanged(object sender, EventArgs e)
        {
            if (tb_Age.Text == "")
            {
                bt_addPerson.Enabled = false;
                tb_Age.BackColor = Color.White;
            }
            else
            {
                bool valid = true;
                try
                {
                    Convert.ToInt32(tb_Age.Text);
                }
                catch (Exception)
                {
                    valid = false;
                }
                bt_addPerson.Enabled = valid;
                if (valid)
                {
                    tb_Age.BackColor = Color.White;
                }
                else
                {
                    tb_Age.BackColor = Color.Red;
                }
            }
        }
        private void tb_SchoolWeek_TextChanged(object sender, EventArgs e)
        {
            if (tb_SchoolWeek.Text == "")
            {
                bt_Calculate.Enabled = false;
                tb_SchoolWeek.BackColor = Color.White;
            }
            else
            {
                bool valid = true;
                try
                {
                    Convert.ToInt32(tb_SchoolWeek.Text);
                }
                catch (Exception)
                {
                    valid = false;
                }
                bt_Calculate.Enabled = valid;
                if (valid)
                {
                    tb_SchoolWeek.BackColor = Color.White;
                }
                else
                {
                    tb_SchoolWeek.BackColor = Color.Red;
                }
            }
        }
        private void tb_SchoolWE_TextChanged(object sender, EventArgs e)
        {
            if (tb_SchoolWE.Text == "")
            {
                bt_Calculate.Enabled = false;
                tb_SchoolWE.BackColor = Color.White;
            }
            else
            {
                bool valid = true;
                try
                {
                    Convert.ToInt32(tb_SchoolWE.Text);
                }
                catch (Exception)
                {
                    valid = false;
                }
                bt_Calculate.Enabled = valid;
                if (valid)
                {
                    tb_SchoolWE.BackColor = Color.White;
                }
                else
                {
                    tb_SchoolWE.BackColor = Color.Red;
                }
            }
        }
        private void tb_HolidayWeek_TextChanged(object sender, EventArgs e)
        {
            if (tb_HolidayWeek.Text == "")
            {
                bt_Calculate.Enabled = false;
                tb_HolidayWeek.BackColor = Color.White;
            }
            else
            {
                bool valid = true;
                try
                {
                    Convert.ToInt32(tb_HolidayWeek.Text);
                }
                catch (Exception)
                {
                    valid = false;
                }
                bt_Calculate.Enabled = valid;
                if (valid)
                {
                    tb_HolidayWeek.BackColor = Color.White;
                }
                else
                {
                    tb_HolidayWeek.BackColor = Color.Red;
                }
            }
        }
        private void tb_HolidayWE_TextChanged(object sender, EventArgs e)
        {
            if (tb_HolidayWE.Text == "")
            {
                bt_Calculate.Enabled = false;
                tb_HolidayWE.BackColor = Color.White;
            }
            else
            {
                bool valid = true;
                try
                {
                    Convert.ToInt32(tb_HolidayWE.Text);
                }
                catch (Exception)
                {
                    valid = false;
                }
                bt_Calculate.Enabled = valid;
                if (valid)
                {
                    tb_HolidayWE.BackColor = Color.White;
                }
                else
                {
                    tb_HolidayWE.BackColor = Color.Red;
                }
            }
        }
        private void bt_Calculate_Click(object sender, EventArgs e)
        {
            if (lb_GroupMembers.Items.Count > 0)
            {
                string cmdLine = "";
                if (tb_SchoolWeek.Text == "")
                {
                    cmdLine += int.MaxValue.ToString() + " ";
                }
                else
                {
                    cmdLine += tb_SchoolWeek.Text + " ";
                }
                if (tb_SchoolWE.Text == "")
                {
                    cmdLine += int.MaxValue.ToString() + " ";
                }
                else
                {
                    cmdLine += tb_SchoolWE.Text + " ";
                }
                if (tb_HolidayWeek.Text == "")
                {
                    cmdLine += int.MaxValue.ToString() + " ";
                }
                else
                {
                    cmdLine += tb_HolidayWeek.Text + " ";
                }
                if (tb_HolidayWE.Text == "")
                {
                    cmdLine += int.MaxValue.ToString() + " ";
                }
                else
                {
                    cmdLine += tb_HolidayWE.Text + " ";
                }
                bool first = true;
                cmdLine += tb_Coupons.Text + " ";
                foreach (object o in lb_GroupMembers.Items)
                {
                    if (!first)
                    {
                        cmdLine += ",";
                    }
                    switch (o.ToString())
                    {
                        case "Erwachsener":
                            cmdLine += "18";
                            break;
                        case "Kind":
                            cmdLine += "10";
                            break;
                        case "Kleinkind":
                            cmdLine += "0";
                            break;
                    }
                    first = false;
                }
                MessageBox.Show(cmdLine);
                calculate(cmdLine);
            }
            else
            {
                lb_output.Items.Clear();
                lbl_Total.Text = "Gesamt: ";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
