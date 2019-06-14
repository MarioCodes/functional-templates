package es.msanchez.templates.spring.utilities;

import org.springframework.context.annotation.AnnotationConfigApplicationContext;

public class SpringRegister {

	/**
	 * Initializes a single Spring Context instance for our Application and register a Spring config class.
	 *
	 * @param configClass 
	 * @return
	 */
	public AnnotationConfigApplicationContext initSpringApplicationContext(
			final Class<?> configClass) {
		final AnnotationConfigApplicationContext springContext = new AnnotationConfigApplicationContext();
		springContext.register(configClass);
		springContext.refresh();
		return springContext;
	}

}
