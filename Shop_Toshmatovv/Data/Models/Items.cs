namespace Shop_Toshmatovv.Data.Models
{
    public class Items
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        public int Price { get; set; }
        public Categorys Categorys { get; set; } = new Categorys();
        public Items(Items item = null)
        {
            if (item != null)
            {
                this.Id = item.Id;
                this.Name = item.Name;
                this.Description = item.Description;
                this.Img = item.Img;
                this.Price = item.Price;
                this.Categorys = item.Categorys;
            }
        }
    }
}