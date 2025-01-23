namespace GildedRose
{
    public class BaseItem : Item
    {
        public virtual void Update()
        {
            SellIn -= 1;
            int rate = SellIn < 0 ? 2 : 1;
            Quality = Math.Max(Quality - rate, 0);
        }
    }
}
