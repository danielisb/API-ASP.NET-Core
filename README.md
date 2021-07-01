## API (backend)

O projeto contém uma API backend, com ASP.NET Core. Dada uma request, a mesma retorna uma lista de objetos, desserializada de um arquivo json. O carregamento da response (lista dos objetos) contém um controle de listagem de metadados de paginação.

![arquitetura](imagens/arquitetura.png?raw=true "")
<br>

**Ferramentas utilizadas**

* Linguagem C#
* Visual Studio 2019 (versão Mac)
* .Net 5.0.301
* Postman
* Git
<br>

**Pacotes usados**

* Microsoft.EntityFrameworkCore.InMemory
* Newtonsoft.Json
<br>

#### Desenvolvimento:
1. Criação do projeto no VisualSutdio:
	```shell
	dotnet new webapi -o nomedoprojeto
	```

2. Incluir dados in memory:
	lógica nos métodos **ConfigureServices()** e **LoadJsonFile()**;

3. Deserializar json:
	Utilizado o pacote Newtonsoft.Json;
	Método responsável: **JsonDeserializer()**;

4. Entity framework
	Utilizado o pacote Microsoft.EntityFrameworkCore.InMemory;
	Método utilizado: **SaveImovel()**;

5. Metadados de Paginação:
	Requisição [HttpGet];
	lógica desenvolvida no método **GetByPortal()**;

6. Regra de negócio.
	Requisição [HttpGet];
	lógica desenvolvida no método **GetByPortal()**;
<br>

#### Executar projeto localmente
1. Execute o projeto no VisualStudio e aguarde o browser abra a janela: 
	https://localhost:5001/swagger
2. Abra o Postman e faça uma requisição **Get** com o link:
	https://localhost:5001/v1/imoveis?idportal=zap
3. A imagem abaixo mostra o resultado da response:

	![resultados-Postman](imagens/resultados-Postman.jpg?raw=true "")

<br>
