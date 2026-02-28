using Shop_Toshmatovv.Data.Models;

public class ItemsBasket : Items
{
    // <summary> Кол-во в корзине
    public int Count { get; set; }

    public ItemsBasket(int Count, Items item) : base(item)
    {
        this.Count = Count;
    }
}