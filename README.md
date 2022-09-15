# Teste Consumo e Filas Rabbit

![GitHub repo size](https://img.shields.io/github/repo-size/conradocabelo/rabbit-file-test?style=for-the-badge)
![GitHub language count](https://img.shields.io/github/languages/count/conradocabelo/rabbit-file-test?style=for-the-badge)
![GitHub forks](https://img.shields.io/github/forks/conradocabelo/rabbit-file-test?style=for-the-badge)
![Bitbucket open issues](https://img.shields.io/github/issues/conradocabelo/rabbit-file-test?style=for-the-badge)

>  Apliccação WebAPI responsavel por consumir uma fila do RabbitMQ e disponibilizar os dados por um Endpoint GET tambem fornecendo dados ao agent telegraf para persistencia em base de dados Influx.

## 💻 Pré-requisitos

Antes de começar, verifique se você atendeu aos seguintes requisitos:
*  Visual Studio 2022 ou VSCode 
* .Net 6 
* Docker e Docker-Compose

## 🚀 Subindo a Aplicação
### Configurando a aplicação

* Para alterar o camninho do RabbitMQ pode ser alterado passado como variavel de ambiente pelo docker, porem o mesmo ja esta configurado para acessar o rabbit pelo nome do serviço, na rede do docker.

### Configurando Telegraf/Influx

* Ao subir a aplicação criar os dados de acesso para o InfluxDB no caminho http://localhost:8086/
![image](https://user-images.githubusercontent.com/79751069/190397417-3d7d0590-d129-42e5-8dbb-f0f72c8f457f.png)

* Depois criar os dados de acesso do telegraf
![image](https://user-images.githubusercontent.com/79751069/190397715-63fde3a5-327a-4b44-b16a-cc9cae2c2cbb.png)

* Com os dados criados configurar o arquivo telegraft.conf na pasta onde se encontra o docker-compose  fornecendo (token, organization e bucket)
![image](https://user-images.githubusercontent.com/79751069/190398333-fbd8b4c3-410e-4c5d-8baf-f4055988ad0d.png)

* Após estes passos reiniciar o container do telegraf.
