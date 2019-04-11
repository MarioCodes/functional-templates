package es.msanchez.frameworks.spring.boot.controller;

import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
public class HelloController {

  @RequestMapping("/java")
  public String index() {
    return "This is a java bean";
  }

}
