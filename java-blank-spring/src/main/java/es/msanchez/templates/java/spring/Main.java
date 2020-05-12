package es.msanchez.templates.java.spring;

import es.msanchez.templates.java.spring.config.SpringConfig;
import es.msanchez.templates.java.spring.entity.Person;
import es.msanchez.templates.java.spring.utilities.SpringRegister;
import lombok.extern.slf4j.Slf4j;
import org.springframework.context.annotation.AnnotationConfigApplicationContext;

@Slf4j
public class Main {

    public static void main(String[] args) {
        final AnnotationConfigApplicationContext context = prepareSpring();
        final Person person = context.getBean(Person.class);
    }

    private static AnnotationConfigApplicationContext prepareSpring() {
        final SpringRegister register = new SpringRegister();
        return register.initSpringApplicationContext(SpringConfig.class);
    }

}
