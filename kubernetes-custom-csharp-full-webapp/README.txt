# Readme
## Build steps
### Docker
In Visual Studio you can automatically build a Dockerfile from a project.

To verify its correct go to the folder where the `Dockerfile` is generated and run the following command. 
~~~ bash
docker build -t k8s-csharp-backend -f Dockerfile .
~~~
* `-f` gives it a tag to reference to it later on. 

Apply the file:
~~~ bash
kubectl apply -f backend.yaml
~~~

An error downloading the image may happen here with minikube. See this:
*(append link to file: errores imagenes minikube)*

Some error may happen with VirtualBox to run this at Minikube. If this happens the cluster is set to NodePort. Run the following command to get its public ip and then access the *service* through the public IP:

*see the service IP and port:*
~~~ bash
kubectl get svc
~~~

*if the previous doesn't work, see public cluster information. Use this URL with port from the service*.
~~~ bash
kubectl cluster-info
~~~