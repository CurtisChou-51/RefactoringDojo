namespace GildedRose
{
    public class Conjured : BaseItem
    {
        public override void Update()
        {
            SellIn -= 1;
            int rate = SellIn < 0 ? 4 : 2;
            Quality = Math.Max(Quality - rate, 0);
        }
    }
}
