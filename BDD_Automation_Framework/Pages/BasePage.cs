using BDD_Automation.Context;


namespace BDD_Automation.Pages
{
    public class BasePage
    {

        public Driver Driver { get; private set; }
        public Assert Assert { get; private set; }
        public RandomHelper Random { get; private set; }

       
    }
 }
