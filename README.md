# OperationsAPIs
![image](https://github.com/user-attachments/assets/8b0c0e55-f9bb-4dfd-aca3-8370da18b718)

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
