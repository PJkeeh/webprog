using System;
using System.Collections.Generic;
using System.Linq;
using webprog.Domain;

namespace webprog
{
    public partial class deleteShopping : pageFunctions
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int matchID;
            int ttID;
            if (!loggedIn())
            {
                Response.Redirect("login.aspx");
            }
            else if (!(Request.QueryString["m"] == null
                || Request.QueryString["tt"] == null)&&Int32.TryParse(Request.QueryString["m"], out matchID) &&Int32.TryParse(Request.QueryString["tt"], out ttID))
            {
                if (Session["shoppingCart"] != null)
                {
                    List<Ticket> cart = (List<Ticket>)Session["shoppingCart"];
                    List<int> toRemove = new List<int>();
                    for (int i = 0; i < cart.Count; i++)
                    {
                        if (cart[i].match.id == matchID && cart[i].ticket_type.id == ttID)
                        {
                            toRemove.Add(i);
                        }
                    }
                    for (int i = toRemove.Count -1; i >= 0; i--)
                    {
                        cart.RemoveAt(toRemove[i]);
                    }
                    Session["shoppingCart"] = cart;
                }
            }
            if(Request.QueryString["m"] != null)
            {
                Session["abo_cart"] = null;
            }
            Response.Redirect("shoppingcart.aspx");
        }
    }
}