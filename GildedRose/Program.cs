namespace GildedRose;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("OMGHAI!");

        IList<BaseItem> Items = new List<BaseItem>{
            new BaseItem {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
            new AgedBrie {Name = "Aged Brie", SellIn = 2, Quality = 0},
            new BaseItem {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
            new Sulfuras {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
            new Sulfuras {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80},
            new BackstagePasses
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 15,
                Quality = 20
            },
            new BackstagePasses
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 10,
                Quality = 49
            },
            new BackstagePasses
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 5,
                Quality = 49
            },
	            new Conjured {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
        };

        var app = new App(Items);


        for (var i = 0; i < 31; i++)
        {
            Console.WriteLine("-------- day " + i + " --------");
            Console.WriteLine("name, sellIn, quality");
            for (var j = 0; j < Items.Count; j++)
            {
                System.Console.WriteLine(Items[j]);
            }
            Console.WriteLine("");
            app.UpdateQuality();
        }
    }
}
