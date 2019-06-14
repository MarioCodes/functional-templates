package es.msanchez.templates.spring.beans

import org.springframework.stereotype.Component

@Component
class KotlinBean {

    override fun toString(): String {
        return "This is a Kotlin Bean"
    }

}
