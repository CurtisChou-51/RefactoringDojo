namespace GildedRose.TestProject
{
    public class UnitTest2
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_normalItem()
        {
            var normalItem1 = new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 };
            var normalItem2 = new Item { Name = "+5 Dexterity Vest", SellIn = 15, Quality = 20 };
            var normalItem3 = new Item { Name = "+5 Dexterity Vest", SellIn = 20, Quality = 20 };
            List<Item> items = [normalItem1, normalItem2, normalItem3];
            var app = new App(items);

            for (int i = 0; i < 16; i++)
                app.UpdateQuality();

            Assert.That(normalItem1.Quality, Is.EqualTo(0));  // min quality is 0
            Assert.That(normalItem1.SellIn, Is.EqualTo(-6));

            Assert.That(normalItem2.Quality, Is.EqualTo(3));  // 20 - 16 - 1 (過期1天)
            Assert.That(normalItem2.SellIn, Is.EqualTo(-1));

            Assert.That(normalItem3.Quality, Is.EqualTo(4));  // 20 - 16
            Assert.That(normalItem3.SellIn, Is.EqualTo(4));
        }

        [Test]
        public void Test_normalItem_base()
        {
            var normalItem1 = new BaseItem { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 };
            var normalItem2 = new BaseItem { Name = "+5 Dexterity Vest", SellIn = 15, Quality = 20 };
            var normalItem3 = new BaseItem { Name = "+5 Dexterity Vest", SellIn = 20, Quality = 20 };
            List<BaseItem> items = [normalItem1, normalItem2, normalItem3];

            for (int i = 0; i < 16; i++)
                foreach (var item in items)
                    item.Update();

            Assert.That(normalItem1.Quality, Is.EqualTo(0));  // min quality is 0
            Assert.That(normalItem1.SellIn, Is.EqualTo(-6));

            Assert.That(normalItem2.Quality, Is.EqualTo(3));  // 20 - 16 - 1 (過期1天)
            Assert.That(normalItem2.SellIn, Is.EqualTo(-1));

            Assert.That(normalItem3.Quality, Is.EqualTo(4));  // 20 - 16
            Assert.That(normalItem3.SellIn, Is.EqualTo(4));
        }

        [Test]
        public void Test_normalItem_conjuredItem()
        {
            var normalItem1 = new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 50 };
            var normalItem2 = new Item { Name = "+5 Dexterity Vest", SellIn = 1, Quality = 50 };
            var conjuredItem1 = new Item { Name = "Conjured Mana Cake", SellIn = 10, Quality = 50 };
            var conjuredItem2 = new Item { Name = "Conjured Mana Cake", SellIn = 1, Quality = 50 };
            var conjuredItem3 = new Item { Name = "Conjured Mana Cake", SellIn = 10, Quality = 3 };
            List<Item> items = [normalItem1, conjuredItem1, normalItem2, conjuredItem2, conjuredItem3];
            var app = new App(items);

            for (int i = 0; i < 2; i++)
                app.UpdateQuality();
            Assert.That(normalItem1.Quality, Is.EqualTo(48));    // 50 - 2
            Assert.That(conjuredItem1.Quality, Is.EqualTo(46));  // 50 - 2*2
            Assert.That(normalItem2.Quality, Is.EqualTo(47));    // 50 - 2 - 1 (過期1天)
            Assert.That(conjuredItem2.Quality, Is.EqualTo(44));  // 50 - 2*2 - 1*2 (過期1天)
            Assert.That(conjuredItem3.Quality, Is.EqualTo(0));   // 3 - 2*2, but min quality is 0
        }
    }
}
