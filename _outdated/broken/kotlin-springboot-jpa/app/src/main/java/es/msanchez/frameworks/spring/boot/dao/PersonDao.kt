package es.msanchez.frameworks.spring.boot.dao

import es.msanchez.frameworks.spring.boot.entity.Person
import org.springframework.data.jpa.repository.Query
import org.springframework.data.repository.query.Param
import org.springframework.stereotype.Repository
import java.util.*

@Repository
interface PersonDao : RawDao<Person> {

    @Query("SELECT p FROM Person p WHERE p.name = :name")
    fun findOneByName(@Param("name") name: String): Optional<Person>

    fun findAllByAge(age: Int): List<Person>

}