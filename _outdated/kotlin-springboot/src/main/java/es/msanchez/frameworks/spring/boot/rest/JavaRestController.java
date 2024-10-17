package es.msanchez.frameworks.spring.boot.rest;

import es.msanchez.frameworks.spring.boot.validator.JavaValidator;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/java")
public class JavaRestController {

    private final JavaValidator javaValidator;

    @Autowired
    public JavaRestController(final JavaValidator javaValidator) {
        this.javaValidator = javaValidator;
    }

    @GetMapping
    public String index() {
        return this.javaValidator.validate();
    }

}
