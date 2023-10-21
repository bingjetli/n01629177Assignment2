using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace n01629177Assignment2.Controllers
{
    public class J1Controller : ApiController
    {
        /// <summary>
        /// Returns the total calories of the meal.
        /// </summary>
        /// <example>api/J1/Menu/4/4/4/4/</example>
        /// <example>api/J1/Menu/1/2/3/4/</example>
        /// <param name="burger">1:Cheeseburger:461kcals, 2:Fish Burger:431kcals, 3:Veggie Burger:420kcals, 4:No burger</param>
        /// <param name="drink">1:SoftDrink:130kcals, 2:Orange Juice:160kcals, 3:Milk:118kcals, 4:No Drink</param>
        /// <param name="side">1:Fries:100kcals, 2:Baked Potato:57kcals, 3:Chef Salad:70kcals, 4:No Side</param>
        /// <param name="dessert">1:Apple Pie:167kcals, 2:Sundae:266kcals, 3:Fruit Cup:75kcals,  4:No Dessert</param>
        /// <returns>Returns the total calories of the meal.</returns>
        [Route("api/J1/Menu/{burger}/{drink}/{side}/{dessert}")]
        public int GetMenu(int burger=4, int drink=4, int side=4, int dessert=4)
        {
            int[] burgers = new int[] {
                461, /*Cheeseburger*/
                431, /*Fish Burger*/
                420, /*Veggie Burger*/
                0 /*No Burger*/
            };

            int[] drinks = new int[]
            {
                130, /*Soft Drink*/
                160, /*Orange Juice*/
                118, /*Milk*/
                0, /*No Drink*/
            };

            int[] sides = new int[]
            {
                100, /*Fries*/
                57, /*Baked Potato*/
                70, /*Chef Salad*/
                0, /*No Side Order*/
            };

            int[] desserts = new int[]
            {
                167, /*Apple Pie*/
                266, /*Sundae*/
                75, /*Fruit Cup*/
                0, /*No Dessert*/
            };

            return burgers[burger-1] + 
                drinks[drink-1] + 
                sides[side-1] + 
                desserts[dessert-1];
        }
    }
}
