using Shop_Toshmatovv.Data.Models;

public class ItemsBasket
{
    public int Count { get; set; }
    public Items Item { get; set; }

    public ItemsBasket(int count, Items item)
    {
        Count = count;
        Item = item;
    }
}