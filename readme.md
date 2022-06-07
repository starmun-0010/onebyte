# OneByte Assignment

The application implementes a RESTFul WebApi over a basic Patients' Visits to Doctors schema.

## Run Locally
To run the app by itself execute the following:

```
$ docker build https://github.com/starmun-0010/onebyte.git#main -t "onbyte:v1"
$ docker run -e "ElasticSearch__Url=your_elastic_search_url" -e "ConnectionString__OneByteDatabase=postgres_database connection_string" "Jwt__Key=jwt_secret_key(32 character minimum)" -e "Jwt__Audience=jwt_audience" -e "Jwt__Issuer=jwt_issuer" onebyte:v1 
```
If you do not have a postgres database pre-configured, there is a docker compose configuration to launch a postgres image along with the app:

```
$ git clone https://github.com/starmun-0010/onebyte.git
$ cd onebyte
$ docker compose build
$ docker compose up
```
Note: the docker compose coniguration requires you to provide environment variables, the easiest way to provide the variables is via a .env file in the same directory as the docker-compose.yaml
### Example .env file:
```
POSTGRES_USER=postgres
POSTGRES_PASSWORD=admin
POSTGRES_DB=onebyte
ELASTICSEARCH_URL=http://localhost:9200
JWT_KEY="kIoAeLFFzuJwJDGTfv4BBQg0QMQfkFH7ALBaOFrBu528ixZBb3bJ6mbRansUgoD"
JWT_AUDIENCE=OneByte
JWT_ISSUER=OneByte
```
If you want to run Kabana and ElasticSearch with default configurations execute the following command:
```
$ docker compose -f docker-compose-elastic.yaml up
```

