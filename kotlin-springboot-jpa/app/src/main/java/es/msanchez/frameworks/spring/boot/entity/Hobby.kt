package es.msanchez.frameworks.spring.boot.entity

import javax.persistence.*

@Entity
@Table(name = "hobby")
class Hobby(
        @Id
        @GeneratedValue(strategy = GenerationType.IDENTITY)
        val id: Long? = null,
        var name: String
) {
    constructor() : this(name = "")
}