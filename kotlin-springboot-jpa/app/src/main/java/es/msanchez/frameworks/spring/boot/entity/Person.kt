package es.msanchez.frameworks.spring.boot.entity

import javax.persistence.*

@Entity
@Table(name = "person")
data class Person(
        @Id
        @GeneratedValue(strategy = GenerationType.IDENTITY)
        val id: Long? = null,
        var name: String,
        var age: Int? = null,

        @ManyToMany(cascade = [CascadeType.PERSIST])
        var hobbies: List<Hobby>
) {
    constructor() : this(name = "",
            hobbies = emptyList())
}