package es.msanchez.frameworks.spring.boot.controller

import es.msanchez.frameworks.spring.boot.validator.KotlinValidator
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.web.bind.annotation.GetMapping
import org.springframework.web.bind.annotation.RequestMapping
import org.springframework.web.bind.annotation.RestController

@RestController("/kotlin")
class KotlinRestController(private val kotlinValidator: KotlinValidator) {

    @GetMapping
    fun index(): String {
        return kotlinValidator.validate()
    }

}
