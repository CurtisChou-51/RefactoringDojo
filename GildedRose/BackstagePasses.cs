namespace GildedRose
{
    public class BackstagePasses : BaseItem
    {
        public override void Update()
        {
            SellIn -= 1;
            Quality = SellIn switch
            {
                < 0 => 0,
                < 5 => Quality + 3,
                < 10 => Quality + 2,
                _ => Quality + 1
            };
            Quality = Math.Min(Quality, 50);
        }
    }
}
