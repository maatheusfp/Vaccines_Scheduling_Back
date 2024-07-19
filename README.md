# Vaccines Scheduling Back
### Como rodar
1. De início, clone o repositório na pasta de sua preferência. Recomendo clonar em uma pasta que não possua outro repositório para evitar conflitos.
2. Por algum motivo, passando para o github o repositório possui dois arquivos .sln. Assim, o correto é o que está dentro da pasta .\Vaccines_Scheduling\. Utilizei o Visual Studio 2022.
3. Para que funcione corretamente, é necessário ajustar a pasta appsettings.json. Em ConnectionStrings, referencie tanto a database quanto o seu server. Utilizando o 
SQL Server Mananagement studio 20, crie o banco de dados com a query: https://gist.github.com/maatheusfp/b8eb1d01e3490237af3beebf315475c6
4. Com o banco criado, basta rodar a aplicação. A configuração para lançar no navegador está desativada, então será necessário abri-lo manualmente no navegador. A porta está presente 
no arquivo de launchSettings.json. (https://localhost:7175)

### Possíveis Problemas 
Ao rodar os testes simultaneamente, alguns alegam problemas de trackeamento ou alguma exceção inesperada. Se rodar aqueles que não funcionarem isoladamente (teste unitário) tendem a funcionar
normalmente. 


