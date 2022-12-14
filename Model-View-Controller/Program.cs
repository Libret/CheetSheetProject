using Model_View_Controller.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//Model_View_Controller.Repositories.Migrations.Run();
/*

var allItemsForTopic = Model_View_Controller.Repositories.TopicRepository.GetTopicWithAllItems("a8c4f9ac-9092-4e20-864c-1001bc31d5d1");


Model_View_Controller.Repositories.UsefulLinkRepository.AddNewUsefulLink(firstUsefulLink, "871420ef-744d-4900-9774-34f3fef587f2");

var newItem = new CheetSheetItem
{
    Name = "useful command",
    AdditionalInfo = "basic tips for git navigation and interaction",
    CodeSnippet = ""
};

Model_View_Controller.Repositories.CheetSheetItemRepository.AddNewCheetSheetItem(newItem, "a8c4f9ac-9092-4e20-864c-1001bc31d5d1");

var secondUsefulLink = new UsefulLink
{
    LinkAddress = "https://learngitbranching.js.org/",
    LinkOrder = 0
};
Model_View_Controller.Repositories.UsefulLinkRepository.AddNewUsefulLink(secondUsefulLink, "78a16b68-5241-49b0-af81-83e1893a8985");
*/



app.Run();