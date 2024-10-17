package es.codes.mario.template.charts

import android.graphics.Color
import android.graphics.DashPathEffect
import com.androidplot.util.PixelUtils
import com.androidplot.xy.*
import es.codes.mario.template.data.Entry
import java.text.FieldPosition
import java.text.Format
import java.text.ParsePosition
import java.util.stream.Collectors
import kotlin.math.roundToInt


class AndroidPlotChart {

    fun resetData(plot: XYPlot) {
        plot.clear()
        plot.redraw()
    }

    fun loadData(plot: XYPlot, allEntries: MutableList<Entry>) {
        val allEntries = allEntries.stream()
            .map { entry -> entry.index }
            .collect(Collectors.toList())

        // this avoids duplicated labels
        plot.setDomainStep(StepMode.SUBDIVIDE, allEntries.size.toDouble())

        // move to config file.
        plot.title.text = ""
        plot.domainTitle.text = ""
        plot.rangeTitle.text = ""

        // set this configurable.
        // plot.setRangeBoundaries(0, 100, BoundaryMode.FIXED)

        // turn the above arrays into XYSeries':
        // (Y_VALS_ONLY means use the element index as the x value)
        val series1 =
            SimpleXYSeries(allEntries, SimpleXYSeries.ArrayFormat.Y_VALS_ONLY, "Kilograms")

        // create formatters to use for drawing a series using LineAndPointRenderer
        // and configure them from xml:
        val series1Format = LineAndPointFormatter(Color.RED, Color.GREEN, Color.BLUE, null)

        plot.addSeries(series1, series1Format)

        // FORMATTING
        // add an "dash" effect to the series1 line:
        series1Format.linePaint.pathEffect = DashPathEffect(
            floatArrayOf( // always use DP when specifying pixel sizes, to keep things consistent across devices:
                PixelUtils.dpToPix(20f),
                PixelUtils.dpToPix(15f)
            ), 0F
        )

        // this is because you cannot smooth the lines with fewer than 3 entries
        if (allEntries.size >= 3) {
            // just for fun, add some smoothing to the lines:
            // see: http://androidplot.com/smooth-curves-and-androidplot/
            series1Format.interpolationParams =
                CatmullRomInterpolator.Params(10, CatmullRomInterpolator.Type.Centripetal)
        }

        // add a new series' to the xyplot:
        // plot.addSeries(series1, series1Format)

        plot.graph.getLineLabelStyle(XYGraphWidget.Edge.BOTTOM).format = object : Format() {
            override fun format(
                obj: Any,
                toAppendTo: StringBuffer,
                pos: FieldPosition?
            ): StringBuffer? {
                val i = (obj as Number).toFloat().roundToInt()
                return toAppendTo.append(allEntries[i])
            }

            override fun parseObject(source: String?, pos: ParsePosition?): Any? {
                return null
            }
        }
    }

}