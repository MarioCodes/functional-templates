package es.msanchez.templates.spring.utilities

import org.springframework.context.annotation.AnnotationConfigApplicationContext

class SpringRegister {

    /**
     * Initializes a single Spring Context instance for our Application and register a Spring config class.
     *
     * @param configClass -
     * @return -
     */
    fun initSpringApplicationContext(configClass: Class<*>): AnnotationConfigApplicationContext {
        val springContext = AnnotationConfigApplicationContext()
        springContext.register(configClass)
        springContext.refresh()
        return springContext
    }
}