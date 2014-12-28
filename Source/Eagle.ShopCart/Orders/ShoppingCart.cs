using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data.Common;
using System.Data;
using Eagle.Common.Utilities;
using Eagle.ShopCart.Products;

namespace Eagle.ShopCart.Orders
{
    /**
     * The ShoppingCart class holds the items that are in the cart and provides methods for their manipulation
     * * Lưu trữ sản phẩm đã mua và thực hiện một số các phương thức khác
     */
    [Serializable]
    public class ShoppingCart
    {
        #region Properties

        public List<CartItem> Items { get; private set; }

        #endregion

        #region Singleton Implementation            
     
        // Hàm khởi tạo
        public static ShoppingCart Instance
        {
            get
            {
                if (HttpContext.Current.Session["ASPNETShoppingCart"] == null)
                {
                    // we are creating a local variable and thus
                    // not interfering with other users sessions
                    ShoppingCart instance = new ShoppingCart();
                    instance.Items = new List<CartItem>();
                    HttpContext.Current.Session["ASPNETShoppingCart"] = instance;
                    return instance;
                }
                else
                {
                    // we are returning the shopping cart for the given user
                    return (ShoppingCart)HttpContext.Current.Session["ASPNETShoppingCart"];
                }
            }
        }


        protected ShoppingCart() { }

        #endregion

        #region Item Modification Methods
        /**
	 * AddItem() - Add một item vào trong giỏ hàng
	 */
        public void AddItem(int productId, int num_items)
        {
            // Tạo mới một Cartitem
            CartItem newItem = new CartItem(productId);

            if (Items.Contains(newItem))
            {
                foreach (CartItem item in Items)
                {
                    int UnitsInStock = ProductController.GetUnitsInStockByProductId(productId);
                    if ((item.Quantity + num_items) <= UnitsInStock)
                    {
                        if (item.Equals(newItem))
                        {
                            item.Quantity += num_items;
                            return;
                        }
                    }
                }
            }
            else
            {
                newItem.Quantity = num_items;
                Items.Add(newItem);
            }
        }

        //public void AddItem(int productId)
        //{
        //    // Tạo mới một Cartitem
        //    CartItem newItem = new CartItem(productId);

        //    if (Items.Contains(newItem))
        //    {
        //        foreach (CartItem item in Items)
        //        {
        //            if (item.Equals(newItem))
        //            {
        //                item.Quantity++;
        //                return;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        newItem.Quantity = 1;
        //        Items.Add(newItem);
        //    }
        //}

        /**
         * SetItemQuantity() - Thay đổi số lượng của sản phẩm trong giỏ hàng
         */
        public void SetItemQuantity(int productId, int quantity)
        {
            // Nếu số lượng bằng 0 thì xóa item
            if (quantity == 0)
            {
                RemoveItem(productId);
                return;
            }

            // Tìm và update số lượng cho item trong giỏ hàng
            CartItem updatedItem = new CartItem(productId);
            foreach (CartItem item in Items)
            {
                if (item.Equals(updatedItem))
                {
                    item.Quantity = quantity;
                    return;
                }
            }
        }
        /**
         * RemoveItem() - Xóa item trong giỏ hàng
         */
        public void RemoveItem(int productId)
        {
            CartItem removedItem = new CartItem(productId);
            Items.Remove(removedItem);
        }

        public void RemoveAll()
        {
            Items.Clear();
        }

        #endregion

        #region Reporting Methods
        /**
	 * GetSubTotal() - Tính tổng tiền
	 */
        public decimal GetSubTotal()
        {
            decimal subTotal = 0;
            foreach (CartItem item in Items)
                subTotal += item.TotalPrice;

            return subTotal;
        }
        #endregion

    }
}
