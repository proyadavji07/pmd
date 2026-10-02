// step-1:Create a new project → name the folder OcelotAPI —> next
// step-2:Right click on Dependencies —> Manage NuGet Packages —> Browse search for ocelot and install the package
// step-3:Right click on solution ocelot —> add —> new project —> name the MvcService
// open OcelotAPI —> program.cs) and update starting code
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange:true);
builder.Services.AddOcelot();

// step-4: Right Click on OcelotAPI->add-> new item-> ocelote.json(select json type) and write a code
//   for BaseUrl goto OcelotAPI-> properties-> launchSetting.json
{
  "Routes": [
    {
      "DownstreamPathTemplate": "/Home/Index",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 5151
        }
      ],
      "UpstreamPathTemplate": "/gateway/home",
      "UpstreamHttpMethod": [
        "Get"
      ]
    }
  ],
  "GlobalConfiguration": {
    "BaseUrl": "http://localhost:5101"
  }
}

// step-5: in MvcService-> models-> right click add class -> StockQuote.cs and update a code
namespace MVCService.Models
{
    public class StockQuote
    {
        public string? Symbol { get; set; }
        public int Price {  get; set; }
    }
}

// MvcService —> Controller—>Homecontroller.cs —> write or updae a code the code
public IActionResult Index()
{
    var model = new StockQuote { Symbol = "Nike", Price = 5000 };
    return View(model);
}

// in MvcService -> view -> Home -> Index.cshtml and add this code
<h2>Symbol: @Model.Symbol</h2>
<h2>Price: @Model.Price</h2>

// run MvcService
// for both: Right click on solution explorer —> properties —> select on multiple —> and start the mvcservice and ocelot —> apply and click ok
