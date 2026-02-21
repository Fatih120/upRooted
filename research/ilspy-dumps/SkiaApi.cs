// SkiaSharp, Version=2.88.0.0, Culture=neutral, PublicKeyToken=0738eb9f132ed756
// SkiaSharp.SkiaApi
using System;
using System.Runtime.InteropServices;
using SkiaSharp;

internal class SkiaApi
{
	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void gr_backendrendertarget_delete(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr gr_backendrendertarget_new_gl(int P_0, int P_1, int P_2, int P_3, GRGlFramebufferInfo* P_4);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr gr_backendrendertarget_new_vulkan(int P_0, int P_1, int P_2, GRVkImageInfo* P_3);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void gr_backendtexture_delete(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr gr_backendtexture_new_gl(int P_0, int P_1, [MarshalAs(UnmanagedType.I1)] bool P_2, GRGlTextureInfo* P_3);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr gr_backendtexture_new_vulkan(int P_0, int P_1, GRVkImageInfo* P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void gr_direct_context_abandon_context(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void gr_direct_context_flush(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void gr_direct_context_flush_and_submit(IntPtr P_0, [MarshalAs(UnmanagedType.I1)] bool P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr gr_direct_context_make_gl(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr gr_direct_context_make_gl_with_options(IntPtr P_0, GRContextOptionsNative* P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr gr_direct_context_make_vulkan(GRVkBackendContextNative P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr gr_direct_context_make_vulkan_with_options(GRVkBackendContextNative P_0, GRContextOptionsNative* P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void gr_direct_context_release_resources_and_abandon_context(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void gr_direct_context_reset_context(IntPtr P_0, uint P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void gr_direct_context_set_resource_cache_limit(IntPtr P_0, IntPtr P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr gr_glinterface_assemble_gl_interface(void* P_0, GRGlGetProcProxyDelegate P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr gr_glinterface_assemble_gles_interface(void* P_0, GRGlGetProcProxyDelegate P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr gr_glinterface_create_native_interface();

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern int gr_recording_context_get_max_surface_sample_count_for_color_type(IntPtr P_0, SKColorTypeNative P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_bitmap_destructor(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_bitmap_erase(IntPtr P_0, uint P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_bitmap_get_info(IntPtr P_0, SKImageInfoNative* P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void* sk_bitmap_get_pixels(IntPtr P_0, IntPtr* P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_bitmap_get_row_bytes(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	internal unsafe static extern bool sk_bitmap_install_pixels(IntPtr P_0, SKImageInfoNative* P_1, void* P_2, IntPtr P_3, SKBitmapReleaseProxyDelegate P_4, void* P_5);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_bitmap_make_shader(IntPtr P_0, SKShaderTileMode P_1, SKShaderTileMode P_2, SKMatrix* P_3);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_bitmap_new();

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_bitmap_notify_pixels_changed(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool sk_bitmap_peek_pixels(IntPtr P_0, IntPtr P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_bitmap_set_immutable(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_bitmap_swap(IntPtr P_0, IntPtr P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	internal unsafe static extern bool sk_bitmap_try_alloc_pixels(IntPtr P_0, SKImageInfoNative* P_1, IntPtr P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_canvas_clear(IntPtr P_0, uint P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_canvas_clip_path_with_operation(IntPtr P_0, IntPtr P_1, SKClipOperation P_2, [MarshalAs(UnmanagedType.I1)] bool P_3);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_canvas_clip_rect_with_operation(IntPtr P_0, SKRect* P_1, SKClipOperation P_2, [MarshalAs(UnmanagedType.I1)] bool P_3);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_canvas_clip_region(IntPtr P_0, IntPtr P_1, SKClipOperation P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_canvas_clip_rrect_with_operation(IntPtr P_0, IntPtr P_1, SKClipOperation P_2, [MarshalAs(UnmanagedType.I1)] bool P_3);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_canvas_destroy(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_canvas_draw_drrect(IntPtr P_0, IntPtr P_1, IntPtr P_2, IntPtr P_3);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_canvas_draw_image_rect(IntPtr P_0, IntPtr P_1, SKRect* P_2, SKRect* P_3, IntPtr P_4);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_canvas_draw_line(IntPtr P_0, float P_1, float P_2, float P_3, float P_4, IntPtr P_5);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_canvas_draw_paint(IntPtr P_0, IntPtr P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_canvas_draw_path(IntPtr P_0, IntPtr P_1, IntPtr P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_canvas_draw_picture(IntPtr P_0, IntPtr P_1, SKMatrix* P_2, IntPtr P_3);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_canvas_draw_rect(IntPtr P_0, SKRect* P_1, IntPtr P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_canvas_draw_region(IntPtr P_0, IntPtr P_1, IntPtr P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_canvas_draw_rrect(IntPtr P_0, IntPtr P_1, IntPtr P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_canvas_draw_text_blob(IntPtr P_0, IntPtr P_1, float P_2, float P_3, IntPtr P_4);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_canvas_flush(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_canvas_get_total_matrix(IntPtr P_0, SKMatrix* P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_canvas_new_from_bitmap(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_canvas_reset_matrix(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_canvas_restore(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_canvas_restore_to_count(IntPtr P_0, int P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern int sk_canvas_save(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern int sk_canvas_save_layer(IntPtr P_0, SKRect* P_1, IntPtr P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_canvas_scale(IntPtr P_0, float P_1, float P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_canvas_set_matrix(IntPtr P_0, SKMatrix* P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_codec_destroy(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern int sk_codec_get_frame_count(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_codec_get_frame_info(IntPtr P_0, SKCodecFrameInfo* P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_codec_get_info(IntPtr P_0, SKImageInfoNative* P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern SKCodecResult sk_codec_get_pixels(IntPtr P_0, SKImageInfoNative* P_1, void* P_2, IntPtr P_3, SKCodecOptionsInternal* P_4);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern int sk_codec_get_repetition_count(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_codec_get_scaled_dimensions(IntPtr P_0, float P_1, SKSizeI* P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_codec_min_buffered_bytes_needed();

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_codec_new_from_data(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_codec_new_from_stream(IntPtr P_0, SKCodecResult* P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_colorfilter_new_color_matrix(float* P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_colorfilter_new_luma_color();

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_colorfilter_new_mode(uint P_0, SKBlendMode P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_colorfilter_new_table_argb(byte* P_0, byte* P_1, byte* P_2, byte* P_3);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_color4f_from_color(uint P_0, SKColorF* P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern uint sk_color4f_to_color(SKColorF* P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_colorspace_new_rgb(SKColorSpaceTransferFn* P_0, SKColorSpaceXyz* P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_colorspace_new_srgb();

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_colorspace_new_srgb_linear();

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_colorspace_transfer_fn_named_linear(SKColorSpaceTransferFn* P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_colorspace_transfer_fn_named_srgb(SKColorSpaceTransferFn* P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_colorspace_unref(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_colorspace_xyz_named_srgb(SKColorSpaceXyz* P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void* sk_data_get_data(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_data_get_size(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_data_new_empty();

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_data_new_from_stream(IntPtr P_0, IntPtr P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_data_new_with_copy(void* P_0, IntPtr P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_data_unref(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_font_delete(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern float sk_font_get_metrics(IntPtr P_0, SKFontMetrics* P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool sk_font_get_path(IntPtr P_0, ushort P_1, IntPtr P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_font_get_pos(IntPtr P_0, ushort* P_1, int P_2, SKPoint* P_3, SKPoint* P_4);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern float sk_font_get_scale_x(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern float sk_font_get_size(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern float sk_font_get_skew_x(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_font_get_typeface(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_font_get_widths_bounds(IntPtr P_0, ushort* P_1, int P_2, float* P_3, SKRect* P_4, IntPtr P_5);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool sk_font_is_embolden(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool sk_font_is_subpixel(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_font_measure_text_no_return(IntPtr P_0, void* P_1, IntPtr P_2, SKTextEncoding P_3, SKRect* P_4, IntPtr P_5, float* P_6);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_font_new_with_values(IntPtr P_0, float P_1, float P_2, float P_3);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_font_set_edging(IntPtr P_0, SKFontEdging P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_font_set_embolden(IntPtr P_0, [MarshalAs(UnmanagedType.I1)] bool P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_font_set_hinting(IntPtr P_0, SKFontHinting P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_font_set_linear_metrics(IntPtr P_0, [MarshalAs(UnmanagedType.I1)] bool P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_font_set_size(IntPtr P_0, float P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_font_set_subpixel(IntPtr P_0, [MarshalAs(UnmanagedType.I1)] bool P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_font_set_typeface(IntPtr P_0, IntPtr P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern int sk_font_text_to_glyphs(IntPtr P_0, void* P_1, IntPtr P_2, SKTextEncoding P_3, ushort* P_4, int P_5);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern ushort sk_font_unichar_to_glyph(IntPtr P_0, int P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_text_utils_get_path(void* P_0, IntPtr P_1, SKTextEncoding P_2, float P_3, float P_4, IntPtr P_5, IntPtr P_6);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern SKColorTypeNative sk_colortype_get_default_8888();

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_refcnt_safe_unref(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern int sk_version_get_increment();

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern int sk_version_get_milestone();

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_image_encode(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_image_encode_specific(IntPtr P_0, SKEncodedImageFormat P_1, int P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern int sk_image_get_height(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern int sk_image_get_width(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_image_make_shader(IntPtr P_0, SKShaderTileMode P_1, SKShaderTileMode P_2, SKMatrix* P_3);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_image_new_from_bitmap(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_image_new_from_encoded(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_image_new_from_texture(IntPtr P_0, IntPtr P_1, GRSurfaceOrigin P_2, SKColorTypeNative P_3, SKAlphaType P_4, IntPtr P_5, SKImageTextureReleaseProxyDelegate P_6, void* P_7);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_image_new_raster(IntPtr P_0, SKImageRasterReleaseProxyDelegate P_1, void* P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	internal unsafe static extern bool sk_image_read_pixels(IntPtr P_0, SKImageInfoNative* P_1, void* P_2, IntPtr P_3, int P_4, int P_5, SKImageCachingHint P_6);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool sk_image_scale_pixels(IntPtr P_0, IntPtr P_1, SKFilterQuality P_2, SKImageCachingHint P_3);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_imagefilter_croprect_destructor(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_imagefilter_croprect_new_with_rect(SKRect* P_0, uint P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_imagefilter_new_arithmetic(float P_0, float P_1, float P_2, float P_3, [MarshalAs(UnmanagedType.I1)] bool P_4, IntPtr P_5, IntPtr P_6, IntPtr P_7);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_imagefilter_new_blur(float P_0, float P_1, SKShaderTileMode P_2, IntPtr P_3, IntPtr P_4);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_imagefilter_new_color_filter(IntPtr P_0, IntPtr P_1, IntPtr P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_imagefilter_new_dilate(float P_0, float P_1, IntPtr P_2, IntPtr P_3);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_imagefilter_new_displacement_map_effect(SKColorChannel P_0, SKColorChannel P_1, float P_2, IntPtr P_3, IntPtr P_4, IntPtr P_5);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_imagefilter_new_distant_lit_diffuse(SKPoint3* P_0, uint P_1, float P_2, float P_3, IntPtr P_4, IntPtr P_5);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_imagefilter_new_distant_lit_specular(SKPoint3* P_0, uint P_1, float P_2, float P_3, float P_4, IntPtr P_5, IntPtr P_6);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_imagefilter_new_drop_shadow(float P_0, float P_1, float P_2, float P_3, uint P_4, IntPtr P_5, IntPtr P_6);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_imagefilter_new_erode(float P_0, float P_1, IntPtr P_2, IntPtr P_3);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_imagefilter_new_image_source(IntPtr P_0, SKRect* P_1, SKRect* P_2, SKFilterQuality P_3);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_imagefilter_new_matrix_convolution(SKSizeI* P_0, float* P_1, float P_2, float P_3, SKPointI* P_4, SKShaderTileMode P_5, [MarshalAs(UnmanagedType.I1)] bool P_6, IntPtr P_7, IntPtr P_8);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_imagefilter_new_merge(IntPtr* P_0, int P_1, IntPtr P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_imagefilter_new_offset(float P_0, float P_1, IntPtr P_2, IntPtr P_3);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_imagefilter_new_paint(IntPtr P_0, IntPtr P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_imagefilter_new_picture_with_croprect(IntPtr P_0, SKRect* P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_imagefilter_new_point_lit_diffuse(SKPoint3* P_0, uint P_1, float P_2, float P_3, IntPtr P_4, IntPtr P_5);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_imagefilter_new_point_lit_specular(SKPoint3* P_0, uint P_1, float P_2, float P_3, float P_4, IntPtr P_5, IntPtr P_6);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_imagefilter_new_spot_lit_diffuse(SKPoint3* P_0, SKPoint3* P_1, float P_2, float P_3, uint P_4, float P_5, float P_6, IntPtr P_7, IntPtr P_8);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_imagefilter_new_spot_lit_specular(SKPoint3* P_0, SKPoint3* P_1, float P_2, float P_3, uint P_4, float P_5, float P_6, float P_7, IntPtr P_8, IntPtr P_9);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_imagefilter_new_tile(SKRect* P_0, SKRect* P_1, IntPtr P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_imagefilter_new_xfermode(SKBlendMode P_0, IntPtr P_1, IntPtr P_2, IntPtr P_3);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_matrix_concat(SKMatrix* P_0, SKMatrix* P_1, SKMatrix* P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_matrix_map_xy(SKMatrix* P_0, float P_1, float P_2, SKPoint* P_3);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_matrix_pre_concat(SKMatrix* P_0, SKMatrix* P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	internal unsafe static extern bool sk_paint_get_fill_path(IntPtr P_0, IntPtr P_1, IntPtr P_2, SKRect* P_3, float P_4);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_paint_get_path_effect(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool sk_paint_is_antialias(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_paint_set_antialias(IntPtr P_0, [MarshalAs(UnmanagedType.I1)] bool P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_paint_set_blendmode(IntPtr P_0, SKBlendMode P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_paint_set_color(IntPtr P_0, uint P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_paint_set_color4f(IntPtr P_0, SKColorF* P_1, IntPtr P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_paint_set_colorfilter(IntPtr P_0, IntPtr P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_paint_set_dither(IntPtr P_0, [MarshalAs(UnmanagedType.I1)] bool P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_paint_set_filter_quality(IntPtr P_0, SKFilterQuality P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_paint_set_imagefilter(IntPtr P_0, IntPtr P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_paint_set_path_effect(IntPtr P_0, IntPtr P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_paint_set_shader(IntPtr P_0, IntPtr P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_paint_set_stroke_cap(IntPtr P_0, SKStrokeCap P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_paint_set_stroke_join(IntPtr P_0, SKStrokeJoin P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_paint_set_stroke_miter(IntPtr P_0, float P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_paint_set_stroke_width(IntPtr P_0, float P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_paint_set_style(IntPtr P_0, SKPaintStyle P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_path_add_circle(IntPtr P_0, float P_1, float P_2, float P_3, SKPathDirection P_4);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_path_add_oval(IntPtr P_0, SKRect* P_1, SKPathDirection P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_path_add_path(IntPtr P_0, IntPtr P_1, SKPathAddMode P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_path_add_poly(IntPtr P_0, SKPoint* P_1, int P_2, [MarshalAs(UnmanagedType.I1)] bool P_3);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_path_add_rect(IntPtr P_0, SKRect* P_1, SKPathDirection P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_path_add_rounded_rect(IntPtr P_0, SKRect* P_1, float P_2, float P_3, SKPathDirection P_4);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_path_arc_to(IntPtr P_0, float P_1, float P_2, float P_3, SKPathArcSize P_4, SKPathDirection P_5, float P_6, float P_7);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_path_clone(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_path_close(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_path_conic_to(IntPtr P_0, float P_1, float P_2, float P_3, float P_4, float P_5);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool sk_path_contains(IntPtr P_0, float P_1, float P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_path_create_iter(IntPtr P_0, int P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_path_create_rawiter(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_path_cubic_to(IntPtr P_0, float P_1, float P_2, float P_3, float P_4, float P_5, float P_6);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_path_delete(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern SKPathFillType sk_path_get_filltype(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern float sk_path_iter_conic_weight(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_path_iter_destroy(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern SKPathVerb sk_path_iter_next(IntPtr P_0, SKPoint* P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_path_line_to(IntPtr P_0, float P_1, float P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_path_move_to(IntPtr P_0, float P_1, float P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_path_new();

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_path_quad_to(IntPtr P_0, float P_1, float P_2, float P_3, float P_4);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_path_rawiter_destroy(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern SKPathVerb sk_path_rawiter_next(IntPtr P_0, SKPoint* P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_path_set_filltype(IntPtr P_0, SKPathFillType P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_path_transform(IntPtr P_0, SKMatrix* P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_pathmeasure_destroy(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern float sk_pathmeasure_get_length(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	internal unsafe static extern bool sk_pathmeasure_get_pos_tan(IntPtr P_0, float P_1, SKPoint* P_2, SKPoint* P_3);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_pathmeasure_new_with_path(IntPtr P_0, [MarshalAs(UnmanagedType.I1)] bool P_1, float P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool sk_pathop_op(IntPtr P_0, IntPtr P_1, SKPathOp P_2, IntPtr P_3);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	internal unsafe static extern bool sk_pathop_tight_bounds(IntPtr P_0, SKRect* P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_path_effect_create_dash(float* P_0, int P_1, float P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_picture_get_cull_rect(IntPtr P_0, SKRect* P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_picture_make_shader(IntPtr P_0, SKShaderTileMode P_1, SKShaderTileMode P_2, SKMatrix* P_3, SKRect* P_4);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_picture_recorder_begin_recording(IntPtr P_0, SKRect* P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_picture_recorder_delete(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_picture_recorder_end_recording(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_picture_recorder_new();

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_color_get_bit_shift(int* P_0, int* P_1, int* P_2, int* P_3);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_pixmap_destructor(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_pixmap_get_info(IntPtr P_0, SKImageInfoNative* P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_pixmap_new();

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_pixmap_new_with_params(SKImageInfoNative* P_0, void* P_1, IntPtr P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool sk_pixmap_scale_pixels(IntPtr P_0, IntPtr P_1, SKFilterQuality P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_region_delete(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_region_get_bounds(IntPtr P_0, SKRectI* P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	internal unsafe static extern bool sk_region_intersects_rect(IntPtr P_0, SKRectI* P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool sk_region_is_empty(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_region_new();

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	internal unsafe static extern bool sk_region_op_rect(IntPtr P_0, SKRectI* P_1, SKRegionOperation P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool sk_region_set_empty(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_rrect_delete(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_rrect_get_radii(IntPtr P_0, SKRoundRectCorner P_1, SKPoint* P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_rrect_get_rect(IntPtr P_0, SKRect* P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_rrect_inset(IntPtr P_0, float P_1, float P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_rrect_new();

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_rrect_outset(IntPtr P_0, float P_1, float P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_rrect_set_empty(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_rrect_set_rect(IntPtr P_0, SKRect* P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_rrect_set_rect_radii(IntPtr P_0, SKRect* P_1, SKPoint* P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_shader_new_blend(SKBlendMode P_0, IntPtr P_1, IntPtr P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_shader_new_color(uint P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_shader_new_color4f(SKColorF* P_0, IntPtr P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_shader_new_linear_gradient(SKPoint* P_0, uint* P_1, float* P_2, int P_3, SKShaderTileMode P_4, SKMatrix* P_5);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_shader_new_linear_gradient_color4f(SKPoint* P_0, SKColorF* P_1, IntPtr P_2, float* P_3, int P_4, SKShaderTileMode P_5, SKMatrix* P_6);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_shader_new_perlin_noise_fractal_noise(float P_0, float P_1, int P_2, float P_3, SKSizeI* P_4);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_shader_new_perlin_noise_turbulence(float P_0, float P_1, int P_2, float P_3, SKSizeI* P_4);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_shader_new_radial_gradient(SKPoint* P_0, float P_1, uint* P_2, float* P_3, int P_4, SKShaderTileMode P_5, SKMatrix* P_6);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_shader_new_radial_gradient_color4f(SKPoint* P_0, float P_1, SKColorF* P_2, IntPtr P_3, float* P_4, int P_5, SKShaderTileMode P_6, SKMatrix* P_7);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_shader_new_sweep_gradient(SKPoint* P_0, uint* P_1, float* P_2, int P_3, SKShaderTileMode P_4, float P_5, float P_6, SKMatrix* P_7);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_shader_new_two_point_conical_gradient(SKPoint* P_0, float P_1, SKPoint* P_2, float P_3, uint* P_4, float* P_5, int P_6, SKShaderTileMode P_7, SKMatrix* P_8);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_shader_new_two_point_conical_gradient_color4f(SKPoint* P_0, float P_1, SKPoint* P_2, float P_3, SKColorF* P_4, IntPtr P_5, float* P_6, int P_7, SKShaderTileMode P_8, SKMatrix* P_9);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_dynamicmemorywstream_destroy(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_dynamicmemorywstream_detach_as_stream(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_dynamicmemorywstream_new();

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_stream_asset_destroy(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_stream_get_length(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_stream_get_position(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool sk_stream_has_length(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool sk_stream_has_position(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool sk_stream_is_at_end(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_stream_read(IntPtr P_0, void* P_1, IntPtr P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool sk_stream_seek(IntPtr P_0, IntPtr P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_wstream_flush(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	internal unsafe static extern bool sk_wstream_write(IntPtr P_0, void* P_1, IntPtr P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_string_destructor(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void* sk_string_get_c_str(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_string_get_size(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_string_new_empty();

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_surface_draw(IntPtr P_0, IntPtr P_1, float P_2, float P_3, IntPtr P_4);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_surface_flush(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_surface_flush_and_submit(IntPtr P_0, [MarshalAs(UnmanagedType.I1)] bool P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_surface_get_canvas(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_surface_new_backend_render_target(IntPtr P_0, IntPtr P_1, GRSurfaceOrigin P_2, SKColorTypeNative P_3, IntPtr P_4, IntPtr P_5);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_surface_new_backend_texture(IntPtr P_0, IntPtr P_1, GRSurfaceOrigin P_2, int P_3, SKColorTypeNative P_4, IntPtr P_5, IntPtr P_6);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_surface_new_image_snapshot(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_surface_new_raster(SKImageInfoNative* P_0, IntPtr P_1, IntPtr P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_surface_new_raster_direct(SKImageInfoNative* P_0, void* P_1, IntPtr P_2, SKSurfaceRasterReleaseProxyDelegate P_3, void* P_4, IntPtr P_5);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_surface_new_render_target(IntPtr P_0, [MarshalAs(UnmanagedType.I1)] bool P_1, SKImageInfoNative* P_2, int P_3, GRSurfaceOrigin P_4, IntPtr P_5, [MarshalAs(UnmanagedType.I1)] bool P_6);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_surfaceprops_delete(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_surfaceprops_new(uint P_0, SKPixelGeometry P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_textblob_builder_alloc_run_pos(IntPtr P_0, IntPtr P_1, int P_2, SKRect* P_3, SKRunBufferInternal* P_4);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern void sk_textblob_builder_alloc_run_rsxform(IntPtr P_0, IntPtr P_1, int P_2, SKRunBufferInternal* P_3);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_textblob_builder_delete(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_textblob_builder_make(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_textblob_builder_new();

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern int sk_textblob_get_intercepts(IntPtr P_0, float* P_1, float* P_2, IntPtr P_3);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_textblob_unref(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern int sk_fontmgr_count_families(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_fontmgr_create_default();

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_fontmgr_get_family_name(IntPtr P_0, int P_1, IntPtr P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_fontmgr_match_family(IntPtr P_0, IntPtr P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_fontmgr_match_family_style(IntPtr P_0, IntPtr P_1, IntPtr P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_fontmgr_match_family_style_character(IntPtr P_0, IntPtr P_1, IntPtr P_2, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr)] string[] P_3, int P_4, int P_5);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_fontmgr_ref_default();

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_fontstyle_delete(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern SKFontStyleSlant sk_fontstyle_get_slant(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern int sk_fontstyle_get_weight(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern int sk_fontstyle_get_width(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_fontstyle_new(int P_0, int P_1, SKFontStyleSlant P_2);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern int sk_fontstyleset_get_count(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_fontstyleset_get_style(IntPtr P_0, int P_1, IntPtr P_2, IntPtr P_3);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern int sk_typeface_count_glyphs(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_typeface_create_from_name(IntPtr P_0, IntPtr P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_typeface_create_from_stream(IntPtr P_0, int P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_typeface_get_family_name(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern SKFontStyleSlant sk_typeface_get_font_slant(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern int sk_typeface_get_font_weight(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern int sk_typeface_get_font_width(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_typeface_get_fontstyle(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_typeface_get_table_data(IntPtr P_0, uint P_1, IntPtr P_2, IntPtr P_3, void* P_4);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_typeface_get_table_size(IntPtr P_0, uint P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern int sk_typeface_get_units_per_em(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	[return: MarshalAs(UnmanagedType.I1)]
	internal static extern bool sk_typeface_is_fixed_pitch(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_typeface_open_stream(IntPtr P_0, int* P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_typeface_ref_default();

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_compatpaint_delete(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_compatpaint_get_font(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern SKTextAlign sk_compatpaint_get_text_align(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_compatpaint_make_font(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern IntPtr sk_compatpaint_new();

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_compatpaint_reset(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_compatpaint_set_text_align(IntPtr P_0, SKTextAlign P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_compatpaint_set_text_encoding(IntPtr P_0, SKTextEncoding P_1);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_managedstream_destroy(IntPtr P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal unsafe static extern IntPtr sk_managedstream_new(void* P_0);

	[DllImport("libSkiaSharp", CallingConvention = CallingConvention.Cdecl)]
	internal static extern void sk_managedstream_set_procs(SKManagedStreamDelegates P_0);
}

