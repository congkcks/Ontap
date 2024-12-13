
Scaffold-DbContext "Data Source=CONGKC\SQLEXPRESS;Initial Catalog=OnlineShop;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models


builder.Services.AddDbContext<OnlineShopContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

 Components
 ViewComponents
