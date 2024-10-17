package es.msanchez.templates.spring;

import es.msanchez.templates.spring.beans.JavaBean;
import es.msanchez.templates.spring.beans.KotlinBean;
import es.msanchez.templates.spring.config.SpringConfig;
import es.msanchez.templates.spring.utilities.SpringRegister;
import lombok.extern.slf4j.Slf4j;
import org.springframework.context.annotation.AnnotationConfigApplicationContext;

@Slf4j
public class Main {

    public static void main(final String[] args) {
        log.info("Application started");
        final AnnotationConfigApplicationContext context = prepareSpring();
        log.info("Configuration is ready");

        final JavaBean javaBean = context.getBean(JavaBean.class);
        log.info(javaBean.toString());

        final KotlinBean kotlinBean = context.getBean(KotlinBean.class);
        log.info(kotlinBean.toString());

        log.info("Application is done");
    }

    private static AnnotationConfigApplicationContext prepareSpring() {
        final SpringRegister register = new SpringRegister();
        return register.initSpringApplicationContext(SpringConfig.class);
    }

}
