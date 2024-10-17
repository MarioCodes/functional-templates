package es.msanchez.fullstack.kotlin.exception

class DataTransferException(message: String,
                            cause: Throwable) : RuntimeException(message) {
    constructor(message: String) : this(message, cause = Throwable())
}