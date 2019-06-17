package es.msanchez.frameworks.spring.boot.dao

import es.msanchez.frameworks.spring.boot.entity.Hobby
import org.springframework.stereotype.Repository

@Repository
interface HobbyDao : RawDao<Hobby>