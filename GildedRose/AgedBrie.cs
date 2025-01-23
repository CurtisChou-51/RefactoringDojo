namespace GildedRose
{
    public class AgedBrie : BaseItem
    {
        public override void Update()
        {
            SellIn -= 1;
            int rate = SellIn < 0 ? 2 : 1;
            Quality = Math.Min(Quality + rate, 50);
        }
    }
}
