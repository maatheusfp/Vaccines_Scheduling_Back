# Vaccines Scheduling Back
### Web App para agendamento de Vacinação feito em .Net
## O que você vai encontrar:
1. Arquitetura em camadas simples: Controller -> Camada de Serviço (Regras de negócios sendo aplicadas) -> Camada de Repositório (Chamada ao banco);
2. Organização das Entidades em classes seguindo o Princípio SOLID da responsabilidade única;
3. Validações utilizando o FluentValidation (https://docs.fluentvalidation.net/en/latest/);
4. Uso de Middleware para evitar repetição de try catch ao longo do código;
5. Uso de Mandatory Transaction para dar rollback nas alterações caso seja acionado alguma exceção na rota.
6. Autenticação de Login feita utilizando Token JWT Bearer.  

### Como rodar
1. De início, clone o repositório na pasta de sua preferência. Recomendo clonar em uma pasta que não possua outro repositório para evitar conflitos.
2. Por algum motivo, passando para o github o repositório possui dois arquivos .sln. Assim, o correto é o que está dentro da pasta .\Vaccines_Scheduling\. . Utilizei o Visual Studio 2022.
3. Para que funcione corretamente, é necessário ajustar a pasta appsettings.json. Em ConnectionStrings, referencie tanto a database quanto o seu server. Utilizando o 
SQL Server Mananagement studio 20, crie o banco de dados com a query: https://gist.github.com/maatheusfp/b8eb1d01e3490237af3beebf315475c6
4. Com o banco criado, basta rodar a aplicação. A configuração para lançar no navegador está desativada, então será necessário abri-lo manualmente no navegador. A porta está presente 
no arquivo de launchSettings.json. (https://localhost:7175)

### Possíveis Problemas 
Ao rodar os testes simultaneamente, alguns alegam problemas de trackeamento ou alguma exceção inesperada. Se rodar aqueles que não funcionarem isoladamente (teste unitário) tendem a funcionar
normalmente. 

**OBS:** Esse é o meu primeiro projeto em .Net, então é bem provável que haja diversas possibilidades de melhorias. Fique à vontade para dar feedbacks! 


