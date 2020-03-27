package es.msanchez.frameworks.java.spring.boot.controller;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/my-java-app")
public class HelloController {

    @GetMapping("hello-world")
    public String index() {
        return "Hello world from Docker!";
    }

}
