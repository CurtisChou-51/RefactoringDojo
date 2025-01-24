namespace GildedRose
{
    public class App
    {
        IList<BaseItem> Items;
        public App(IList<BaseItem> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                item.Update();
            }
        }
    }
}
