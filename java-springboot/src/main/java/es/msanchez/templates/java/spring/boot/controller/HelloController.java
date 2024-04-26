package es.msanchez.templates.java.spring.boot.controller;

import es.msanchez.templates.java.spring.boot.service.IHelloService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class HelloController {

  @Autowired
  private IHelloService helloService;

  @RequestMapping("/")
  public String index() {
    return helloService.GetGreetings();
  }

}
