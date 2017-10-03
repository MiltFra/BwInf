using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BwInf_36._1._2
{
    class Program
    {
        static void Main(string[] args)
        {
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
                        minimalPrices[i] = new Offer(coupons, 0);
                    }
                    else
                    {
                        for (int childrenCoupons = 0; childrenCoupons <= couponCount && childrenCoupons <= children; childrenCoupons++)
                        {
                            Offer tempPrice = getPrice(smallChildren, children - childrenCoupons, adults + childrenCoupons - couponCount, i);
                            for (int j = couponCount; j > 0; j++)
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
                            for (int j = couponCount - 1; j > 0; j++)
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
            Console.WriteLine("Eine Lösung wurde gefunden, folgender Termin mit beiliegenden Eintrittskarten ist für Sie am besten:");
            switch(best)
            {
                case 0:
                    Console.WriteLine("Ferien: Nein, Wochenende: Nein");
                    break;
                case 1:
                    Console.WriteLine("Ferien: Nein, Wochenende:   Ja");
                    break;
                case 2:
                    Console.WriteLine("Ferien:   Ja, Wochenende: Nein");
                    break;
                case 3:
                    Console.WriteLine("Ferien:   Ja, Wochenende:   Ja");
                    break;
            }
            returnResult(minimalPrices[best]);
        }
        public static void returnResult(Offer offer)
        {
            Console.WriteLine("Preis: {0} Euro", offer.Price.ToString("N2"));
            int tag = 0;
            int familie = 0;
            int ad = 0;
            int ch = 0;
            foreach (string s in offer.Tickets)
            {
                if (s == "Tag") { tag++; }
                if (s == "Familie") { familie++; }
                if (s == "Erwachsen") { ad++; }
                if (s == "Kind") { ch++; }
            }
            Console.Write("Tageskarten: {0}\nFamilienkarten: {1}\nErwachsene: {2}\nKinder: {3}\n", tag, familie, ad, ch);
            Console.ReadKey();
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
                    total = new Offer(new List<string>() { "Kind", "Kind", "Kind", "Kind" }, 10);
                }
            }
            else if (adults + children == 5)
            {
                if (children >= 3 && adults >= 1)
                {
                    total = new Offer(new List<string>() { "Familie", "Kind" }, 10.50);
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
            total.Price *= 0.8;
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
    }
}
