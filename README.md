# OperationsAPIs
![image](https://github.com/user-attachments/assets/926aa412-453c-43f2-baac-35f293f10ead)

A Aplicação consiste em 2 serviços e é necessário ter o Docker instalado na máquina:

Serviços:

- Operations.API => Pode inserir operações de débito e de crédito, com banco de dados In-Memory.

   Swagger:
    https://localhost:58412/swagger/index.html

   Endpoints:

     https://localhost:58412/addCredit?value=25
  
     https://localhost:58412/addDebit?value=35
   

- Operations.Report.API => Disponibiliza o consolidado do dia das operações de crédito e débito efetuadas pelo serviço Operations.API, com banco de dados MongoDB.

   Obter as operações por dia:
    http://localhost:5422/getoperation 

Para subir a aplicação você deve executar o comando abaixo no diretório "OperationsAPIs":

- docker-compose up

Para desfazer o ambiente você deve executar o comando abaixo no diretório "OperationsAPIs":

- docker-compose down


Considerando alguns ambientes é melhor executar com o Visual Studio Community 2022 e selecionar o projeto do docker-compose como inicial.
