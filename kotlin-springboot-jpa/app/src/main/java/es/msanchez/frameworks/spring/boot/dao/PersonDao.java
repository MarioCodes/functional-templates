package es.msanchez.frameworks.spring.boot.dao;

import es.msanchez.frameworks.spring.boot.entity.Person;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Optional;

@Repository
public interface PersonDao extends RawDao<Person> {

    @Query("SELECT p FROM Person p WHERE p.name = :name")
    Optional<Person> findOneByName(@Param("name") final String name);

    List<Person> findAllByAge(final Integer age);

}
