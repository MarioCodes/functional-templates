package es.msanchez.frameworks.spring.boot.service

import es.msanchez.frameworks.spring.boot.dao.PersonDao
import es.msanchez.frameworks.spring.boot.entity.Person
import org.springframework.stereotype.Service

@Service
class PersonService(private val personDao: PersonDao) {

    fun isValid(name: String): Boolean {
        val person = this.personDao.findOneByName(name)
        return !person.isPresent
    }

    fun save(person: Person) {
        this.personDao.save(person)
    }

}