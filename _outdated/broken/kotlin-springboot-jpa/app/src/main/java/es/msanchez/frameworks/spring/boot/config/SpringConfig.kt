package es.msanchez.frameworks.spring.boot.config

import org.springframework.context.annotation.ComponentScan
import org.springframework.context.annotation.Configuration

@Configuration
@ComponentScan(basePackages = ["es.msanchez.frameworks.spring.**.*"])
class SpringConfig