Configurated templates to start a project directly out of the box. All of them contain all common Frameworks that I use to develop. The difference resides at big Frameworks such as Spring or Vertx, which modify the main structure's usage.

## How-to start
### Vertx template
```  
mvn clean install  
java -jar [name]-fat.jar -cluster
```  

### Rest of them
```  
mvn clean install
java -jar [name].jar  
```
