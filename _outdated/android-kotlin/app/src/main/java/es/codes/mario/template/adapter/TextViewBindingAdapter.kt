package es.codes.mario.template.adapter

import android.widget.TextView
import androidx.databinding.BindingAdapter
import androidx.databinding.InverseBindingAdapter

/**
 * I need this for DataBinding, to be able to assign directly float values in xml.
 */
class TextViewBindingAdapter {

    @BindingAdapter("android:text")
    fun setText(view: TextView, value: Float?) {
        if (value == null) return
        view.text = value.toString()
    }

    @InverseBindingAdapter(attribute = "android:text", event = "android:textAttrChanged")
    fun getTextString(view: TextView): Float? {
        return java.lang.Float.valueOf(view.text.toString())
    }

}