# docker-compose-examples
Working examples for `docker-compose.yml` files

## Why
There're a lot of online examples for `DockerFiles` but not that much for `docker-compose` files, which results in reaaaally long start commands.

## Documentation
The commands needed to run the images, are located within each folder.  

### java
Uses a multi-stage image which builds upon a maven image and then deploys the resultant `.jar` into a Java image.  

### jekyll
Template blog created with `jekyll new website`.  

The `build.sh` script builds the image from `docker-compose.yml` and `DockerFile`, but does not start the container. This is useful to deploy into `DockerHub` when all changes are ready.  

`development.sh` script builds the image from cache and starts a container with `--watch` option. This is useful to write posts, as it will automatically reload all changes on `.md` files. 

### mysql & mongo
Official images, without ini script. Initializes a blank database.
