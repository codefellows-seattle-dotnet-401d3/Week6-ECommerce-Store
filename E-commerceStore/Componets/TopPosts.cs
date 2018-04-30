using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace EcommerceStore.Componets
{

    /* insert the mini-controllers here for the view componets; the box within a box
     *  
     */


    [ViewComponent]
    public class TopPostsComponet : ViewComponent
    {

       // private WeekSix _componet;

        //bring in database context in order to perform dependency injection
        /* public TopPostsComponet(insert database here, context )
        {
        _context= context
        }
        */

      //  public IViewComponetResult Invoke(int number)
       // { //you need parameters 

            //insert logic here, insert interface & dependency injection DI, create a interface in solution and invoke it here.
            //Insert product items here

            // var posts = await _conext.Post.Take(number).ToListAsync();
            //return view here(Posts) -> sends to default Componet folder, looks for the defult route;




        //}


        /*
         * 
         */


    }
}
