Tu będzie zad4 (cw5)





delete json from /.config

jdbc:jtds:sqlserver://db-mssql.pjwstk.edu.pl/s24651
db-mssql.pjwstk.edu.pl
USENTLMV2: true
DOMAIN: PJWSTK
 
dotnet ef dbcontext scaffold "Data Source=db-mssql;Initial Catalog=s24651;Integrated Security=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models --context-dir Context
