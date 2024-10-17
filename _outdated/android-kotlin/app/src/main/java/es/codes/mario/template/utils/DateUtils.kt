package es.codes.mario.template.utils

import java.text.SimpleDateFormat
import java.util.*

class DateUtils {

    private val dayAndMonth = "dd/MM"
    private val fullDate = "dd/MM/yyyy"

    fun epochToDayMonth(value: Long): String {
        val date = Calendar.getInstance().apply {
            timeInMillis = value
        }.time
        return SimpleDateFormat(dayAndMonth, Locale.getDefault()).format(date)
    }

}