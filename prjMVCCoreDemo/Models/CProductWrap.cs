using System.ComponentModel;

namespace prjMVCCoreDemo.Models
{
    public class CProductWrap
    {
        private TProduct _product = null;
        public CProductWrap()
        {
            _product = new TProduct();
        }
        public int FId
        {
            get { return _product.FId; }
            set { _product.FId = value; }
        }
        [DisplayName("課程名稱")]
        public string? FName
        {
            get { return _product.FName; }
            set { _product.FName = value; }
        }
        [DisplayName("人數")]

        public int? FQty
        {
            get { return _product.FQty; }
            set { _product.FQty = value; }
        }
        [DisplayName("成本")]


        public decimal? FCost
        {
            get { return _product.FCost; }
            set { _product.FCost = value; }
        }

        [DisplayName("費用")]

        public decimal? FPrice
        {
            get { return _product.FPrice; }
            set { _product.FPrice = value; }
        }

        [DisplayName("檔名")]

        public string? FImagePath
        {
            get { return _product.FImagePath; }
            set { _product.FImagePath = value; }
        }

        public IFormFile photo { get;set; }

    }
    public static class CProductWrapExt
    {
        public static CProductWrap toWrap(this TProduct product)
        {
            CProductWrap productWrap = new CProductWrap();
            productWrap.FId = product.FId;
            productWrap.FName = product.FName;
            productWrap.FQty = product.FQty;
            productWrap.FCost = product.FCost;
            productWrap.FPrice = product.FPrice;
            productWrap.FImagePath= product.FImagePath;

            return productWrap;
        }
    }
}
