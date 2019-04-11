package es.msanchez.frameworks.spring.boot.controller

import org.springframework.web.bind.annotation.RequestMapping
import org.springframework.web.bind.annotation.RestController

@RestController
class KotlinController {

    @RequestMapping("/kotlin")
    fun index(): String {
        return "This is a Kotlin Bean"
    }

}
