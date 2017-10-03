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
    public partial class Form1 : Form
    {
        private void calculate(string input)
        {
            string[] args = input.Split(' ');
            if (args.Count() < 6)
            {
                args = "100 0 100 0 0 14,14,14,14,14,14,14,14".Split(' ');
            }
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

            Offer[] minimalPrices = new Offer[4];
            int children = 0;
            int adults = 0;
            int smallChildren = 0;
            foreach (int age in ages)
            {
                if (age > 16)
                {
                    adults++;
                }
                else if (age < 4)
                {
                    smallChildren++;
                }
                else
                {
                    children++;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                Offer currentPrice = getPrice(smallChildren, children, adults, i);
                if (i < 2)
                {
                    if (couponCount >= ages.Count())
                    {
                        List<string> coupons = new List<string>();
                        foreach (int age in ages)
                        {
                            coupons.Add("Einzelgutschein");
                        }
                        currentPrice = new Offer(coupons, 0);
                    }
                    else
                    {
                        for (int childrenCoupons = 0; childrenCoupons <= couponCount && childrenCoupons <= children; childrenCoupons++)
                        {
                            Offer tempPrice = getPrice(smallChildren, children - childrenCoupons, adults + childrenCoupons - couponCount, i);
                            for (int j = couponCount; j > 0; j--)
                            {
                                tempPrice.Tickets.Add("Einzelgutschein");
                            }
                            if (tempPrice.Price < currentPrice.Price)
                            {
                                currentPrice = tempPrice;
                            }
                        }
                        for (int childrenCoupons = 0; childrenCoupons <= couponCount - 1 && childrenCoupons <= children; childrenCoupons++)
                        {
                            Offer tempPrice = getPrice(smallChildren, children - childrenCoupons, adults - (couponCount - 1 - childrenCoupons), i);
                            tempPrice.Price *= 0.9;
                            tempPrice.Tickets.Add("Gruppengutschein");
                            for (int j = couponCount - 1; j > 0; j--)
                            {
                               tempPrice.Tickets.Add("Einzelgutschein");
                            }
                            if (tempPrice.Price < currentPrice.Price)
                            {
                                currentPrice = tempPrice;
                            }
                        }
                    }
                }
                minimalPrices[i] = currentPrice;
            }

            int best = 0;
            for (int i = 0; i < 4; i++)
            {
                if (minimalPrices[i].Price + Convert.ToDouble(differenceNeeded[i]) < minimalPrices[best].Price + Convert.ToDouble(differenceNeeded[best]))
                {
                    best = i;
                }
            }
            returnResult(minimalPrices[best], best);
        }
        public void returnResult(Offer offer, int day)
        {
            if (offer.Price > 0)
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
                if (age > 16)
                {
                    adults++;
                }
                else if (age < 4)
                {
                    smallChildren++;
                }
                else
                {
                    children++;
                }
            }
            return getPrice(smallChildren, children, adults, day);
        }
        public static Offer getPrice(int smallChildren, int children, int adults, int day)
        {
            if (smallChildren == 0 || adults > 0)
            {
                Offer best = OnlySinglePrices(children, adults);
                if (adults + children <= 6)
                {
                    if (day % 2 == 0)
                    {
                        best = FixedPricesDuringWeek(children, adults);
                    }
                    else
                    {
                        best = FixedPricesDuringWeekEnd(children, adults);
                    }
                }
                else
                {
                    Offer contestant = new Offer(new List<string>(), double.MaxValue);
                    if (day % 2 == 0)
                    {
                        contestant = CombinedPricesDuringWeek(children, adults);
                    }
                    else
                    {
                        contestant = CombinedPricesDuringWeekEnd(children, adults);
                    }
                    if (best.Price > contestant.Price)
                    {
                        best = contestant;
                    }
                }
                return best;
            }
            else
            {
                return new Offer(new List<string>(), -1);
            }
        }
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
            foreach (var v in calculatedPricesWE)
            {
                if (v.input.adults == adults && v.input.children == children)
                {
                    return v.output;
                }
            }
            Offer best = OnlySinglePrices(children, adults);
            if (children + adults > 6)
            {
                for (int i = 1; i <= 6 && i <= children + adults; i++)
                {
                    for (int j = 0; j <= i && j <= children; j++)
                    {
                        if (adults - (i - j) >= 0)
                        {
                            Offer part1 = FixedPricesDuringWeekEnd(j, i - j);
                            Offer part2 = CombinedPricesDuringWeekEnd(children - j, adults - (i - j));
                            if (part1.Price + part2.Price < best.Price)
                            {
                                part1.Tickets.AddRange(part2.Tickets);
                                best = new Offer(part1.Tickets, part1.Price + part2.Price);
                            }
                        }
                    }
                }
            }
            else
            {
                best = FixedPricesDuringWeek(children, adults);
            }
            calculatedPricesWE.Add(((children, adults), best));
            return best;
        }

        static List<((int children, int adults) input, Offer output)> calculatedPricesW = new List<((int children, int adults) input, Offer output)>();
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
            foreach (var v in calculatedPricesW)
            {
                if (v.input.adults == adults && v.input.children == children)
                {
                    return v.output;
                }
            }
            Offer best = OnlySinglePrices(children, adults);
            if (children + adults > 6)
            {
                for (int i = 1; i <= 6 && i <= children + adults; i++)
                {
                    for (int j = 0; j <= i && j <= children; j++)
                    {
                        if (adults - (i - j) >= 0)
                        {
                            Offer part1 = FixedPricesDuringWeek(j, i - j);
                            Offer part2 = CombinedPricesDuringWeek(children - j, adults - (i - j));
                            if (part1.Price + part2.Price < best.Price)
                            {
                                part1.Tickets.AddRange(part2.Tickets);
                                best = new Offer(part1.Tickets, part1.Price + part2.Price);
                            }
                        }
                    }
                }
            }
            else
            {
                best = FixedPricesDuringWeek(children, adults);
            }
            calculatedPricesW.Add(((children, adults), best));
            return best;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
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
                calculate(cmdLine);
            }
            else
            {
                lb_output.Items.Clear();
                lbl_Total.Text = "Gesamt: ";
            }
        }
    }
}
