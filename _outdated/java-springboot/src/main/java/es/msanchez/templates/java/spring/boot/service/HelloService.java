package es.msanchez.templates.java.spring.boot.service;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

@Service
public class HelloService implements IHelloService {

    @Value("${custom.property}")
    private String customProperty;

    @Override
    public String GetGreetings() {
        return String.format("Hello there %s from an autowired class!", customProperty);
    }

}
