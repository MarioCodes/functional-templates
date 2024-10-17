package es.codes.mario.template.view

import android.content.Context
import android.content.res.TypedArray
import android.util.AttributeSet
import android.view.View
import android.widget.LinearLayout
import android.widget.TextView
import es.codes.mario.template.R

class EntryView : LinearLayout {

    /*
        It's possible to change this either programmatically, through the setters at this class, or on XML declaration
     */
    lateinit var textViewValueTitle: TextView
    lateinit var textViewValueContent: TextView

    lateinit var textViewDateContent: TextView

    /**
     * This constructor is needed to inflate a custom view per XML.
     */
    constructor(context: Context, attrs: AttributeSet) : super(context, attrs) {
        initialize(context, attrs)
    }

    /**
     * This constructor is used to inflate the view programmatically.
     */
    constructor(context: Context) : super(context) {
        initialize(context)
    }

    private fun initialize(context: Context, attrs: AttributeSet) {
        View.inflate(context, R.layout.single_entry_example, this)

        val sets = listOf(R.attr.entry_title)
        val typedArray: TypedArray = context.obtainStyledAttributes(attrs, sets.toIntArray())

        val entryTitle = typedArray.getText(0)
        typedArray.recycle()

        initComponentsToModify()

        textViewValueTitle.text = entryTitle
    }

    private fun initialize(context: Context) {
        View.inflate(context, R.layout.single_entry_example, this)

        val sets = listOf(R.attr.entry_title)
        val typedArray: TypedArray = context.obtainStyledAttributes(sets.toIntArray())

        val entryTitle = typedArray.getText(0)
        typedArray.recycle()

        initComponentsToModify()

        textViewValueTitle.text = entryTitle
    }

    private fun initComponentsToModify() {
        textViewValueTitle = findViewById(R.id.textViewValueTitle)
        textViewValueContent = findViewById(R.id.textViewValueContent)
        textViewDateContent = findViewById(R.id.textViewDateContent)
    }

    fun setTitle(value: String) {
        textViewValueTitle.text = value
    }

    fun setContent(value: String) {
        textViewValueContent.text = value
    }

    fun setDate(value: String) {
        textViewDateContent.text = value
    }

}