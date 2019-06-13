package es.msanchez.frameworks.java.spring.boot.dao;

import es.msanchez.frameworks.java.spring.boot.entity.Person;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

import java.util.List;
import java.util.Optional;

public interface PersonDao extends RawDao<Person> {

    @Query("SELECT p FROM Person p WHERE p.name = :name")
    Optional<Person> findOneByName(@Param("name") final String name);

    List<Person> findAllByAge(final Integer age);

}
