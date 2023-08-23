﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

#pragma warning disable 1591
namespace SharpGL
{
    public partial class OpenGL
    {
		#region OpenGL 1.1

	    //   OpenGL Version Identifier
		public const uint GL_VERSION_1_1 = 1;		

	    //  AccumOp
		public const uint GL_ACCUM                          = 0x0100;
		public const uint GL_LOAD                           = 0x0101;
		public const uint GL_RETURN                         = 0x0102;
		public const uint GL_MULT                           = 0x0103;
		public const uint GL_ADD                            = 0x0104;

        //  Alpha functions
		public const uint GL_NEVER                          = 0x0200;
		public const uint GL_LESS                           = 0x0201;
		public const uint GL_EQUAL                          = 0x0202;
		public const uint GL_LEQUAL                         = 0x0203;
		public const uint GL_GREATER                        = 0x0204;
		public const uint GL_NOTEQUAL                       = 0x0205;
		public const uint GL_GEQUAL                         = 0x0206;
		public const uint GL_ALWAYS                         = 0x0207;

	    //  AttribMask
		public const uint GL_CURRENT_BIT                    = 0x00000001;
		public const uint GL_POINT_BIT                      = 0x00000002;
		public const uint GL_LINE_BIT                       = 0x00000004;
		public const uint GL_POLYGON_BIT                    = 0x00000008;
		public const uint GL_POLYGON_STIPPLE_BIT            = 0x00000010;
		public const uint GL_PIXEL_MODE_BIT                 = 0x00000020;
		public const uint GL_LIGHTING_BIT                   = 0x00000040;
		public const uint GL_FOG_BIT                        = 0x00000080;
		public const uint GL_DEPTH_BUFFER_BIT               = 0x00000100;
		public const uint GL_ACCUM_BUFFER_BIT               = 0x00000200;
		public const uint GL_STENCIL_BUFFER_BIT             = 0x00000400;
		public const uint GL_VIEWPORT_BIT                   = 0x00000800;
		public const uint GL_TRANSFORM_BIT                  = 0x00001000;
		public const uint GL_ENABLE_BIT                     = 0x00002000;
		public const uint GL_COLOR_BUFFER_BIT               = 0x00004000;
		public const uint GL_HINT_BIT                       = 0x00008000;
		public const uint GL_EVAL_BIT                       = 0x00010000;
		public const uint GL_LIST_BIT                       = 0x00020000;
		public const uint GL_TEXTURE_BIT                    = 0x00040000;
		public const uint GL_SCISSOR_BIT                    = 0x00080000;
		public const uint GL_ALL_ATTRIB_BITS                = 0x000fffff;

	    //  BeginMode

        /// <summary>
        /// Treats each vertex as a single point. Vertex n defines point n. N points are drawn.
        /// </summary>
		public const uint GL_POINTS                         = 0x0000;

        /// <summary>
        /// Treats each pair of vertices as an independent line segment. Vertices 2n - 1 and 2n define line n. N/2 lines are drawn.
        /// </summary>
		public const uint GL_LINES                          = 0x0001;

        /// <summary>
        /// Draws a connected group of line segments from the first vertex to the last, then back to the first. Vertices n and n + 1 define line n. The last line, however, is defined by vertices N and 1. N lines are drawn.
        /// </summary>
		public const uint GL_LINE_LOOP                      = 0x0002;

        /// <summary>
        /// Draws a connected group of line segments from the first vertex to the last. Vertices n and n+1 define line n. N - 1 lines are drawn.
        /// </summary>
		public const uint GL_LINE_STRIP                     = 0x0003;

        /// <summary>
        /// Treats each triplet of vertices as an independent triangle. Vertices 3n - 2, 3n - 1, and 3n define triangle n. N/3 triangles are drawn.
        /// </summary>
		public const uint GL_TRIANGLES                      = 0x0004;

        /// <summary>
        /// Draws a connected group of triangles. One triangle is defined for each vertex presented after the first two vertices. For odd n, vertices n, n + 1, and n + 2 define triangle n. For even n, vertices n + 1, n, and n + 2 define triangle n. N - 2 triangles are drawn.
        /// </summary>
		public const uint GL_TRIANGLE_STRIP                 = 0x0005;

	    /// <summary>
	    /// Draws a connected group of triangles. one triangle is defined for each vertex presented after the first two vertices. Vertices 1, n + 1, n + 2 define triangle n. N - 2 triangles are drawn.
	    /// </summary>
	    public const uint GL_TRIANGLE_FAN = 0x0006;

        /// <summary>
        /// Treats each group of four vertices as an independent quadrilateral. Vertices 4n - 3, 4n - 2, 4n - 1, and 4n define quadrilateral n. N/4 quadrilaterals are drawn.
        /// </summary>
		public const uint GL_QUADS                          = 0x0007;

        /// <summary>
        /// Draws a connected group of quadrilaterals. One quadrilateral is defined for each pair of vertices presented after the first pair. Vertices 2n - 1, 2n, 2n + 2, and 2n + 1 define quadrilateral n. N/2 - 1 quadrilaterals are drawn. Note that the order in which vertices are used to construct a quadrilateral from strip data is different from that used with independent data.
        /// </summary>
		public const uint GL_QUAD_STRIP                     = 0x0008;

        /// <summary>
        /// Draws a single, convex polygon. Vertices 1 through N define this polygon.
        /// </summary>
		public const uint GL_POLYGON                        = 0x0009;

	    //  BlendingFactorDest
		public const uint GL_ZERO                           = 0;
		public const uint GL_ONE                            = 1;
		public const uint GL_SRC_COLOR                      = 0x0300;
		public const uint GL_ONE_MINUS_SRC_COLOR            = 0x0301;
		public const uint GL_SRC_ALPHA                      = 0x0302;
		public const uint GL_ONE_MINUS_SRC_ALPHA            = 0x0303;
		public const uint GL_DST_ALPHA                      = 0x0304;
		public const uint GL_ONE_MINUS_DST_ALPHA            = 0x0305;

	    //  BlendingFactorSrc
		public const uint GL_DST_COLOR                      = 0x0306;
		public const uint GL_ONE_MINUS_DST_COLOR            = 0x0307;
		public const uint GL_SRC_ALPHA_SATURATE             = 0x0308;
		
	    //   Boolean
		public const uint GL_TRUE                           = 1;
		public const uint GL_FALSE                          = 0;
		     
	    //   ClipPlaneName
		public const uint GL_CLIP_PLANE0                    = 0x3000;
		public const uint GL_CLIP_PLANE1                    = 0x3001;
		public const uint GL_CLIP_PLANE2                    = 0x3002;
		public const uint GL_CLIP_PLANE3                    = 0x3003;
		public const uint GL_CLIP_PLANE4                    = 0x3004;
		public const uint GL_CLIP_PLANE5                    = 0x3005;
	
	    //   DataType
		public const uint GL_BYTE                           = 0x1400;
		public const uint GL_UNSIGNED_BYTE                  = 0x1401;
		public const uint GL_SHORT                          = 0x1402;
		public const uint GL_UNSIGNED_SHORT                 = 0x1403;
		public const uint GL_INT                            = 0x1404;
		public const uint GL_UNSIGNED_INT                   = 0x1405;
		public const uint GL_FLOAT                          = 0x1406;
		public const uint GL_2_BYTES                        = 0x1407;
		public const uint GL_3_BYTES                        = 0x1408;
		public const uint GL_4_BYTES                        = 0x1409;
		public const uint GL_DOUBLE                         = 0x140A;
	
	    //   DrawBufferMode
		public const uint GL_NONE                           = 0;
		public const uint GL_FRONT_LEFT                     = 0x0400;
		public const uint GL_FRONT_RIGHT                    = 0x0401;
		public const uint GL_BACK_LEFT                      = 0x0402;
		public const uint GL_BACK_RIGHT                     = 0x0403;
		public const uint GL_FRONT                          = 0x0404;
		public const uint GL_BACK                           = 0x0405;
		public const uint GL_LEFT                           = 0x0406;
		public const uint GL_RIGHT                          = 0x0407;
		public const uint GL_FRONT_AND_BACK                 = 0x0408;
		public const uint GL_AUX0                           = 0x0409;
		public const uint GL_AUX1                           = 0x040A;
		public const uint GL_AUX2                           = 0x040B;
		public const uint GL_AUX3                           = 0x040C;
	
	    //   ErrorCode
		public const uint GL_NO_ERROR                       = 0;
		public const uint GL_INVALID_ENUM                   = 0x0500;
		public const uint GL_INVALID_VALUE                  = 0x0501;
		public const uint GL_INVALID_OPERATION              = 0x0502;
		public const uint GL_STACK_OVERFLOW                 = 0x0503;
		public const uint GL_STACK_UNDERFLOW                = 0x0504;
		public const uint GL_OUT_OF_MEMORY                  = 0x0505;
	
	    //   FeedBackMode
		public const uint GL_2D                             = 0x0600;
		public const uint GL_3D                             = 0x0601;
		public const uint GL_4D_COLOR                       = 0x0602;
		public const uint GL_3D_COLOR_TEXTURE               = 0x0603;
		public const uint GL_4D_COLOR_TEXTURE               = 0x0604;
	
	    //   FeedBackToken
		public const uint GL_PASS_THROUGH_TOKEN             = 0x0700;
		public const uint GL_POINT_TOKEN                    = 0x0701;
		public const uint GL_LINE_TOKEN                     = 0x0702;
		public const uint GL_POLYGON_TOKEN                  = 0x0703;
		public const uint GL_BITMAP_TOKEN                   = 0x0704;
		public const uint GL_DRAW_PIXEL_TOKEN               = 0x0705;
		public const uint GL_COPY_PIXEL_TOKEN               = 0x0706;
		public const uint GL_LINE_RESET_TOKEN               = 0x0707;
	
	    //   FogMode
	   	public const uint GL_EXP                            = 0x0800;
		public const uint GL_EXP2                           = 0x0801;
	
	    //   FrontFaceDirection
		public const uint GL_CW                             = 0x0900;
		public const uint GL_CCW                            = 0x0901;
	
	    //    GetMapTarget 
		public const uint GL_COEFF                          = 0x0A00;
		public const uint GL_ORDER                          = 0x0A01;
		public const uint GL_DOMAIN                         = 0x0A02;
	
	    //   GetTarget
		public const uint GL_CURRENT_COLOR                  = 0x0B00;
		public const uint GL_CURRENT_INDEX                  = 0x0B01;
		public const uint GL_CURRENT_NORMAL                 = 0x0B02;
		public const uint GL_CURRENT_TEXTURE_COORDS         = 0x0B03;
		public const uint GL_CURRENT_RASTER_COLOR           = 0x0B04;
		public const uint GL_CURRENT_RASTER_INDEX           = 0x0B05;
		public const uint GL_CURRENT_RASTER_TEXTURE_COORDS  = 0x0B06;
		public const uint GL_CURRENT_RASTER_POSITION        = 0x0B07;
		public const uint GL_CURRENT_RASTER_POSITION_VALID  = 0x0B08;
		public const uint GL_CURRENT_RASTER_DISTANCE        = 0x0B09;
		public const uint GL_POINT_SMOOTH                   = 0x0B10;
		public const uint GL_POINT_SIZE                     = 0x0B11;
		public const uint GL_POINT_SIZE_RANGE               = 0x0B12;
		public const uint GL_POINT_SIZE_GRANULARITY         = 0x0B13;
		public const uint GL_LINE_SMOOTH                    = 0x0B20;
		public const uint GL_LINE_WIDTH                     = 0x0B21;
		public const uint GL_LINE_WIDTH_RANGE               = 0x0B22;
		public const uint GL_LINE_WIDTH_GRANULARITY         = 0x0B23;
		public const uint GL_LINE_STIPPLE                   = 0x0B24;
		public const uint GL_LINE_STIPPLE_PATTERN           = 0x0B25;
		public const uint GL_LINE_STIPPLE_REPEAT            = 0x0B26;
		public const uint GL_LIST_MODE                      = 0x0B30;
		public const uint GL_MAX_LIST_NESTING               = 0x0B31;
		public const uint GL_LIST_BASE                      = 0x0B32;
		public const uint GL_LIST_INDEX                     = 0x0B33;
		public const uint GL_POLYGON_MODE                   = 0x0B40;
		public const uint GL_POLYGON_SMOOTH                 = 0x0B41;
		public const uint GL_POLYGON_STIPPLE                = 0x0B42;
		public const uint GL_EDGE_FLAG                      = 0x0B43;
		public const uint GL_CULL_FACE                      = 0x0B44;
		public const uint GL_CULL_FACE_MODE                 = 0x0B45;
		public const uint GL_FRONT_FACE                     = 0x0B46;
		public const uint GL_LIGHTING                       = 0x0B50;
		public const uint GL_LIGHT_MODEL_LOCAL_VIEWER       = 0x0B51;
		public const uint GL_LIGHT_MODEL_TWO_SIDE           = 0x0B52;
		public const uint GL_LIGHT_MODEL_AMBIENT            = 0x0B53;
		public const uint GL_SHADE_MODEL                    = 0x0B54;
		public const uint GL_COLOR_MATERIAL_FACE            = 0x0B55;
		public const uint GL_COLOR_MATERIAL_PARAMETER       = 0x0B56;
		public const uint GL_COLOR_MATERIAL                 = 0x0B57;
		public const uint GL_FOG                            = 0x0B60;
		public const uint GL_FOG_INDEX                      = 0x0B61;
		public const uint GL_FOG_DENSITY                    = 0x0B62;
		public const uint GL_FOG_START                      = 0x0B63;
		public const uint GL_FOG_END                        = 0x0B64;
		public const uint GL_FOG_MODE                       = 0x0B65;
		public const uint GL_FOG_COLOR                      = 0x0B66;
		public const uint GL_DEPTH_RANGE                    = 0x0B70;
		public const uint GL_DEPTH_TEST                     = 0x0B71;
		public const uint GL_DEPTH_WRITEMASK                = 0x0B72;
		public const uint GL_DEPTH_CLEAR_VALUE              = 0x0B73;
		public const uint GL_DEPTH_FUNC                     = 0x0B74;
		public const uint GL_ACCUM_CLEAR_VALUE              = 0x0B80;
		public const uint GL_STENCIL_TEST                   = 0x0B90;
		public const uint GL_STENCIL_CLEAR_VALUE            = 0x0B91;
		public const uint GL_STENCIL_FUNC                   = 0x0B92;
		public const uint GL_STENCIL_VALUE_MASK             = 0x0B93;
		public const uint GL_STENCIL_FAIL                   = 0x0B94;
		public const uint GL_STENCIL_PASS_DEPTH_FAIL        = 0x0B95;
		public const uint GL_STENCIL_PASS_DEPTH_PASS        = 0x0B96;
		public const uint GL_STENCIL_REF                    = 0x0B97;
		public const uint GL_STENCIL_WRITEMASK              = 0x0B98;
		public const uint GL_MATRIX_MODE                    = 0x0BA0;
		public const uint GL_NORMALIZE                      = 0x0BA1;
		public const uint GL_VIEWPORT                       = 0x0BA2;
		public const uint GL_MODELVIEW_STACK_DEPTH          = 0x0BA3;
		public const uint GL_PROJECTION_STACK_DEPTH         = 0x0BA4;
		public const uint GL_TEXTURE_STACK_DEPTH            = 0x0BA5;
		public const uint GL_MODELVIEW_MATRIX               = 0x0BA6;
		public const uint GL_PROJECTION_MATRIX              = 0x0BA7;
		public const uint GL_TEXTURE_MATRIX                 = 0x0BA8;
		public const uint GL_ATTRIB_STACK_DEPTH             = 0x0BB0;
		public const uint GL_CLIENT_ATTRIB_STACK_DEPTH      = 0x0BB1;
		public const uint GL_ALPHA_TEST                     = 0x0BC0;
		public const uint GL_ALPHA_TEST_FUNC                = 0x0BC1;
		public const uint GL_ALPHA_TEST_REF                 = 0x0BC2;
		public const uint GL_DITHER                         = 0x0BD0;
		public const uint GL_BLEND_DST                      = 0x0BE0;
		public const uint GL_BLEND_SRC                      = 0x0BE1;
		public const uint GL_BLEND                          = 0x0BE2;
		public const uint GL_LOGIC_OP_MODE                  = 0x0BF0;
		public const uint GL_INDEX_LOGIC_OP                 = 0x0BF1;
		public const uint GL_COLOR_LOGIC_OP                 = 0x0BF2;
		public const uint GL_AUX_BUFFERS                    = 0x0C00;
		public const uint GL_DRAW_BUFFER                    = 0x0C01;
		public const uint GL_READ_BUFFER                    = 0x0C02;
		public const uint GL_SCISSOR_BOX                    = 0x0C10;
		public const uint GL_SCISSOR_TEST                   = 0x0C11;
		public const uint GL_INDEX_CLEAR_VALUE              = 0x0C20;
		public const uint GL_INDEX_WRITEMASK                = 0x0C21;
		public const uint GL_COLOR_CLEAR_VALUE              = 0x0C22;
		public const uint GL_COLOR_WRITEMASK                = 0x0C23;
		public const uint GL_INDEX_MODE                     = 0x0C30;
		public const uint GL_RGBA_MODE                      = 0x0C31;
		public const uint GL_DOUBLEBUFFER                   = 0x0C32;
		public const uint GL_STEREO                         = 0x0C33;
		public const uint GL_RENDER_MODE                    = 0x0C40;
		public const uint GL_PERSPECTIVE_CORRECTION_HINT    = 0x0C50;
		public const uint GL_POINT_SMOOTH_HINT              = 0x0C51;
		public const uint GL_LINE_SMOOTH_HINT               = 0x0C52;
		public const uint GL_POLYGON_SMOOTH_HINT            = 0x0C53;
		public const uint GL_FOG_HINT                       = 0x0C54;
		public const uint GL_TEXTURE_GEN_S                  = 0x0C60;
		public const uint GL_TEXTURE_GEN_T                  = 0x0C61;
		public const uint GL_TEXTURE_GEN_R                  = 0x0C62;
		public const uint GL_TEXTURE_GEN_Q                  = 0x0C63;
		public const uint GL_PIXEL_MAP_I_TO_I               = 0x0C70;
		public const uint GL_PIXEL_MAP_S_TO_S               = 0x0C71;
		public const uint GL_PIXEL_MAP_I_TO_R               = 0x0C72;
		public const uint GL_PIXEL_MAP_I_TO_G               = 0x0C73;
		public const uint GL_PIXEL_MAP_I_TO_B               = 0x0C74;
		public const uint GL_PIXEL_MAP_I_TO_A               = 0x0C75;
		public const uint GL_PIXEL_MAP_R_TO_R               = 0x0C76;
		public const uint GL_PIXEL_MAP_G_TO_G               = 0x0C77;
		public const uint GL_PIXEL_MAP_B_TO_B               = 0x0C78;
		public const uint GL_PIXEL_MAP_A_TO_A               = 0x0C79;
		public const uint GL_PIXEL_MAP_I_TO_I_SIZE          = 0x0CB0;
		public const uint GL_PIXEL_MAP_S_TO_S_SIZE          = 0x0CB1;
		public const uint GL_PIXEL_MAP_I_TO_R_SIZE          = 0x0CB2;
		public const uint GL_PIXEL_MAP_I_TO_G_SIZE          = 0x0CB3;
		public const uint GL_PIXEL_MAP_I_TO_B_SIZE          = 0x0CB4;
		public const uint GL_PIXEL_MAP_I_TO_A_SIZE          = 0x0CB5;
		public const uint GL_PIXEL_MAP_R_TO_R_SIZE          = 0x0CB6;
		public const uint GL_PIXEL_MAP_G_TO_G_SIZE          = 0x0CB7;
		public const uint GL_PIXEL_MAP_B_TO_B_SIZE          = 0x0CB8;
		public const uint GL_PIXEL_MAP_A_TO_A_SIZE          = 0x0CB9;
		public const uint GL_UNPACK_SWAP_BYTES              = 0x0CF0;
		public const uint GL_UNPACK_LSB_FIRST               = 0x0CF1;
		public const uint GL_UNPACK_ROW_LENGTH              = 0x0CF2;
		public const uint GL_UNPACK_SKIP_ROWS               = 0x0CF3;
		public const uint GL_UNPACK_SKIP_PIXELS             = 0x0CF4;
		public const uint GL_UNPACK_ALIGNMENT               = 0x0CF5;
		public const uint GL_PACK_SWAP_BYTES                = 0x0D00;
		public const uint GL_PACK_LSB_FIRST                 = 0x0D01;
		public const uint GL_PACK_ROW_LENGTH                = 0x0D02;
		public const uint GL_PACK_SKIP_ROWS                 = 0x0D03;
		public const uint GL_PACK_SKIP_PIXELS               = 0x0D04;
		public const uint GL_PACK_ALIGNMENT                 = 0x0D05;
		public const uint GL_MAP_COLOR                      = 0x0D10;
		public const uint GL_MAP_STENCIL                    = 0x0D11;
		public const uint GL_INDEX_SHIFT                    = 0x0D12;
		public const uint GL_INDEX_OFFSET                   = 0x0D13;
		public const uint GL_RED_SCALE                      = 0x0D14;
		public const uint GL_RED_BIAS                       = 0x0D15;
		public const uint GL_ZOOM_X                         = 0x0D16;
		public const uint GL_ZOOM_Y                         = 0x0D17;
		public const uint GL_GREEN_SCALE                    = 0x0D18;
		public const uint GL_GREEN_BIAS                     = 0x0D19;
		public const uint GL_BLUE_SCALE                     = 0x0D1A;
		public const uint GL_BLUE_BIAS                      = 0x0D1B;
		public const uint GL_ALPHA_SCALE                    = 0x0D1C;
		public const uint GL_ALPHA_BIAS                     = 0x0D1D;
		public const uint GL_DEPTH_SCALE                    = 0x0D1E;
		public const uint GL_DEPTH_BIAS                     = 0x0D1F;
		public const uint GL_MAX_EVAL_ORDER                 = 0x0D30;
		public const uint GL_MAX_LIGHTS                     = 0x0D31;
		public const uint GL_MAX_CLIP_PLANES                = 0x0D32;
		public const uint GL_MAX_TEXTURE_SIZE               = 0x0D33;
		public const uint GL_MAX_PIXEL_MAP_TABLE            = 0x0D34;
		public const uint GL_MAX_ATTRIB_STACK_DEPTH         = 0x0D35;
		public const uint GL_MAX_MODELVIEW_STACK_DEPTH      = 0x0D36;
		public const uint GL_MAX_NAME_STACK_DEPTH           = 0x0D37;
		public const uint GL_MAX_PROJECTION_STACK_DEPTH     = 0x0D38;
		public const uint GL_MAX_TEXTURE_STACK_DEPTH        = 0x0D39;
		public const uint GL_MAX_VIEWPORT_DIMS              = 0x0D3A;
		public const uint GL_MAX_CLIENT_ATTRIB_STACK_DEPTH  = 0x0D3B;
		public const uint GL_SUBPIXEL_BITS                  = 0x0D50;
		public const uint GL_INDEX_BITS                     = 0x0D51;
		public const uint GL_RED_BITS                       = 0x0D52;
		public const uint GL_GREEN_BITS                     = 0x0D53;
		public const uint GL_BLUE_BITS                      = 0x0D54;
		public const uint GL_ALPHA_BITS                     = 0x0D55;
		public const uint GL_DEPTH_BITS                     = 0x0D56;
		public const uint GL_STENCIL_BITS                   = 0x0D57;
		public const uint GL_ACCUM_RED_BITS                 = 0x0D58;
		public const uint GL_ACCUM_GREEN_BITS               = 0x0D59;
		public const uint GL_ACCUM_BLUE_BITS                = 0x0D5A;
		public const uint GL_ACCUM_ALPHA_BITS               = 0x0D5B;
		public const uint GL_NAME_STACK_DEPTH               = 0x0D70;
		public const uint GL_AUTO_NORMAL                    = 0x0D80;
		public const uint GL_MAP1_COLOR_4                   = 0x0D90;
		public const uint GL_MAP1_INDEX                     = 0x0D91;
		public const uint GL_MAP1_NORMAL                    = 0x0D92;
		public const uint GL_MAP1_TEXTURE_COORD_1           = 0x0D93;
		public const uint GL_MAP1_TEXTURE_COORD_2           = 0x0D94;
		public const uint GL_MAP1_TEXTURE_COORD_3           = 0x0D95;
		public const uint GL_MAP1_TEXTURE_COORD_4           = 0x0D96;
		public const uint GL_MAP1_VERTEX_3                  = 0x0D97;
		public const uint GL_MAP1_VERTEX_4                  = 0x0D98;
		public const uint GL_MAP2_COLOR_4                   = 0x0DB0;
		public const uint GL_MAP2_INDEX                     = 0x0DB1;
		public const uint GL_MAP2_NORMAL                    = 0x0DB2;
		public const uint GL_MAP2_TEXTURE_COORD_1           = 0x0DB3;
		public const uint GL_MAP2_TEXTURE_COORD_2           = 0x0DB4;
		public const uint GL_MAP2_TEXTURE_COORD_3           = 0x0DB5;
		public const uint GL_MAP2_TEXTURE_COORD_4           = 0x0DB6;
		public const uint GL_MAP2_VERTEX_3                  = 0x0DB7;
		public const uint GL_MAP2_VERTEX_4                  = 0x0DB8;
		public const uint GL_MAP1_GRID_DOMAIN               = 0x0DD0;
		public const uint GL_MAP1_GRID_SEGMENTS             = 0x0DD1;
		public const uint GL_MAP2_GRID_DOMAIN               = 0x0DD2;
		public const uint GL_MAP2_GRID_SEGMENTS             = 0x0DD3;
		public const uint GL_TEXTURE_1D                     = 0x0DE0;
		public const uint GL_TEXTURE_2D                     = 0x0DE1;
		public const uint GL_FEEDBACK_BUFFER_POINTER        = 0x0DF0;
		public const uint GL_FEEDBACK_BUFFER_SIZE           = 0x0DF1;
		public const uint GL_FEEDBACK_BUFFER_TYPE           = 0x0DF2;
		public const uint GL_SELECTION_BUFFER_POINTER       = 0x0DF3;
		public const uint GL_SELECTION_BUFFER_SIZE          = 0x0DF4;
	
	    //   GetTextureParameter
		public const uint GL_TEXTURE_WIDTH                  = 0x1000;
		public const uint GL_TEXTURE_HEIGHT                 = 0x1001;
		public const uint GL_TEXTURE_INTERNAL_FORMAT        = 0x1003;
		public const uint GL_TEXTURE_BORDER_COLOR           = 0x1004;
		public const uint GL_TEXTURE_BORDER                 = 0x1005;
	
	    //   HintMode
		public const uint GL_DONT_CARE                      = 0x1100;
		public const uint GL_FASTEST                        = 0x1101;
		public const uint GL_NICEST                         = 0x1102;
	
	    //   LightName
		public const uint GL_LIGHT0                         = 0x4000;
		public const uint GL_LIGHT1                         = 0x4001;
		public const uint GL_LIGHT2                         = 0x4002;
		public const uint GL_LIGHT3                         = 0x4003;
		public const uint GL_LIGHT4                         = 0x4004;
		public const uint GL_LIGHT5                         = 0x4005;
		public const uint GL_LIGHT6                         = 0x4006;
		public const uint GL_LIGHT7                         = 0x4007;
	
	    //   LightParameter
		public const uint GL_AMBIENT                        = 0x1200;
		public const uint GL_DIFFUSE                        = 0x1201;
		public const uint GL_SPECULAR                       = 0x1202;
		public const uint GL_POSITION                       = 0x1203;
		public const uint GL_SPOT_DIRECTION                 = 0x1204;
		public const uint GL_SPOT_EXPONENT                  = 0x1205;
		public const uint GL_SPOT_CUTOFF                    = 0x1206;
		public const uint GL_CONSTANT_ATTENUATION           = 0x1207;
		public const uint GL_LINEAR_ATTENUATION             = 0x1208;
		public const uint GL_QUADRATIC_ATTENUATION          = 0x1209;
	
	    //   ListMode
		public const uint GL_COMPILE                        = 0x1300;
		public const uint GL_COMPILE_AND_EXECUTE            = 0x1301;
	
	    //   LogicOp
		public const uint GL_CLEAR                          = 0x1500;
		public const uint GL_AND                            = 0x1501;
		public const uint GL_AND_REVERSE                    = 0x1502;
		public const uint GL_COPY                           = 0x1503;
		public const uint GL_AND_INVERTED                   = 0x1504;
		public const uint GL_NOOP                           = 0x1505;
		public const uint GL_XOR                            = 0x1506;
		public const uint GL_OR                             = 0x1507;
		public const uint GL_NOR                            = 0x1508;
		public const uint GL_EQUIV                          = 0x1509;
		public const uint GL_INVERT                         = 0x150A;
		public const uint GL_OR_REVERSE                     = 0x150B;
		public const uint GL_COPY_INVERTED                  = 0x150C;
		public const uint GL_OR_INVERTED                    = 0x150D;
		public const uint GL_NAND                           = 0x150E;
		public const uint GL_SET                            = 0x150F;
	
	    //   MaterialParameter
		public const uint GL_EMISSION                       = 0x1600;
		public const uint GL_SHININESS                      = 0x1601;
		public const uint GL_AMBIENT_AND_DIFFUSE            = 0x1602;
		public const uint GL_COLOR_INDEXES                  = 0x1603;
	
	    //   MatrixMode
		public const uint GL_MODELVIEW                      = 0x1700;
		public const uint GL_PROJECTION                     = 0x1701;
		public const uint GL_TEXTURE                        = 0x1702;
	
	    //   PixelCopyType
		public const uint GL_COLOR                          = 0x1800;
		public const uint GL_DEPTH                          = 0x1801;
		public const uint GL_STENCIL                        = 0x1802;
	
	    //   PixelFormat
		public const uint GL_COLOR_INDEX                    = 0x1900;
		public const uint GL_STENCIL_INDEX                  = 0x1901;
		public const uint GL_DEPTH_COMPONENT                = 0x1902;
		public const uint GL_RED                            = 0x1903;
		public const uint GL_GREEN                          = 0x1904;
		public const uint GL_BLUE                           = 0x1905;
		public const uint GL_ALPHA                          = 0x1906;
		public const uint GL_RGB                            = 0x1907;
		public const uint GL_RGBA                           = 0x1908;
		public const uint GL_LUMINANCE                      = 0x1909;
		public const uint GL_LUMINANCE_ALPHA                = 0x190A;
	
	    //   PixelType
		public const uint GL_BITMAP                     = 0x1A00;
		
	    //   PolygonMode
		public const uint GL_POINT                          = 0x1B00;
		public const uint GL_LINE                           = 0x1B01;
		public const uint GL_FILL                           = 0x1B02;
	
	    //   RenderingMode 
		public const uint GL_RENDER                         = 0x1C00;
		public const uint GL_FEEDBACK                       = 0x1C01;
		public const uint GL_SELECT                         = 0x1C02;
	
	    //   ShadingModel
		public const uint GL_FLAT                           = 0x1D00;
		public const uint GL_SMOOTH                         = 0x1D01;
	
	    //   StencilOp	
		public const uint GL_KEEP                           = 0x1E00;
		public const uint GL_REPLACE                        = 0x1E01;
		public const uint GL_INCR                           = 0x1E02;
		public const uint GL_DECR                           = 0x1E03;
	
	    //   StringName
		public const uint GL_VENDOR                         = 0x1F00;
		public const uint GL_RENDERER                       = 0x1F01;
		public const uint GL_VERSION                        = 0x1F02;
		public const uint GL_EXTENSIONS                     = 0x1F03;
	
	    //   TextureCoordName
		public const uint GL_S                              = 0x2000;
		public const uint GL_T                              = 0x2001;
		public const uint GL_R                              = 0x2002;
		public const uint GL_Q                              = 0x2003;
	
	    //   TextureEnvMode
		public const uint GL_MODULATE                       = 0x2100;
		public const uint GL_DECAL                          = 0x2101;
	
	    //   TextureEnvParameter
		public const uint GL_TEXTURE_ENV_MODE               = 0x2200;
		public const uint GL_TEXTURE_ENV_COLOR              = 0x2201;
	
	    //   TextureEnvTarget
		public const uint GL_TEXTURE_ENV                    = 0x2300;
	
	    //   TextureGenMode 
		public const uint GL_EYE_LINEAR                     = 0x2400;
		public const uint GL_OBJECT_LINEAR                  = 0x2401;
		public const uint GL_SPHERE_MAP                     = 0x2402;
	
	    //   TextureGenParameter
		public const uint GL_TEXTURE_GEN_MODE               = 0x2500;
		public const uint GL_OBJECT_PLANE                   = 0x2501;
		public const uint GL_EYE_PLANE                      = 0x2502;
	
	    //   TextureMagFilter
		public const uint GL_NEAREST                        = 0x2600;
		public const uint GL_LINEAR                         = 0x2601;
	
	    //   TextureMinFilter 
		public const uint GL_NEAREST_MIPMAP_NEAREST         = 0x2700;
		public const uint GL_LINEAR_MIPMAP_NEAREST          = 0x2701;
		public const uint GL_NEAREST_MIPMAP_LINEAR          = 0x2702;
		public const uint GL_LINEAR_MIPMAP_LINEAR           = 0x2703;
	
	    //   TextureParameterName
		public const uint GL_TEXTURE_MAG_FILTER             = 0x2800;
		public const uint GL_TEXTURE_MIN_FILTER             = 0x2801;
		public const uint GL_TEXTURE_WRAP_S                 = 0x2802;
		public const uint GL_TEXTURE_WRAP_T                 = 0x2803;
	
	    //   TextureWrapMode
		public const uint GL_CLAMP                          = 0x2900;
		public const uint GL_REPEAT                         = 0x2901;
	
	    //   ClientAttribMask
		public const uint GL_CLIENT_PIXEL_STORE_BIT         = 0x00000001;
		public const uint GL_CLIENT_VERTEX_ARRAY_BIT        = 0x00000002;
		public const uint GL_CLIENT_ALL_ATTRIB_BITS         = 0xffffffff;
	
	    //   Polygon Offset
		public const uint GL_POLYGON_OFFSET_FACTOR          = 0x8038;
		public const uint GL_POLYGON_OFFSET_UNITS           = 0x2A00;
		public const uint GL_POLYGON_OFFSET_POINT           = 0x2A01;
		public const uint GL_POLYGON_OFFSET_LINE            = 0x2A02;
		public const uint GL_POLYGON_OFFSET_FILL            = 0x8037;
	
	    //   Texture 
		public const uint GL_ALPHA4                         = 0x803B;
		public const uint GL_ALPHA8                         = 0x803C;
		public const uint GL_ALPHA12                        = 0x803D;
		public const uint GL_ALPHA16                        = 0x803E;
		public const uint GL_LUMINANCE4                     = 0x803F;
		public const uint GL_LUMINANCE8                     = 0x8040;
		public const uint GL_LUMINANCE12                    = 0x8041;
		public const uint GL_LUMINANCE16                    = 0x8042;
		public const uint GL_LUMINANCE4_ALPHA4              = 0x8043;
		public const uint GL_LUMINANCE6_ALPHA2              = 0x8044;
		public const uint GL_LUMINANCE8_ALPHA8              = 0x8045;
		public const uint GL_LUMINANCE12_ALPHA4             = 0x8046;
		public const uint GL_LUMINANCE12_ALPHA12            = 0x8047;
		public const uint GL_LUMINANCE16_ALPHA16            = 0x8048;
		public const uint GL_INTENSITY                      = 0x8049;
		public const uint GL_INTENSITY4                     = 0x804A;
		public const uint GL_INTENSITY8                     = 0x804B;
		public const uint GL_INTENSITY12                    = 0x804C;
		public const uint GL_INTENSITY16                    = 0x804D;
		public const uint GL_R3_G3_B2                       = 0x2A10;
		public const uint GL_RGB4                           = 0x804F;
		public const uint GL_RGB5                           = 0x8050;
		public const uint GL_RGB8                           = 0x8051;
		public const uint GL_RGB10                          = 0x8052;
		public const uint GL_RGB12                          = 0x8053;
		public const uint GL_RGB16                          = 0x8054;
		public const uint GL_RGBA2                          = 0x8055;
		public const uint GL_RGBA4                          = 0x8056;
		public const uint GL_RGB5_A1                        = 0x8057;
		public const uint GL_RGBA8                          = 0x8058;
		public const uint GL_RGB10_A2                       = 0x8059;
		public const uint GL_RGBA12                         = 0x805A;
		public const uint GL_RGBA16                         = 0x805B;
		public const uint GL_TEXTURE_RED_SIZE               = 0x805C;
		public const uint GL_TEXTURE_GREEN_SIZE             = 0x805D;
		public const uint GL_TEXTURE_BLUE_SIZE              = 0x805E;
		public const uint GL_TEXTURE_ALPHA_SIZE             = 0x805F;
		public const uint GL_TEXTURE_LUMINANCE_SIZE         = 0x8060;
		public const uint GL_TEXTURE_INTENSITY_SIZE         = 0x8061;
		public const uint GL_PROXY_TEXTURE_1D               = 0x8063;
		public const uint GL_PROXY_TEXTURE_2D               = 0x8064;
	
	    //   Texture object
		public const uint GL_TEXTURE_PRIORITY               = 0x8066;
		public const uint GL_TEXTURE_RESIDENT               = 0x8067;
		public const uint GL_TEXTURE_BINDING_1D             = 0x8068;
		public const uint GL_TEXTURE_BINDING_2D             = 0x8069;
	
	    //   Vertex array
		public const uint GL_VERTEX_ARRAY                   = 0x8074;
		public const uint GL_NORMAL_ARRAY                   = 0x8075;
		public const uint GL_COLOR_ARRAY                    = 0x8076;
		public const uint GL_INDEX_ARRAY                    = 0x8077;
		public const uint GL_TEXTURE_COORD_ARRAY            = 0x8078;
		public const uint GL_EDGE_FLAG_ARRAY                = 0x8079;
		public const uint GL_VERTEX_ARRAY_SIZE              = 0x807A;
		public const uint GL_VERTEX_ARRAY_TYPE              = 0x807B;
		public const uint GL_VERTEX_ARRAY_STRIDE            = 0x807C;
		public const uint GL_NORMAL_ARRAY_TYPE              = 0x807E;
		public const uint GL_NORMAL_ARRAY_STRIDE            = 0x807F;
		public const uint GL_COLOR_ARRAY_SIZE               = 0x8081;
		public const uint GL_COLOR_ARRAY_TYPE               = 0x8082;
		public const uint GL_COLOR_ARRAY_STRIDE             = 0x8083;
		public const uint GL_INDEX_ARRAY_TYPE               = 0x8085;
		public const uint GL_INDEX_ARRAY_STRIDE             = 0x8086;
		public const uint GL_TEXTURE_COORD_ARRAY_SIZE       = 0x8088;
		public const uint GL_TEXTURE_COORD_ARRAY_TYPE       = 0x8089;
		public const uint GL_TEXTURE_COORD_ARRAY_STRIDE     = 0x808A;
		public const uint GL_EDGE_FLAG_ARRAY_STRIDE         = 0x808C;
		public const uint GL_VERTEX_ARRAY_POINTER           = 0x808E;
		public const uint GL_NORMAL_ARRAY_POINTER           = 0x808F;
		public const uint GL_COLOR_ARRAY_POINTER            = 0x8090;
		public const uint GL_INDEX_ARRAY_POINTER            = 0x8091;
		public const uint GL_TEXTURE_COORD_ARRAY_POINTER    = 0x8092;
		public const uint GL_EDGE_FLAG_ARRAY_POINTER        = 0x8093;
		public const uint GL_V2F                            = 0x2A20;
		public const uint GL_V3F                            = 0x2A21;
		public const uint GL_C4UB_V2F                       = 0x2A22;
		public const uint GL_C4UB_V3F                       = 0x2A23;
		public const uint GL_C3F_V3F                        = 0x2A24;
		public const uint GL_N3F_V3F                        = 0x2A25;
		public const uint GL_C4F_N3F_V3F                    = 0x2A26;
		public const uint GL_T2F_V3F                        = 0x2A27;
		public const uint GL_T4F_V4F                        = 0x2A28;
		public const uint GL_T2F_C4UB_V3F                   = 0x2A29;
		public const uint GL_T2F_C3F_V3F                    = 0x2A2A;
		public const uint GL_T2F_N3F_V3F                    = 0x2A2B;
		public const uint GL_T2F_C4F_N3F_V3F                = 0x2A2C;
		public const uint GL_T4F_C4F_N3F_V4F                = 0x2A2D;
	
	//   Extensions
		public const uint GL_EXT_vertex_array               = 1;
		public const uint GL_EXT_bgra                       = 1;
		public const uint GL_EXT_paletted_texture           = 1;
		public const uint GL_WIN_swap_hint                  = 1;
		public const uint GL_WIN_draw_range_elements        = 1;
		
	//   EXT_vertex_array 
		public const uint GL_VERTEX_ARRAY_EXT               = 0x8074;
		public const uint GL_NORMAL_ARRAY_EXT               = 0x8075;
		public const uint GL_COLOR_ARRAY_EXT                = 0x8076;
		public const uint GL_INDEX_ARRAY_EXT                = 0x8077;
		public const uint GL_TEXTURE_COORD_ARRAY_EXT        = 0x8078;
		public const uint GL_EDGE_FLAG_ARRAY_EXT            = 0x8079;
		public const uint GL_VERTEX_ARRAY_SIZE_EXT          = 0x807A;
		public const uint GL_VERTEX_ARRAY_TYPE_EXT          = 0x807B;
		public const uint GL_VERTEX_ARRAY_STRIDE_EXT        = 0x807C;
		public const uint GL_VERTEX_ARRAY_COUNT_EXT         = 0x807D;
		public const uint GL_NORMAL_ARRAY_TYPE_EXT          = 0x807E;
		public const uint GL_NORMAL_ARRAY_STRIDE_EXT        = 0x807F;
		public const uint GL_NORMAL_ARRAY_COUNT_EXT         = 0x8080;
		public const uint GL_COLOR_ARRAY_SIZE_EXT           = 0x8081;
		public const uint GL_COLOR_ARRAY_TYPE_EXT           = 0x8082;
		public const uint GL_COLOR_ARRAY_STRIDE_EXT         = 0x8083;
		public const uint GL_COLOR_ARRAY_COUNT_EXT          = 0x8084;
		public const uint GL_INDEX_ARRAY_TYPE_EXT           = 0x8085;
		public const uint GL_INDEX_ARRAY_STRIDE_EXT         = 0x8086;
		public const uint GL_INDEX_ARRAY_COUNT_EXT          = 0x8087;
		public const uint GL_TEXTURE_COORD_ARRAY_SIZE_EXT   = 0x8088;
		public const uint GL_TEXTURE_COORD_ARRAY_TYPE_EXT   = 0x8089;
		public const uint GL_TEXTURE_COORD_ARRAY_STRIDE_EXT = 0x808A;
		public const uint GL_TEXTURE_COORD_ARRAY_COUNT_EXT  = 0x808B;
		public const uint GL_EDGE_FLAG_ARRAY_STRIDE_EXT     = 0x808C;
		public const uint GL_EDGE_FLAG_ARRAY_COUNT_EXT      = 0x808D;
		public const uint GL_VERTEX_ARRAY_POINTER_EXT       = 0x808E;
		public const uint GL_NORMAL_ARRAY_POINTER_EXT       = 0x808F;
		public const uint GL_COLOR_ARRAY_POINTER_EXT        = 0x8090;
		public const uint GL_INDEX_ARRAY_POINTER_EXT        = 0x8091;
		public const uint GL_TEXTURE_COORD_ARRAY_POINTER_EXT = 0x8092;
		public const uint GL_EDGE_FLAG_ARRAY_POINTER_EXT    = 0x8093;
		public const uint GL_DOUBLE_EXT                     =1;/*DOUBLE*/
		
	//   EXT_paletted_texture
		public const uint GL_COLOR_TABLE_FORMAT_EXT         = 0x80D8;
		public const uint GL_COLOR_TABLE_WIDTH_EXT          = 0x80D9;
		public const uint GL_COLOR_TABLE_RED_SIZE_EXT       = 0x80DA;
		public const uint GL_COLOR_TABLE_GREEN_SIZE_EXT     = 0x80DB;
		public const uint GL_COLOR_TABLE_BLUE_SIZE_EXT      = 0x80DC;
		public const uint GL_COLOR_TABLE_ALPHA_SIZE_EXT     = 0x80DD;
		public const uint GL_COLOR_TABLE_LUMINANCE_SIZE_EXT = 0x80DE;
		public const uint GL_COLOR_TABLE_INTENSITY_SIZE_EXT = 0x80DF;
		public const uint GL_COLOR_INDEX1_EXT               = 0x80E2;
		public const uint GL_COLOR_INDEX2_EXT               = 0x80E3;
		public const uint GL_COLOR_INDEX4_EXT               = 0x80E4;
		public const uint GL_COLOR_INDEX8_EXT               = 0x80E5;
		public const uint GL_COLOR_INDEX12_EXT              = 0x80E6;
		public const uint GL_COLOR_INDEX16_EXT              = 0x80E7;
	
	//   WIN_draw_range_elements
		public const uint GL_MAX_ELEMENTS_VERTICES_WIN      = 0x80E8;
		public const uint GL_MAX_ELEMENTS_INDICES_WIN       = 0x80E9;
	
	//   WIN_phong_shading
		public const uint GL_PHONG_WIN                      = 0x80EA;
		public const uint GL_PHONG_HINT_WIN                 = 0x80EB; 
	

	//   WIN_specular_fog 
		public const uint FOG_SPECULAR_TEXTURE_WIN       = 0x80EC;

		// Delegates
		private delegate void glAccum(uint op, float value);
		private Delegate glAccumDelegate;
		private delegate void glAlphaFunc(uint func, float ref_notkeword);
		private Delegate glAlphaFuncDelegate;
		private delegate byte glAreTexturesResident(int n, uint[] textures, byte[] residences);
		private Delegate glAreTexturesResidentDelegate;
		private delegate void glArrayElement(int i);
		private Delegate glArrayElementDelegate;
		private delegate void glBegin(uint mode);
		private Delegate glBeginDelegate;
		private delegate void glBindTexture(uint target, uint texture);
		private Delegate glBindTextureDelegate;
		private delegate void glBitmap(int width, int height, float xorig, float yorig, float xmove, float ymove, byte[] bitmap);
		private Delegate glBitmapDelegate;
		private delegate void glBlendFunc(uint sfactor, uint dfactor);
		private Delegate glBlendFuncDelegate;
		private delegate void glCallList(uint list);
		private Delegate glCallListDelegate;
		private delegate void glCallLists(int n, uint type, IntPtr lists);
		private Delegate glCallListsDelegate;
		private delegate void glClear(uint mask);
		private Delegate glClearDelegate;
		private delegate void glClearAccum(float red, float green, float blue, float alpha);
		private Delegate glClearAccumDelegate;
		private delegate void glClearColor(float red, float green, float blue, float alpha);
		private Delegate glClearColorDelegate;
		private delegate void glClearDepth(double depth);
		private Delegate glClearDepthDelegate;
		private delegate void glClearIndex(float c);
		private Delegate glClearIndexDelegate;
		private delegate void glClearStencil(int s);
		private Delegate glClearStencilDelegate;
		private delegate void glClipPlane(uint plane, double[] equation);
		private Delegate glClipPlaneDelegate;
		private delegate void glColor3d(double red, double green, double blue);
		private Delegate glColor3dDelegate;
		private delegate void glColor3dv(double[] v);
		private Delegate glColor3dvDelegate;
		private delegate void glColor3f(float red, float green, float blue);
		private Delegate glColor3fDelegate;
		private delegate void glColor3fv(float[] v);
		private Delegate glColor3fvDelegate;
		private delegate void glColor3i(int red, int green, int blue);
		private Delegate glColor3iDelegate;
		private delegate void glColor3iv(int[] v);
		private Delegate glColor3ivDelegate;
		private delegate void glColor3s(short red, short green, short blue);
		private Delegate glColor3sDelegate;
		private delegate void glColor3sv(short[] v);
		private Delegate glColor3svDelegate;
		private delegate void glColor3ub(byte red, byte green, byte blue);
		private Delegate glColor3ubDelegate;
		private delegate void glColor3ubv(byte[] v);
		private Delegate glColor3ubvDelegate;
		private delegate void glColor3ui(uint red, uint green, uint blue);
		private Delegate glColor3uiDelegate;
		private delegate void glColor3uiv(uint[] v);
		private Delegate glColor3uivDelegate;
		private delegate void glColor3us(ushort red, ushort green, ushort blue);
		private Delegate glColor3usDelegate;
		private delegate void glColor3usv(ushort[] v);
		private Delegate glColor3usvDelegate;
		private delegate void glColor4d(double red, double green, double blue, double alpha);
		private Delegate glColor4dDelegate;
		private delegate void glColor4dv(double[] v);
		private Delegate glColor4dvDelegate;
		private delegate void glColor4f(float red, float green, float blue, float alpha);
		private Delegate glColor4fDelegate;
		private delegate void glColor4fv(float[] v);
		private Delegate glColor4fvDelegate;
		private delegate void glColor4i(int red, int green, int blue, int alpha);
		private Delegate glColor4iDelegate;
		private delegate void glColor4iv(int[] v);
		private Delegate glColor4ivDelegate;
		private delegate void glColor4s(short red, short green, short blue, short alpha);
		private Delegate glColor4sDelegate;
		private delegate void glColor4sv(short[] v);
		private Delegate glColor4svDelegate;
		private delegate void glColor4ub(byte red, byte green, byte blue, byte alpha);
		private Delegate glColor4ubDelegate;
		private delegate void glColor4ubv(byte[] v);
		private Delegate glColor4ubvDelegate;
		private delegate void glColor4ui(uint red, uint green, uint blue, uint alpha);
		private Delegate glColor4uiDelegate;
		private delegate void glColor4uiv(uint[] v);
		private Delegate glColor4uivDelegate;
		private delegate void glColor4us(ushort red, ushort green, ushort blue, ushort alpha);
		private Delegate glColor4usDelegate;
		private delegate void glColor4usv(ushort[] v);
		private Delegate glColor4usvDelegate;
		private delegate void glColorMask(byte red, byte green, byte blue, byte alpha);
		private Delegate glColorMaskDelegate;
		private delegate void glColorMaterial(uint face, uint mode);
		private Delegate glColorMaterialDelegate;
		private delegate void glColorPointer(int size, uint type, int stride, IntPtr pointer);
		private Delegate glColorPointerDelegate;
		private delegate void glCopyPixels(int x, int y, int width, int height, uint type);
		private Delegate glCopyPixelsDelegate;
		private delegate void glCopyTexImage1D(uint target, int level, uint internalFormat, int x, int y, int width, int border);
		private Delegate glCopyTexImage1DDelegate;
		private delegate void glCopyTexImage2D(uint target, int level, uint internalFormat, int x, int y, int width, int height, int border);
		private Delegate glCopyTexImage2DDelegate;
		private delegate void glCopyTexSubImage1D(uint target, int level, int xoffset, int x, int y, int width);
		private Delegate glCopyTexSubImage1DDelegate;
		private delegate void glCopyTexSubImage2D(uint target, int level, int xoffset, int yoffset, int x, int y, int width, int height);
		private Delegate glCopyTexSubImage2DDelegate;
		private delegate void glCullFace(uint mode);
		private Delegate glCullFaceDelegate;
		private delegate void glDeleteLists(uint list, int range);
		private Delegate glDeleteListsDelegate;
		private delegate void glDeleteTextures(int n, uint[] textures);
		private Delegate glDeleteTexturesDelegate;
		private delegate void glDepthFunc(uint func);
		private Delegate glDepthFuncDelegate;
		private delegate void glDepthMask(byte flag);
		private Delegate glDepthMaskDelegate;
		private delegate void glDepthRange(double zNear, double zFar);
		private Delegate glDepthRangeDelegate;
		private delegate void glDisable(uint cap);
		private Delegate glDisableDelegate;
		private delegate void glDisableClientState(uint array);
		private Delegate glDisableClientStateDelegate;
		private delegate void glDrawArrays(uint mode, int first, int count);
		private Delegate glDrawArraysDelegate;
		private delegate void glDrawBuffer(uint mode);
		private Delegate glDrawBufferDelegate;
        private delegate void glDrawElements(uint mode, int count, uint type, IntPtr indices);
		private Delegate glDrawElementsDelegate;
        private delegate void glDrawPixels(int width, int height, uint format, uint type, IntPtr pixels);
		private Delegate glDrawPixelsDelegate;
		private delegate void glEdgeFlag(byte flag);
		private Delegate glEdgeFlagDelegate;
		private delegate void glEdgeFlagPointer(int stride, int[] pointer);
		private Delegate glEdgeFlagPointerDelegate;
		private delegate void glEdgeFlagv(byte[] flag);
		private Delegate glEdgeFlagvDelegate;
		private delegate void glEnable(uint cap);
		private Delegate glEnableDelegate;
		private delegate void glEnableClientState(uint array);
		private Delegate glEnableClientStateDelegate;
		private delegate void glEnd();
		private Delegate glEndDelegate;
		private delegate void glEndList();
		private Delegate glEndListDelegate;
		private delegate void glEvalCoord1d(double u);
		private Delegate glEvalCoord1dDelegate;
		private delegate void glEvalCoord1dv(double[] u);
		private Delegate glEvalCoord1dvDelegate;
		private delegate void glEvalCoord1f(float u);
		private Delegate glEvalCoord1fDelegate;
		private delegate void glEvalCoord1fv(float[] u);
		private Delegate glEvalCoord1fvDelegate;
		private delegate void glEvalCoord2d(double u, double v);
		private Delegate glEvalCoord2dDelegate;
		private delegate void glEvalCoord2dv(double[] u);
		private Delegate glEvalCoord2dvDelegate;
		private delegate void glEvalCoord2f(float u, float v);
		private Delegate glEvalCoord2fDelegate;
		private delegate void glEvalCoord2fv(float[] u);
		private Delegate glEvalCoord2fvDelegate;
		private delegate void glEvalMesh1(uint mode, int i1, int i2);
		private Delegate glEvalMesh1Delegate;
		private delegate void glEvalMesh2(uint mode, int i1, int i2, int j1, int j2);
		private Delegate glEvalMesh2Delegate;
		private delegate void glEvalPoint1(int i);
		private Delegate glEvalPoint1Delegate;
		private delegate void glEvalPoint2(int i, int j);
		private Delegate glEvalPoint2Delegate;
		private delegate void glFeedbackBuffer(int size, uint type, float[] buffer);
		private Delegate glFeedbackBufferDelegate;
		private delegate void glFinish();
		private Delegate glFinishDelegate;
		private delegate void glFlush();
		private Delegate glFlushDelegate;
		private delegate void glFogf(uint pname, float param);
		private Delegate glFogfDelegate;
		private delegate void glFogfv(uint pname, float[] params_notkeyword);
		private Delegate glFogfvDelegate;
		private delegate void glFogi(uint pname, int param);
		private Delegate glFogiDelegate;
		private delegate void glFogiv(uint pname, int[] params_notkeyword);
		private Delegate glFogivDelegate;
		private delegate void glFrontFace(uint mode);
		private Delegate glFrontFaceDelegate;
		private delegate void glFrustum(double left, double right, double bottom, double top, double zNear, double zFar);
		private Delegate glFrustumDelegate;
		private delegate uint glGenLists(int range);
		private Delegate glGenListsDelegate;
		private delegate void glGenTextures(int n, uint[] textures);
		private Delegate glGenTexturesDelegate;
		private delegate void glGetBooleanv(uint pname, byte[] params_notkeyword);
		private Delegate glGetBooleanvDelegate;
		private delegate void glGetClipPlane(uint plane, double[] equation);
		private Delegate glGetClipPlaneDelegate;
		private delegate void glGetDoublev(uint pname, double[] params_notkeyword);
		private Delegate glGetDoublevDelegate;
		private delegate uint glGetError();
		private Delegate glGetErrorDelegate;
		private delegate void glGetFloatv(uint pname, float[] params_notkeyword);
		private Delegate glGetFloatvDelegate;
		private delegate void glGetIntegerv(uint pname, int[] params_notkeyword);
		private Delegate glGetIntegervDelegate;
		private delegate void glGetLightfv(uint light, uint pname, float[] params_notkeyword);
		private Delegate glGetLightfvDelegate;
		private delegate void glGetLightiv(uint light, uint pname, int[] params_notkeyword);
		private Delegate glGetLightivDelegate;
		private delegate void glGetMapdv(uint target, uint query, double[] v);
		private Delegate glGetMapdvDelegate;
		private delegate void glGetMapfv(uint target, uint query, float[] v);
		private Delegate glGetMapfvDelegate;
		private delegate void glGetMapiv(uint target, uint query, int[] v);
		private Delegate glGetMapivDelegate;
		private delegate void glGetMaterialfv(uint face, uint pname, float[] params_notkeyword);
		private Delegate glGetMaterialfvDelegate;
		private delegate void glGetMaterialiv(uint face, uint pname, int[] params_notkeyword);
		private Delegate glGetMaterialivDelegate;
		private delegate void glGetPixelMapfv(uint map, float[] values);
		private Delegate glGetPixelMapfvDelegate;
		private delegate void glGetPixelMapuiv(uint map, uint[] values);
		private Delegate glGetPixelMapuivDelegate;
		private delegate void glGetPixelMapusv(uint map, ushort[] values);
		private Delegate glGetPixelMapusvDelegate;
		private delegate void glGetPointerv(uint pname, int[] params_notkeyword);
		private Delegate glGetPointervDelegate;
		private delegate void glGetPolygonStipple(byte[] mask);
		private Delegate glGetPolygonStippleDelegate;
		private delegate IntPtr glGetString(uint name);
		private Delegate glGetStringDelegate;
		private delegate void glGetTexEnvfv(uint target, uint pname, float[] params_notkeyword);
		private Delegate glGetTexEnvfvDelegate;
		private delegate void glGetTexEnviv(uint target, uint pname, int[] params_notkeyword);
		private Delegate glGetTexEnvivDelegate;
		private delegate void glGetTexGendv(uint coord, uint pname, double[] params_notkeyword);
		private Delegate glGetTexGendvDelegate;
		private delegate void glGetTexGenfv(uint coord, uint pname, float[] params_notkeyword);
		private Delegate glGetTexGenfvDelegate;
		private delegate void glGetTexGeniv(uint coord, uint pname, int[] params_notkeyword);
		private Delegate glGetTexGenivDelegate;
		private delegate void glGetTexImage(uint target, int level, uint format, uint type, int[] pixels);
		private Delegate glGetTexImageDelegate;
		private delegate void glGetTexLevelParameterfv(uint target, int level, uint pname, float[] params_notkeyword);
		private Delegate glGetTexLevelParameterfvDelegate;
		private delegate void glGetTexLevelParameteriv(uint target, int level, uint pname, int[] params_notkeyword);
		private Delegate glGetTexLevelParameterivDelegate;
		private delegate void glGetTexParameterfv(uint target, uint pname, float[] params_notkeyword);
		private Delegate glGetTexParameterfvDelegate;
		private delegate void glGetTexParameteriv(uint target, uint pname, int[] params_notkeyword);
		private Delegate glGetTexParameterivDelegate;
		private delegate void glHint(uint target, uint mode);
		private Delegate glHintDelegate;
		private delegate void glIndexMask(uint mask);
		private Delegate glIndexMaskDelegate;
		private delegate void glIndexPointer(uint type, int stride, int[] pointer);
		private Delegate glIndexPointerDelegate;
		private delegate void glIndexd(double c);
		private Delegate glIndexdDelegate;
		private delegate void glIndexdv(double[] c);
		private Delegate glIndexdvDelegate;
		private delegate void glIndexf(float c);
		private Delegate glIndexfDelegate;
		private delegate void glIndexfv(float[] c);
		private Delegate glIndexfvDelegate;
		private delegate void glIndexi(int c);
		private Delegate glIndexiDelegate;
		private delegate void glIndexiv(int[] c);
		private Delegate glIndexivDelegate;
		private delegate void glIndexs(short c);
		private Delegate glIndexsDelegate;
		private delegate void glIndexsv(short[] c);
		private Delegate glIndexsvDelegate;
		private delegate void glIndexub(byte c);
		private Delegate glIndexubDelegate;
		private delegate void glIndexubv(byte[] c);
		private Delegate glIndexubvDelegate;
		private delegate void glInitNames();
		private Delegate glInitNamesDelegate;
		private delegate void glInterleavedArrays(uint format, int stride, int[] pointer);
		private Delegate glInterleavedArraysDelegate;
		private delegate byte glIsEnabled(uint cap);
		private Delegate glIsEnabledDelegate;
		private delegate byte glIsList(uint list);
		private Delegate glIsListDelegate;
		private delegate byte glIsTexture(uint texture);
		private Delegate glIsTextureDelegate;
		private delegate void glLightModelf(uint pname, float param);
		private Delegate glLightModelfDelegate;
		private delegate void glLightModelfv(uint pname, float[] params_notkeyword);
		private Delegate glLightModelfvDelegate;
		private delegate void glLightModeli(uint pname, int param);
		private Delegate glLightModeliDelegate;
		private delegate void glLightModeliv(uint pname, int[] params_notkeyword);
		private Delegate glLightModelivDelegate;
		private delegate void glLightf(uint light, uint pname, float param);
		private Delegate glLightfDelegate;
		private delegate void glLightfv(uint light, uint pname, float[] params_notkeyword);
		private Delegate glLightfvDelegate;
		private delegate void glLighti(uint light, uint pname, int param);
		private Delegate glLightiDelegate;
		private delegate void glLightiv(uint light, uint pname, int[] params_notkeyword);
		private Delegate glLightivDelegate;
		private delegate void glLineStipple(int factor, ushort pattern);
		private Delegate glLineStippleDelegate;
		private delegate void glLineWidth(float width);
		private Delegate glLineWidthDelegate;
		private delegate void glListBase(uint base_notkeyword);
		private Delegate glListBaseDelegate;
		private delegate void glLoadIdentity();
		private Delegate glLoadIdentityDelegate;
		private delegate void glLoadMatrixd(double[] m);
		private Delegate glLoadMatrixdDelegate;
		private delegate void glLoadMatrixf(float[] m);
		private Delegate glLoadMatrixfDelegate;
		private delegate void glLoadName(uint name);
		private Delegate glLoadNameDelegate;
		private delegate void glLogicOp(uint opcode);
		private Delegate glLogicOpDelegate;
		private delegate void glMap1d(uint target, double u1, double u2, int stride, int order, double[] points);
		private Delegate glMap1dDelegate;
		private delegate void glMap1f(uint target, float u1, float u2, int stride, int order, float[] points);
		private Delegate glMap1fDelegate;
		private delegate void glMap2d(uint target, double u1, double u2, int ustride, int uorder, double v1, double v2, int vstride, int vorder, double[] points);
		private Delegate glMap2dDelegate;
		private delegate void glMap2f(uint target, float u1, float u2, int ustride, int uorder, float v1, float v2, int vstride, int vorder, float[] points);
		private Delegate glMap2fDelegate;
		private delegate void glMapGrid1d(int un, double u1, double u2);
		private Delegate glMapGrid1dDelegate;
		private delegate void glMapGrid1f(int un, float u1, float u2);
		private Delegate glMapGrid1fDelegate;
		private delegate void glMapGrid2d(int un, double u1, double u2, int vn, double v1, double v2);
		private Delegate glMapGrid2dDelegate;
		private delegate void glMapGrid2f(int un, float u1, float u2, int vn, float v1, float v2);
		private Delegate glMapGrid2fDelegate;
		private delegate void glMaterialf(uint face, uint pname, float param);
		private Delegate glMaterialfDelegate;
		private delegate void glMaterialfv(uint face, uint pname, float[] params_notkeyword);
		private Delegate glMaterialfvDelegate;
		private delegate void glMateriali(uint face, uint pname, int param);
		private Delegate glMaterialiDelegate;
		private delegate void glMaterialiv(uint face, uint pname, int[] params_notkeyword);
		private Delegate glMaterialivDelegate;
		private delegate void glMatrixMode(uint mode);
		private Delegate glMatrixModeDelegate;
		private delegate void glMultMatrixd(double[] m);
		private Delegate glMultMatrixdDelegate;
		private delegate void glMultMatrixf(float[] m);
		private Delegate glMultMatrixfDelegate;
		private delegate void glNewList(uint list, uint mode);
		private Delegate glNewListDelegate;
		private delegate void glNormal3b(byte nx, byte ny, byte nz);
		private Delegate glNormal3bDelegate;
		private delegate void glNormal3bv(byte[] v);
		private Delegate glNormal3bvDelegate;
		private delegate void glNormal3d(double nx, double ny, double nz);
		private Delegate glNormal3dDelegate;
		private delegate void glNormal3dv(double[] v);
		private Delegate glNormal3dvDelegate;
		private delegate void glNormal3f(float nx, float ny, float nz);
		private Delegate glNormal3fDelegate;
		private delegate void glNormal3fv(float[] v);
		private Delegate glNormal3fvDelegate;
		private delegate void glNormal3i(int nx, int ny, int nz);
		private Delegate glNormal3iDelegate;
		private delegate void glNormal3iv(int[] v);
		private Delegate glNormal3ivDelegate;
		private delegate void glNormal3s(short nx, short ny, short nz);
		private Delegate glNormal3sDelegate;
        private delegate void glNormal3sv(short[] v);
		private Delegate glNormal3svDelegate;
        private delegate void glNormalPointer(uint type, int stride, IntPtr pointer);
		private Delegate glNormalPointerDelegate;
		private delegate void glOrtho(double left, double right, double bottom, double top, double zNear, double zFar);
		private Delegate glOrthoDelegate;
		private delegate void glPassThrough(float token);
		private Delegate glPassThroughDelegate;
		private delegate void glPixelMapfv(uint map, int mapsize, float[] values);
		private Delegate glPixelMapfvDelegate;
		private delegate void glPixelMapuiv(uint map, int mapsize, uint[] values);
		private Delegate glPixelMapuivDelegate;
		private delegate void glPixelMapusv(uint map, int mapsize, ushort[] values);
		private Delegate glPixelMapusvDelegate;
		private delegate void glPixelStoref(uint pname, float param);
		private Delegate glPixelStorefDelegate;
		private delegate void glPixelStorei(uint pname, int param);
		private Delegate glPixelStoreiDelegate;
		private delegate void glPixelTransferf(uint pname, float param);
		private Delegate glPixelTransferfDelegate;
		private delegate void glPixelTransferi(uint pname, int param);
		private Delegate glPixelTransferiDelegate;
		private delegate void glPixelZoom(float xfactor, float yfactor);
		private Delegate glPixelZoomDelegate;
		private delegate void glPointSize(float size);
		private Delegate glPointSizeDelegate;
		private delegate void glPolygonMode(uint face, uint mode);
		private Delegate glPolygonModeDelegate;
		private delegate void glPolygonOffset(float factor, float units);
		private Delegate glPolygonOffsetDelegate;
		private delegate void glPolygonStipple(byte[] mask);
		private Delegate glPolygonStippleDelegate;
		private delegate void glPopAttrib();
		private Delegate glPopAttribDelegate;
		private delegate void glPopClientAttrib();
		private Delegate glPopClientAttribDelegate;
		private delegate void glPopMatrix();
		private Delegate glPopMatrixDelegate;
		private delegate void glPopName();
		private Delegate glPopNameDelegate;
		private delegate void glPrioritizeTextures(int n, uint[] textures, float[] priorities);
		private Delegate glPrioritizeTexturesDelegate;
		private delegate void glPushAttrib(uint mask);
		private Delegate glPushAttribDelegate;
		private delegate void glPushClientAttrib(uint mask);
		private Delegate glPushClientAttribDelegate;
		private delegate void glPushMatrix();
		private Delegate glPushMatrixDelegate;
		private delegate void glPushName(uint name);
		private Delegate glPushNameDelegate;
		private delegate void glRasterPos2d(double x, double y);
		private Delegate glRasterPos2dDelegate;
		private delegate void glRasterPos2dv(double[] v);
		private Delegate glRasterPos2dvDelegate;
		private delegate void glRasterPos2f(float x, float y);
		private Delegate glRasterPos2fDelegate;
		private delegate void glRasterPos2fv(float[] v);
		private Delegate glRasterPos2fvDelegate;
		private delegate void glRasterPos2i(int x, int y);
		private Delegate glRasterPos2iDelegate;
		private delegate void glRasterPos2iv(int[] v);
		private Delegate glRasterPos2ivDelegate;
		private delegate void glRasterPos2s(short x, short y);
		private Delegate glRasterPos2sDelegate;
		private delegate void glRasterPos2sv(short[] v);
		private Delegate glRasterPos2svDelegate;
		private delegate void glRasterPos3d(double x, double y, double z);
		private Delegate glRasterPos3dDelegate;
		private delegate void glRasterPos3dv(double[] v);
		private Delegate glRasterPos3dvDelegate;
		private delegate void glRasterPos3f(float x, float y, float z);
		private Delegate glRasterPos3fDelegate;
		private delegate void glRasterPos3fv(float[] v);
		private Delegate glRasterPos3fvDelegate;
		private delegate void glRasterPos3i(int x, int y, int z);
		private Delegate glRasterPos3iDelegate;
		private delegate void glRasterPos3iv(int[] v);
		private Delegate glRasterPos3ivDelegate;
		private delegate void glRasterPos3s(short x, short y, short z);
		private Delegate glRasterPos3sDelegate;
		private delegate void glRasterPos3sv(short[] v);
		private Delegate glRasterPos3svDelegate;
		private delegate void glRasterPos4d(double x, double y, double z, double w);
		private Delegate glRasterPos4dDelegate;
		private delegate void glRasterPos4dv(double[] v);
		private Delegate glRasterPos4dvDelegate;
		private delegate void glRasterPos4f(float x, float y, float z, float w);
		private Delegate glRasterPos4fDelegate;
		private delegate void glRasterPos4fv(float[] v);
		private Delegate glRasterPos4fvDelegate;
		private delegate void glRasterPos4i(int x, int y, int z, int w);
		private Delegate glRasterPos4iDelegate;
		private delegate void glRasterPos4iv(int[] v);
		private Delegate glRasterPos4ivDelegate;
		private delegate void glRasterPos4s(short x, short y, short z, short w);
		private Delegate glRasterPos4sDelegate;
		private delegate void glRasterPos4sv(short[] v);
		private Delegate glRasterPos4svDelegate;
		private delegate void glReadBuffer(uint mode);
		private Delegate glReadBufferDelegate;
        private delegate void glReadPixels(int x, int y, int width, int height, uint format, uint type, IntPtr pixels);
		private Delegate glReadPixelsDelegate;
		private delegate void glRectd(double x1, double y1, double x2, double y2);
		private Delegate glRectdDelegate;
		private delegate void glRectdv(double[] v1, double[] v2);
		private Delegate glRectdvDelegate;
		private delegate void glRectf(float x1, float y1, float x2, float y2);
		private Delegate glRectfDelegate;
		private delegate void glRectfv(float[] v1, float[] v2);
		private Delegate glRectfvDelegate;
		private delegate void glRecti(int x1, int y1, int x2, int y2);
		private Delegate glRectiDelegate;
		private delegate void glRectiv(int[] v1, int[] v2);
		private Delegate glRectivDelegate;
		private delegate void glRects(short x1, short y1, short x2, short y2);
		private Delegate glRectsDelegate;
		private delegate void glRectsv(short[] v1, short[] v2);
		private Delegate glRectsvDelegate;
		private delegate int glRenderMode(uint mode);
		private Delegate glRenderModeDelegate;
		private delegate void glRotated(double angle, double x, double y, double z);
		private Delegate glRotatedDelegate;
		private delegate void glRotatef(float angle, float x, float y, float z);
		private Delegate glRotatefDelegate;
		private delegate void glScaled(double x, double y, double z);
		private Delegate glScaledDelegate;
		private delegate void glScalef(float x, float y, float z);
		private Delegate glScalefDelegate;
		private delegate void glScissor(int x, int y, int width, int height);
		private Delegate glScissorDelegate;
		private delegate void glSelectBuffer(int size, uint[] buffer);
		private Delegate glSelectBufferDelegate;
		private delegate void glShadeModel(uint mode);
		private Delegate glShadeModelDelegate;
		private delegate void glStencilFunc(uint func, int ref_notkeword, uint mask);
		private Delegate glStencilFuncDelegate;
		private delegate void glStencilMask(uint mask);
		private Delegate glStencilMaskDelegate;
		private delegate void glStencilOp(uint fail, uint zfail, uint zpass);
		private Delegate glStencilOpDelegate;
		private delegate void glTexCoord1d(double s);
		private Delegate glTexCoord1dDelegate;
		private delegate void glTexCoord1dv(double[] v);
		private Delegate glTexCoord1dvDelegate;
		private delegate void glTexCoord1f(float s);
		private Delegate glTexCoord1fDelegate;
		private delegate void glTexCoord1fv(float[] v);
		private Delegate glTexCoord1fvDelegate;
		private delegate void glTexCoord1i(int s);
		private Delegate glTexCoord1iDelegate;
		private delegate void glTexCoord1iv(int[] v);
		private Delegate glTexCoord1ivDelegate;
		private delegate void glTexCoord1s(short s);
		private Delegate glTexCoord1sDelegate;
		private delegate void glTexCoord1sv(short[] v);
		private Delegate glTexCoord1svDelegate;
		private delegate void glTexCoord2d(double s, double t);
		private Delegate glTexCoord2dDelegate;
		private delegate void glTexCoord2dv(double[] v);
		private Delegate glTexCoord2dvDelegate;
		private delegate void glTexCoord2f(float s, float t);
		private Delegate glTexCoord2fDelegate;
		private delegate void glTexCoord2fv(float[] v);
		private Delegate glTexCoord2fvDelegate;
		private delegate void glTexCoord2i(int s, int t);
		private Delegate glTexCoord2iDelegate;
		private delegate void glTexCoord2iv(int[] v);
		private Delegate glTexCoord2ivDelegate;
		private delegate void glTexCoord2s(short s, short t);
		private Delegate glTexCoord2sDelegate;
		private delegate void glTexCoord2sv(short[] v);
		private Delegate glTexCoord2svDelegate;
		private delegate void glTexCoord3d(double s, double t, double r);
		private Delegate glTexCoord3dDelegate;
		private delegate void glTexCoord3dv(double[] v);
		private Delegate glTexCoord3dvDelegate;
		private delegate void glTexCoord3f(float s, float t, float r);
		private Delegate glTexCoord3fDelegate;
		private delegate void glTexCoord3fv(float[] v);
		private Delegate glTexCoord3fvDelegate;
		private delegate void glTexCoord3i(int s, int t, int r);
		private Delegate glTexCoord3iDelegate;
		private delegate void glTexCoord3iv(int[] v);
		private Delegate glTexCoord3ivDelegate;
		private delegate void glTexCoord3s(short s, short t, short r);
		private Delegate glTexCoord3sDelegate;
		private delegate void glTexCoord3sv(short[] v);
		private Delegate glTexCoord3svDelegate;
		private delegate void glTexCoord4d(double s, double t, double r, double q);
		private Delegate glTexCoord4dDelegate;
		private delegate void glTexCoord4dv(double[] v);
		private Delegate glTexCoord4dvDelegate;
		private delegate void glTexCoord4f(float s, float t, float r, float q);
		private Delegate glTexCoord4fDelegate;
		private delegate void glTexCoord4fv(float[] v);
		private Delegate glTexCoord4fvDelegate;
		private delegate void glTexCoord4i(int s, int t, int r, int q);
		private Delegate glTexCoord4iDelegate;
		private delegate void glTexCoord4iv(int[] v);
		private Delegate glTexCoord4ivDelegate;
		private delegate void glTexCoord4s(short s, short t, short r, short q);
		private Delegate glTexCoord4sDelegate;
        private delegate void glTexCoord4sv(short[] v);
		private Delegate glTexCoord4svDelegate;
        private delegate void glTexCoordPointer(int size, uint type, int stride, IntPtr pointer);
		private Delegate glTexCoordPointerDelegate;
		private delegate void glTexEnvf(uint target, uint pname, float param);
		private Delegate glTexEnvfDelegate;
		private delegate void glTexEnvfv(uint target, uint pname, float[] params_notkeyword);
		private Delegate glTexEnvfvDelegate;
		private delegate void glTexEnvi(uint target, uint pname, int param);
		private Delegate glTexEnviDelegate;
		private delegate void glTexEnviv(uint target, uint pname, int[] params_notkeyword);
		private Delegate glTexEnvivDelegate;
		private delegate void glTexGend(uint coord, uint pname, double param);
		private Delegate glTexGendDelegate;
		private delegate void glTexGendv(uint coord, uint pname, double[] params_notkeyword);
		private Delegate glTexGendvDelegate;
		private delegate void glTexGenf(uint coord, uint pname, float param);
		private Delegate glTexGenfDelegate;
		private delegate void glTexGenfv(uint coord, uint pname, float[] params_notkeyword);
		private Delegate glTexGenfvDelegate;
		private delegate void glTexGeni(uint coord, uint pname, int param);
		private Delegate glTexGeniDelegate;
		private delegate void glTexGeniv(uint coord, uint pname, int[] params_notkeyword);
		private Delegate glTexGenivDelegate;
		private delegate void glTexImage1D(uint target, int level, uint internalformat, int width, int border, uint format, uint type, IntPtr pixels);
		private Delegate glTexImage1DDelegate;
		private delegate void glTexImage2D(uint target, int level, uint internalformat, int width, int height, int border, uint format, uint type, IntPtr pixels);
		private Delegate glTexImage2DDelegate;
		private delegate void glTexParameterf(uint target, uint pname, float param);
		private Delegate glTexParameterfDelegate;
		private delegate void glTexParameterfv(uint target, uint pname, float[] params_notkeyword);
		private Delegate glTexParameterfvDelegate;
		private delegate void glTexParameteri(uint target, uint pname, int param);
		private Delegate glTexParameteriDelegate;
		private delegate void glTexParameteriv(uint target, uint pname, int[] params_notkeyword);
		private Delegate glTexParameterivDelegate;
		private delegate void glTexSubImage1D(uint target, int level, int xoffset, int width, uint format, uint type, int[] pixels);
		private Delegate glTexSubImage1DDelegate;
		private delegate void glTexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, uint type, int[] pixels);
		private Delegate glTexSubImage2DDelegate;
		private delegate void glTranslated(double x, double y, double z);
		private Delegate glTranslatedDelegate;
		private delegate void glTranslatef(float x, float y, float z);
		private Delegate glTranslatefDelegate;
		private delegate void glVertex2d(double x, double y);
		private Delegate glVertex2dDelegate;
		private delegate void glVertex2dv(double[] v);
		private Delegate glVertex2dvDelegate;
		private delegate void glVertex2f(float x, float y);
		private Delegate glVertex2fDelegate;
		private delegate void glVertex2fv(float[] v);
		private Delegate glVertex2fvDelegate;
		private delegate void glVertex2i(int x, int y);
		private Delegate glVertex2iDelegate;
		private delegate void glVertex2iv(int[] v);
		private Delegate glVertex2ivDelegate;
		private delegate void glVertex2s(short x, short y);
		private Delegate glVertex2sDelegate;
		private delegate void glVertex2sv(short[] v);
		private Delegate glVertex2svDelegate;
		private delegate void glVertex3d(double x, double y, double z);
		private Delegate glVertex3dDelegate;
		private delegate void glVertex3dv(double[] v);
		private Delegate glVertex3dvDelegate;
		private delegate void glVertex3f(float x, float y, float z);
		private Delegate glVertex3fDelegate;
		private delegate void glVertex3fv(float[] v);
		private Delegate glVertex3fvDelegate;
		private delegate void glVertex3i(int x, int y, int z);
		private Delegate glVertex3iDelegate;
		private delegate void glVertex3iv(int[] v);
		private Delegate glVertex3ivDelegate;
		private delegate void glVertex3s(short x, short y, short z);
		private Delegate glVertex3sDelegate;
		private delegate void glVertex3sv(short[] v);
		private Delegate glVertex3svDelegate;
		private delegate void glVertex4d(double x, double y, double z, double w);
		private Delegate glVertex4dDelegate;
		private delegate void glVertex4dv(double[] v);
		private Delegate glVertex4dvDelegate;
		private delegate void glVertex4f(float x, float y, float z, float w);
		private Delegate glVertex4fDelegate;
		private delegate void glVertex4fv(float[] v);
		private Delegate glVertex4fvDelegate;
		private delegate void glVertex4i(int x, int y, int z, int w);
		private Delegate glVertex4iDelegate;
		private delegate void glVertex4iv(int[] v);
		private Delegate glVertex4ivDelegate;
		private delegate void glVertex4s(short x, short y, short z, short w);
		private Delegate glVertex4sDelegate;
		private delegate void glVertex4sv(short[] v);
		private Delegate glVertex4svDelegate;
        private delegate void glVertexPointer(int size, uint type, int stride, IntPtr pointer);
		private Delegate glVertexPointerDelegate;
		private delegate void glViewport(int x, int y, int width, int height);
		private Delegate glViewportDelegate;

		/// <summary>
		/// Set the Accumulation Buffer operation.
		/// </summary>
		/// <param name="op">Operation of the buffer.</param>
		/// <param name="value">Reference value.</param>
		public void Accum(uint op, float value)
		{
			getDelegateFor<glAccum>(ref glAccumDelegate)(op, value);
		}

        /// <summary>
        /// Set the Accumulation Buffer operation.
        /// </summary>
        /// <param name="op">Operation of the buffer.</param>
        /// <param name="value">Reference value.</param>
        public void Accum(Enumerations.AccumOperation op, float value)
        {
            getDelegateFor<glAccum>(ref glAccumDelegate)((uint)op, value);
        }

        /// <summary>
        /// Specify the Alpha Test function.
        /// </summary>
        /// <param name="func">Specifies the alpha comparison function. Symbolic constants OpenGL.NEVER, OpenGL.LESS, OpenGL.EQUAL, OpenGL.LEQUAL, OpenGL.GREATER, OpenGL.NOTEQUAL, OpenGL.GEQUAL and OpenGL.ALWAYS are accepted. The initial value is OpenGL.ALWAYS.</param>
        /// <param name="reference">Specifies the reference	value that incoming alpha values are compared to. This value is clamped to the range 0	through	1, where 0 represents the lowest possible alpha value and 1 the highest possible value. The initial reference value is 0.</param>
		public void AlphaFunc(uint func, float reference)
        {
            getDelegateFor<glAlphaFunc>(ref glAlphaFuncDelegate)(func, reference);
        }

        /// <summary>
        /// Specify the Alpha Test function.
        /// </summary>
        /// <param name="function">Specifies the alpha comparison function.</param>
        /// <param name="reference">Specifies the reference	value that incoming alpha values are compared to. This value is clamped to the range 0	through	1, where 0 represents the lowest possible alpha value and 1 the highest possible value. The initial reference value is 0.</param>
        public void AlphaFunc(Enumerations.AlphaTestFunction function, float reference)
        {
            getDelegateFor<glAlphaFunc>(ref glAlphaFuncDelegate)((uint)function, reference);
        }

        /// <summary>
        /// Determine if textures are loaded in texture memory.
        /// </summary>
        /// <param name="n">Specifies the number of textures to be queried.</param>
        /// <param name="textures">Specifies an array containing the names of the textures to be queried.</param>
        /// <param name="residences">Specifies an array in which the texture residence status is returned. The residence status of a texture named by an element of textures is returned in the corresponding element of residences.</param>
        /// <returns></returns>
		public byte AreTexturesResident(int n, uint[] textures, byte[] residences)
        {
            byte returnValue = getDelegateFor<glAreTexturesResident>(ref glAreTexturesResidentDelegate)(n, textures, residences);
            return returnValue;
        }

        /// <summary>
        /// Render a vertex using the specified vertex array element.
        /// </summary>
        /// <param name="i">Specifies an index	into the enabled vertex	data arrays.</param>
		public void ArrayElement(int i)
        {
            getDelegateFor<glArrayElement>(ref glArrayElementDelegate)(i);
        }
        
		/// <summary>
		/// Begin drawing geometry in the specified mode.
		/// </summary>
		/// <param name="mode">The mode to draw in, e.g. OpenGL.POLYGONS.</param>
        public void Begin(uint mode)
        {
            getDelegateFor<glBegin>(ref glBeginDelegate)(mode);
        }

        /// <summary>
        /// Begin drawing geometry in the specified mode.
        /// </summary>
        /// <param name="mode">The mode to draw in, e.g. OpenGL.POLYGONS.</param>
        public void Begin(Enumerations.BeginMode mode)
        {
            getDelegateFor<glBegin>(ref glBeginDelegate)((uint)mode);
        }

		/// <summary>
		/// Call this function after creating a texture to finalise creation of it, 
		/// or to make an existing texture current.
		/// </summary>
		/// <param name="target">The target type, e.g TEXTURE_2D.</param>
		/// <param name="texture">The OpenGL texture object.</param>
		public void BindTexture(uint target, uint texture)
		{
			getDelegateFor<glBindTexture>(ref glBindTextureDelegate)(target, texture);
		}

        /// <summary>
        /// Draw a bitmap.
        /// </summary>
        /// <param name="width">Specify the pixel width	of the bitmap image.</param>
        /// <param name="height">Specify the pixel height of the bitmap image.</param>
        /// <param name="xorig">Specify	the location of	the origin in the bitmap image. The origin is measured from the lower left corner of the bitmap, with right and up being the positive axes.</param>
        /// <param name="yorig">Specify	the location of	the origin in the bitmap image. The origin is measured from the lower left corner of the bitmap, with right and up being the positive axes.</param>
        /// <param name="xmove">Specify	the x and y offsets to be added	to the current	raster position	after the bitmap is drawn.</param>
        /// <param name="ymove">Specify	the x and y offsets to be added	to the current	raster position	after the bitmap is drawn.</param>
        /// <param name="bitmap">Specifies the address of the bitmap image.</param>
		public void Bitmap(int width, int height, float xorig, float yorig, float xmove, float ymove, byte[] bitmap)
        {
            getDelegateFor<glBitmap>(ref glBitmapDelegate)(width, height, xorig, yorig, xmove, ymove, bitmap);
        }

		/// <summary>
		/// This function sets the current blending function.
		/// </summary>
		/// <param name="sfactor">Source factor.</param>
		/// <param name="dfactor">Destination factor.</param>
		public void BlendFunc(uint sfactor, uint dfactor)
		{
			getDelegateFor<glBlendFunc>(ref glBlendFuncDelegate)(sfactor,dfactor);
		}

        /// <summary>
        /// This function sets the current blending function.
        /// </summary>
        /// <param name="sourceFactor">The source factor.</param>
        /// <param name="destinationFactor">The destination factor.</param>
        public void BlendFunc(Enumerations.BlendingSourceFactor sourceFactor, Enumerations.BlendingDestinationFactor destinationFactor)
        {
            getDelegateFor<glBlendFunc>(ref glBlendFuncDelegate)((uint)sourceFactor,(uint)destinationFactor);
        }

		/// <summary>
		/// This function calls a certain display list.
		/// </summary>
		/// <param name="list">The display list to call.</param>
		public void CallList(uint list)
		{
			getDelegateFor<glCallList>(ref glCallListDelegate)(list);
		}

        /// <summary>
        /// Execute	a list of display lists.
        /// </summary>
        /// <param name="n">Specifies the number of display lists to be executed.</param>
        /// <param name="type">Specifies the type of values in lists. Symbolic constants OpenGL.BYTE, OpenGL.UNSIGNED_BYTE, OpenGL.SHORT, OpenGL.UNSIGNED_SHORT, OpenGL.INT, OpenGL.UNSIGNED_INT, OpenGL.FLOAT, OpenGL.2_BYTES, OpenGL.3_BYTES and OpenGL.4_BYTES are accepted.</param>
        /// <param name="lists">Specifies the address of an array of name offsets in the display list. The pointer type is void because the offsets can be bytes, shorts, ints, or floats, depending on the value of type.</param>
		public void CallLists(int n, uint type, IntPtr lists)
        {
            getDelegateFor<glCallLists>(ref glCallListsDelegate)(n, type, lists);
        }

        /// <summary>
        /// Execute	a list of display lists.
        /// </summary>
        /// <param name="n">Specifies the number of display lists to be executed.</param>
        /// <param name="type">Specifies the type of values in lists. Symbolic constants OpenGL.BYTE, OpenGL.UNSIGNED_BYTE, OpenGL.SHORT, OpenGL.UNSIGNED_SHORT, OpenGL.INT, OpenGL.UNSIGNED_INT, OpenGL.FLOAT, OpenGL.2_BYTES, OpenGL.3_BYTES and OpenGL.4_BYTES are accepted.</param>
        /// <param name="lists">Specifies the address of an array of name offsets in the display list. The pointer type is void because the offsets can be bytes, shorts, ints, or floats, depending on the value of type.</param>
        public void CallLists(int n, Enumerations.DataType type, IntPtr lists)
        {
            getDelegateFor<glCallLists>(ref glCallListsDelegate)(n,(uint)type, lists);
        }

        /// <summary>
        /// Execute	a list of display lists. Automatically uses the GL_UNSIGNED_BYTE version of the function.
        /// </summary>
        /// <param name="n">The number of lists.</param>
        /// <param name="lists">The lists.</param>
        public void CallLists(int n, byte[] lists)
        {
            var pinned = GCHandle.Alloc(lists, GCHandleType.Pinned);
            getDelegateFor<glCallLists>(ref glCallListsDelegate)(n, GL_UNSIGNED_BYTE, pinned.AddrOfPinnedObject());
            pinned.Free();
        }

        /// <summary>
        /// Execute	a list of display lists. Automatically uses the GL_UNSIGNED_INT version of the function.
        /// </summary>
        /// <param name="n">The number of lists.</param>
        /// <param name="lists">The lists.</param>
        public void CallLists(int n, uint[] lists)
        {
            var pinned = GCHandle.Alloc(lists, GCHandleType.Pinned);
            getDelegateFor<glCallLists>(ref glCallListsDelegate)(n, GL_UNSIGNED_INT, pinned.AddrOfPinnedObject());
            pinned.Free();
        }

		/// <summary>
		/// This function clears the buffers specified by mask.
		/// </summary>
		/// <param name="mask">Which buffers to clear.</param>
		public void Clear(uint mask)
		{
			getDelegateFor<glClear>(ref glClearDelegate)(mask);
		}

        /// <summary>
        /// Specify clear values for the accumulation buffer.
        /// </summary>
        /// <param name="red">Specify the red, green, blue and alpha values used when the accumulation buffer is cleared. The initial values are all 0.</param>
        /// <param name="green">Specify the red, green, blue and alpha values used when the accumulation buffer is cleared. The initial values are all 0.</param>
        /// <param name="blue">Specify the red, green, blue and alpha values used when the accumulation buffer is cleared. The initial values are all 0.</param>
        /// <param name="alpha">Specify the red, green, blue and alpha values used when the accumulation buffer is cleared. The initial values are all 0.</param>
		public void ClearAccum(float red, float green, float blue, float alpha)
        {
            getDelegateFor<glClearAccum>(ref glClearAccumDelegate)(red, green, blue, alpha);
        }

		/// <summary>
		/// This function sets the color that the drawing buffer is 'cleared' to.
		/// </summary>
		/// <param name="red">Red component of the color(between 0 and 1).</param>
		/// <param name="green">Green component of the color(between 0 and 1).</param>
		/// <param name="blue">Blue component of the color(between 0 and 1)./</param>
		/// <param name="alpha">Alpha component of the color(between 0 and 1).</param>
		public void ClearColor(float red, float green, float blue, float alpha)
		{
			getDelegateFor<glClearColor>(ref glClearColorDelegate)(red, green, blue, alpha);
		}

        /// <summary>
        /// Specify the clear value for the depth buffer.
        /// </summary>
        /// <param name="depth">Specifies the depth value used	when the depth buffer is cleared. The initial value is 1.</param>
		public void ClearDepth(double depth)
        {
            getDelegateFor<glClearDepth>(ref glClearDepthDelegate)(depth);
        }

        /// <summary>
        /// Specify the clear value for the color index buffers.
        /// </summary>
        /// <param name="c">Specifies the index used when the color index buffers are cleared. The initial value is 0.</param>
		public void ClearIndex(float c)
        {
            getDelegateFor<glClearIndex>(ref glClearIndexDelegate)(c);
        }

        /// <summary>
        /// Specify the clear value for the stencil buffer.
        /// </summary>
        /// <param name="s">Specifies the index used when the stencil buffer is cleared. The initial value is 0.</param>
		public void ClearStencil(int s)
        {
            getDelegateFor<glClearStencil>(ref glClearStencilDelegate)(s);
        }

        /// <summary>
        /// Specify a plane against which all geometry is clipped.
        /// </summary>
        /// <param name="plane">Specifies which clipping plane is being positioned. Symbolic names of the form OpenGL.CLIP_PLANEi, where i is an integer between 0 and OpenGL.MAX_CLIP_PLANES -1, are accepted.</param>
        /// <param name="equation">Specifies the address of an	array of four double-precision floating-point values. These values are interpreted as a plane equation.</param>
		public void ClipPlane(uint plane, double[] equation)
        {
            getDelegateFor<glClipPlane>(ref glClipPlaneDelegate)(plane, equation);
        }

        /// <summary>
        /// Specify a plane against which all geometry is clipped.
        /// </summary>
        /// <param name="plane">Specifies which clipping plane is being positioned. Symbolic names of the form OpenGL.CLIP_PLANEi, where i is an integer between 0 and OpenGL.MAX_CLIP_PLANES -1, are accepted.</param>
        /// <param name="equation">Specifies the address of an	array of four double-precision floating-point values. These values are interpreted as a plane equation.</param>
        public void ClipPlane(Enumerations.ClipPlaneName plane, double[] equation)
        {
            getDelegateFor<glClipPlane>(ref glClipPlaneDelegate)((uint)plane, equation);
        }

		/// <summary>
		/// Sets the current color.
		/// </summary>
		/// <param name="red">Red color component(between 0 and 255).</param>
		/// <param name="green">Green color component(between 0 and 255).</param>
		/// <param name="blue">Blue color component(between 0 and 255).</param>
		public void Color(byte red, byte green, byte blue)
		{
			getDelegateFor<glColor3ub>(ref glColor3ubDelegate)(red, green, blue);
		}

        /// <summary>
        /// Sets the current color.
        /// </summary>
        /// <param name="red">Red color component(between 0 and 255).</param>
        /// <param name="green">Green color component(between 0 and 255).</param>
        /// <param name="blue">Blue color component(between 0 and 255).</param>
        /// <param name="alpha">Alpha color component(between 0 and 255).</param>
        public void Color(byte red, byte green, byte blue, byte alpha)
        {
            getDelegateFor<glColor4ub>(ref glColor4ubDelegate)(red, green, blue, alpha);
        }

		/// <summary>
		/// Sets the current color.
		/// </summary>
		/// <param name="red">Red color component(between 0 and 1).</param>
		/// <param name="green">Green color component(between 0 and 1).</param>
		/// <param name="blue">Blue color component(between 0 and 1).</param>
		public void Color(double red, double green, double blue)
		{
			getDelegateFor<glColor3d>(ref glColor3dDelegate)(red, green, blue);
		}

        /// <summary>
        /// Sets the current color.
        /// </summary>
        /// <param name="red">Red color component(between 0 and 1).</param>
        /// <param name="green">Green color component(between 0 and 1).</param>
        /// <param name="blue">Blue color component(between 0 and 1).</param>
        /// <param name="alpha">Alpha color component.</param>
        public void Color(double red, double green, double blue, double alpha)
        {
            getDelegateFor<glColor4d>(ref glColor4dDelegate)(red, green, blue, alpha);
        }

		/// <summary>
		/// Sets the current color.
		/// </summary>
		/// <param name="red">Red color component(between 0 and 1).</param>
		/// <param name="green">Green color component(between 0 and 1).</param>
		/// <param name="blue">Blue color component(between 0 and 1).</param>
		public void Color(float red, float green, float blue)
		{
			getDelegateFor<glColor3f>(ref glColor3fDelegate)(red, green, blue);
		}

		/// <summary>
		/// Sets the current color to 'v'.
		/// </summary>
		/// <param name="v">An array of either 3 or 4 float values.</param>
		public void Color(float[] v)
		{
			if(v.Length == 3)
				getDelegateFor<glColor3fv>(ref glColor3fvDelegate)(v);
			else if(v.Length == 4)
				getDelegateFor<glColor4fv>(ref glColor4fvDelegate)(v);
		}

        /// <summary>
        /// Sets the current color to 'v'.
        /// </summary>
        /// <param name="v">An array of either 3 or 4 int values.</param>
        public void Color(int[] v)
        {
            if(v.Length == 3)
                getDelegateFor<glColor3iv>(ref glColor3ivDelegate)(v);
            else if(v.Length == 4)
                getDelegateFor<glColor4iv>(ref glColor4ivDelegate)(v);
        }

        /// <summary>
        /// Sets the current color to 'v'.
        /// </summary>
        /// <param name="v">An array of either 3 or 4 int values.</param>
        public void Color(short[] v)
        {
            if(v.Length == 3)
                getDelegateFor<glColor3sv>(ref glColor3svDelegate)(v);
            else if(v.Length == 4)
                getDelegateFor<glColor4sv>(ref glColor4svDelegate)(v);
        }

        /// <summary>
        /// Sets the current color to 'v'.
        /// </summary>
        /// <param name="v">An array of either 3 or 4 double values.</param>
        public void Color(double[] v)
        {
            if(v.Length == 3)
                getDelegateFor<glColor3dv>(ref glColor3dvDelegate)(v);
            else if(v.Length == 4)
                getDelegateFor<glColor4dv>(ref glColor4dvDelegate)(v);
        }

        /// <summary>
        /// Sets the current color to 'v'.
        /// </summary>
        /// <param name="v">An array of either 3 or 4 byte values.</param>
        public void Color(byte[] v)
        {
            if(v.Length == 3)
                getDelegateFor<glColor3ubv>(ref glColor3ubvDelegate)(v);
            else if(v.Length == 4)
                getDelegateFor<glColor4ubv>(ref glColor4ubvDelegate)(v);
        }

        /// <summary>
        /// Sets the current color to 'v'.
        /// </summary>
        /// <param name="v">An array of either 3 or 4 unsigned int values.</param>
        public void Color(uint[] v) 
        {
            if(v.Length == 3)
                getDelegateFor<glColor3uiv>(ref glColor3uivDelegate)(v);
            else if(v.Length == 4)
                getDelegateFor<glColor4uiv>(ref glColor4uivDelegate)(v);
        }

        /// <summary>
        /// Sets the current color to 'v'.
        /// </summary>
        /// <param name="v">An array of either 3 or 4 unsigned short values.</param>
        public void Color(ushort[] v)
        {
            if(v.Length == 3)
                getDelegateFor<glColor3usv>(ref glColor3usvDelegate)(v);
            else if(v.Length == 4)
                getDelegateFor<glColor4usv>(ref glColor4usvDelegate)(v);
        }

		/// <summary>
		/// Sets the current color.
		/// </summary>
		/// <param name="red">Red color component(between 0 and 1).</param>
		/// <param name="green">Green color component(between 0 and 1).</param>
		/// <param name="blue">Blue color component(between 0 and 1).</param>
		public void Color(int red, int green, int blue)
		{
			getDelegateFor<glColor3i>(ref glColor3iDelegate)(red, green, blue);
		}

        /// <summary>
        /// Sets the current color.
        /// </summary>
        /// <param name="red">Red color component(between 0 and 1).</param>
        /// <param name="green">Green color component(between 0 and 1).</param>
        /// <param name="blue">Blue color component(between 0 and 1).</param>
        /// <param name="alpha">Alpha color component.</param>
        public void Color(int red, int green, int blue, int alpha)
        {
            getDelegateFor<glColor4i>(ref glColor4iDelegate)(red, green, blue, alpha);
        }

		/// <summary>
		/// Sets the current color.
		/// </summary>
		/// <param name="red">Red color component(between 0 and 1).</param>
		/// <param name="green">Green color component(between 0 and 1).</param>
		/// <param name="blue">Blue color component(between 0 and 1).</param>
		public void Color(short red, short green, short blue)
		{
			getDelegateFor<glColor3s>(ref glColor3sDelegate)(red, green, blue);
		}

        /// <summary>
        /// Sets the current color.
        /// </summary>
        /// <param name="red">Red color component(between 0 and 1).</param>
        /// <param name="green">Green color component(between 0 and 1).</param>
        /// <param name="blue">Blue color component(between 0 and 1).</param>
        /// <param name="alpha">Alpha color component.</param>
        public void Color(short red, short green, short blue, short alpha)
        {
            getDelegateFor<glColor4s>(ref glColor4sDelegate)(red, green, blue, alpha);
        }

		/// <summary>
		/// Sets the current color.
		/// </summary>
		/// <param name="red">Red color component(between 0 and 1).</param>
		/// <param name="green">Green color component(between 0 and 1).</param>
		/// <param name="blue">Blue color component(between 0 and 1).</param>
		public void Color(uint red, uint green, uint blue)
		{
			getDelegateFor<glColor3ui>(ref glColor3uiDelegate)(red, green, blue);
		}

        /// <summary>
        /// Sets the current color.
        /// </summary>
        /// <param name="red">Red color component(between 0 and 1).</param>
        /// <param name="green">Green color component(between 0 and 1).</param>
        /// <param name="blue">Blue color component(between 0 and 1).</param>
        /// <param name="alpha">Alpha color component.</param>
        public void Color(uint red, uint green, uint blue, uint alpha)
        {
            getDelegateFor<glColor4ui>(ref glColor4uiDelegate)(red, green, blue, alpha);
        }

		/// <summary>
		/// Sets the current color.
		/// </summary>
		/// <param name="red">Red color component(between 0 and 1).</param>
		/// <param name="green">Green color component(between 0 and 1).</param>
		/// <param name="blue">Blue color component(between 0 and 1).</param>
		public void Color(ushort red, ushort green, ushort blue)
		{
			getDelegateFor<glColor3us>(ref glColor3usDelegate)(red, green, blue);
		}

        /// <summary>
        /// Sets the current color.
        /// </summary>
        /// <param name="red">Red color component(between 0 and 1).</param>
        /// <param name="green">Green color component(between 0 and 1).</param>
        /// <param name="blue">Blue color component(between 0 and 1).</param>
        /// <param name="alpha">Alpha color component.</param>
        public void Color(ushort red, ushort green, ushort blue, ushort alpha)
        {
            getDelegateFor<glColor4us>(ref glColor4usDelegate)(red, green, blue, alpha);
        }

		/// <summary>
		/// Sets the current color.
		/// </summary>
		/// <param name="red">Red color component(between 0 and 1).</param>
		/// <param name="green">Green color component(between 0 and 1).</param>
		/// <param name="blue">Blue color component(between 0 and 1).</param>
		/// <param name="alpha">Alpha color component(between 0 and 1).</param>
		public void Color(float red, float green, float blue, float alpha)
		{
			getDelegateFor<glColor4f>(ref glColor4fDelegate)(red, green, blue, alpha);
		}

		/// <summary>
		/// This function sets the current colour mask.
		/// </summary>
		/// <param name="red">Red component mask.</param>
		/// <param name="green">Green component mask.</param>
		/// <param name="blue">Blue component mask.</param>
		/// <param name="alpha">Alpha component mask.</param>
		public void ColorMask(byte red, byte green, byte blue, byte alpha)
		{
			getDelegateFor<glColorMask>(ref glColorMaskDelegate)(red, green, blue, alpha);
		}

        /// <summary>
        /// Cause a material color to track the current color.
        /// </summary>
        /// <param name="face">Specifies whether front, back, or both front and back material parameters should track the current color. Accepted values are OpenGL.FRONT, OpenGL.BACK, and OpenGL.FRONT_AND_BACK. The initial value is OpenGL.FRONT_AND_BACK.</param>
        /// <param name="mode">Specifies which	of several material parameters track the current color. Accepted values are	OpenGL.EMISSION, OpenGL.AMBIENT, OpenGL.DIFFUSE, OpenGL.SPECULAR and OpenGL.AMBIENT_AND_DIFFUSE. The initial value is OpenGL.AMBIENT_AND_DIFFUSE.</param>
		public void ColorMaterial(uint face, uint mode)
        {
            getDelegateFor<glColorMaterial>(ref glColorMaterialDelegate)(face, mode);
        }

        /// <summary>
        /// Define an array of colors.
        /// </summary>
        /// <param name="size">Specifies the number	of components per color. Must be 3 or 4.</param>
        /// <param name="type">Specifies the data type of each color component in the array. Symbolic constants OpenGL.BYTE, OpenGL.UNSIGNED_BYTE, OpenGL.SHORT, OpenGL.UNSIGNED_SHORT, OpenGL.INT, OpenGL.UNSIGNED_INT, OpenGL.FLOAT and OpenGL.DOUBLE are accepted.</param>
        /// <param name="stride">Specifies the byte offset between consecutive colors. If stride is 0,(the initial value), the colors are understood to be tightly packed in the array.</param>
        /// <param name="pointer">Specifies a pointer to the first component of the first color element in the array.</param>
		public void ColorPointer(int size, uint type, int stride, IntPtr pointer)
        {
            getDelegateFor<glColorPointer>(ref glColorPointerDelegate)(size, type, stride, pointer);
        }

        /// <summary>
        /// Define an array of colors.
        /// </summary>
        /// <param name="size">Specifies the number	of components per color. Must be 3 or 4.</param>
        /// <param name="stride">Specifies the byte offset between consecutive colors. If stride is 0,(the initial value), the colors are understood to be tightly packed in the array.</param>
        /// <param name="pointer">The array.</param>
        public void ColorPointer(int size, int stride, byte[] pointer)
        {
            var pinned = GCHandle.Alloc(pointer, GCHandleType.Pinned);
            getDelegateFor<glColorPointer>(ref glColorPointerDelegate)(size, GL_UNSIGNED_BYTE, stride, pinned.AddrOfPinnedObject());
            pinned.Free();
        }

        /// <summary>
        /// Define an array of colors.
        /// </summary>
        /// <param name="size">Specifies the number	of components per color. Must be 3 or 4.</param>
        /// <param name="stride">Specifies the byte offset between consecutive colors. If stride is 0,(the initial value), the colors are understood to be tightly packed in the array.</param>
        /// <param name="pointer">The array.</param>
        public void ColorPointer(int size, int stride, float[] pointer)
        {
            var pinned = GCHandle.Alloc(pointer, GCHandleType.Pinned);
            getDelegateFor<glColorPointer>(ref glColorPointerDelegate)(size, GL_FLOAT, stride, pinned.AddrOfPinnedObject());
            pinned.Free();
        }

        /// <summary>
        /// Copy pixels in	the frame buffer.
        /// </summary>
        /// <param name="x">Specify the window coordinates of the lower left corner of the rectangular region of pixels to be copied.</param>
        /// <param name="y">Specify the window coordinates of the lower left corner of the rectangular region of pixels to be copied.</param>
        /// <param name="width">Specify the dimensions of the rectangular region of pixels to be copied. Both must be nonnegative.</param>
        /// <param name="height">Specify the dimensions of the rectangular region of pixels to be copied. Both must be nonnegative.</param>
        /// <param name="type">Specifies whether color values, depth values, or stencil values are to be copied. Symbolic constants OpenGL.COLOR, OpenGL.DEPTH, and OpenGL.STENCIL are accepted.</param>
		public void CopyPixels(int x, int y, int width, int height, uint type)
        {
            getDelegateFor<glCopyPixels>(ref glCopyPixelsDelegate)(x, y, width, height, type);
        }

        /// <summary>
        /// Copy pixels into a 1D texture image.
        /// </summary>
        /// <param name="target">Specifies the target texture. Must be OpenGL.TEXTURE_1D.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="internalFormat">Specifies the internal format of the texture.</param>
        /// <param name="x">Specify the window coordinates of the left corner of the row of pixels to be copied.</param>
        /// <param name="y">Specify the window coordinates of the left corner of the row of pixels to be copied.</param>
        /// <param name="width">Specifies the width of the texture image. Must be 0 or 2^n =(2 * border) for some integer n. The height of the texture image is 1.</param>
        /// <param name="border">Specifies the width of the border. Must be either 0 or 1.</param>
		public void CopyTexImage1D(uint target, int level, uint internalFormat, int x, int y, int width, int border)
        {
            getDelegateFor<glCopyTexImage1D>(ref glCopyTexImage1DDelegate)(target, level, internalFormat, x, y, width, border);
        }

        /// <summary>
        /// Copy pixels into a	2D texture image.
        /// </summary>
        /// <param name="target">Specifies the target texture. Must be OpenGL.TEXTURE_2D.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="internalFormat">Specifies the internal format of the texture.</param>
        /// <param name="x">Specify the window coordinates of the left corner of the row of pixels to be copied.</param>
        /// <param name="y">Specify the window coordinates of the left corner of the row of pixels to be copied.</param>
        /// <param name="width">Specifies the width of the texture image.</param>
        /// <param name="height">Specifies the height of the texture image.</param>
        /// <param name="border">Specifies the width of the border. Must be either 0 or 1.</param>
		public void CopyTexImage2D(uint target, int level, uint internalFormat, int x, int y, int width, int height, int border)
        {
            getDelegateFor<glCopyTexImage2D>(ref glCopyTexImage2DDelegate)(target, level, internalFormat, x, y, width, height, border);
        }

        /// <summary>
        /// Copy a one-dimensional texture subimage.
        /// </summary>
        /// <param name="target">Specifies the target texture. Must be OpenGL.TEXTURE_1D.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xoffset">Specifies the texel offset within the texture array.</param>
        /// <param name="x">Specify the window coordinates of the left corner of the row of pixels to be copied.</param>
        /// <param name="y">Specify the window coordinates of the left corner of the row of pixels to be copied.</param>
        /// <param name="width">Specifies the width of the texture image.</param>
		public void CopyTexSubImage1D(uint target, int level, int xoffset, int x, int y, int width)
        {
            getDelegateFor<glCopyTexSubImage1D>(ref glCopyTexSubImage1DDelegate)(target, level, xoffset, x, y, width);
        }

        /// <summary>
        /// Copy a two-dimensional texture subimage.
        /// </summary>
        /// <param name="target">Specifies the target texture. Must be OpenGL.TEXTURE_2D.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xoffset">Specifies the texel offset within the texture array.</param>
        /// <param name="yoffset">Specifies the texel offset within the texture array.</param>
        /// <param name="x">Specify the window coordinates of the left corner of the row of pixels to be copied.</param>
        /// <param name="y">Specify the window coordinates of the left corner of the row of pixels to be copied.</param>
        /// <param name="width">Specifies the width of the texture image.</param>
        /// <param name="height">Specifies the height of the texture image.</param>
		public void CopyTexSubImage2D(uint target, int level, int xoffset, int yoffset, int x, int y, int width, int height)
        {
            getDelegateFor<glCopyTexSubImage2D>(ref glCopyTexSubImage2DDelegate)(target, level, xoffset, yoffset, x, y, width, height);
        }

        /// <summary>
        /// Specify whether front- or back-facing facets can be culled.
        /// </summary>
        /// <param name="mode">Specifies whether front- or back-facing facets are candidates for culling. Symbolic constants OpenGL.FRONT, OpenGL.BACK, and OpenGL.FRONT_AND_BACK are accepted. The initial	value is OpenGL.BACK.</param>
		public void CullFace(uint mode)
        {
            getDelegateFor<glCullFace>(ref glCullFaceDelegate)(mode);
        }

		/// <summary>
		/// This function deletes a list, or a range of lists.
		/// </summary>
		/// <param name="list">The list to delete.</param>
		/// <param name="range">The range of lists(often just 1).</param>
		public void DeleteLists(uint list, int range)
		{
			getDelegateFor<glDeleteLists>(ref glDeleteListsDelegate)(list, range);
		}

		/// <summary>
		/// This function deletes a set of Texture objects.
		/// </summary>
		/// <param name="n">Number of textures to delete.</param>
		/// <param name="textures">The array containing the names of the textures to delete.</param>
		public void DeleteTextures(int n, uint[] textures)
		{
			getDelegateFor<glDeleteTextures>(ref glDeleteTexturesDelegate)(n, textures);
		}

		/// <summary>
		/// This function sets the current depth buffer comparison function, the default it LESS.
		/// </summary>
		/// <param name="func">The comparison function to set.</param>
		public void DepthFunc(uint func)
		{
			getDelegateFor<glDepthFunc>(ref glDepthFuncDelegate)(func);
		}
        
		/// <summary>
		/// This function sets the current depth buffer comparison function, the default it LESS.
		/// </summary>
        /// <param name="function">The comparison function to set.</param>
        public void DepthFunc(Enumerations.DepthFunction function)
		{
            getDelegateFor<glDepthFunc>(ref glDepthFuncDelegate)((uint)function);
		}
        

		/// <summary>
		/// This function sets the depth mask.
		/// </summary>
		/// <param name="flag">The depth mask flag, normally 1.</param>
		public void DepthMask(byte flag)
		{
			getDelegateFor<glDepthMask>(ref glDepthMaskDelegate)(flag);
		}

        /// <summary>
        /// Specify mapping of depth values from normalized device coordinates	to window coordinates.
        /// </summary>
        /// <param name="zNear">Specifies the mapping of the near clipping plane to window coordinates. The initial value is 0.</param>
        /// <param name="zFar">Specifies the mapping of the near clipping plane to window coordinates. The initial value is 1.</param>
		public void DepthRange(double zNear, double zFar)
        {
            getDelegateFor<glDepthRange>(ref glDepthRangeDelegate)(zNear, zFar);
        }

		/// <summary>
		/// Call this function to disable an OpenGL capability.
		/// </summary>
		/// <param name="cap">The capability to disable.</param>
		public void Disable(uint cap)
		{
			getDelegateFor<glDisable>(ref glDisableDelegate)(cap);
		}

		/// <summary>
		/// This function disables a client state array, such as a vertex array.
		/// </summary>
		/// <param name="array">The array to disable.</param>
		public void DisableClientState(uint array)
		{
			getDelegateFor<glDisableClientState>(ref glDisableClientStateDelegate)(array);
		}

        /// <summary>
        /// Render	primitives from	array data.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render. Symbolic constants OpenGL.POINTS, OpenGL.LINE_STRIP, OpenGL.LINE_LOOP, OpenGL.LINES, OpenGL.TRIANGLE_STRIP, OpenGL.TRIANGLE_FAN, OpenGL.TRIANGLES, OpenGL.QUAD_STRIP, OpenGL.QUADS, and OpenGL.POLYGON are accepted.</param>
        /// <param name="first">Specifies the starting	index in the enabled arrays.</param>
        /// <param name="count">Specifies the number of indices to be rendered.</param>
		public void DrawArrays(uint mode, int first, int count)
        {
            getDelegateFor<glDrawArrays>(ref glDrawArraysDelegate)(mode, first, count);
        }

        /// <summary>
        /// Specify which color buffers are to be drawn into.
        /// </summary>
        /// <param name="mode">Specifies up to	four color buffers to be drawn into. Symbolic constants OpenGL.NONE, OpenGL.FRONT_LEFT, OpenGL.FRONT_RIGHT,	OpenGL.BACK_LEFT, OpenGL.BACK_RIGHT, OpenGL.FRONT, OpenGL.BACK, OpenGL.LEFT, OpenGL.RIGHT, OpenGL.FRONT_AND_BACK, and OpenGL.AUXi, where i is between 0 and(OpenGL.AUX_BUFFERS - 1), are accepted(OpenGL.AUX_BUFFERS is not the upper limit; use glGet to query the number of	available aux buffers.)  The initial value is OpenGL.FRONT for single- buffered contexts, and OpenGL.BACK for double-buffered contexts.</param>
		public void DrawBuffer(uint mode)
        {
            getDelegateFor<glDrawBuffer>(ref glDrawBufferDelegate)(mode);
        }

        /// <summary>
        /// Specify which color buffers are to be drawn into.
        /// </summary>
        /// <param name="drawBufferMode">Specifies up to	four color buffers to be drawn into.</param>
        public void DrawBuffer(Enumerations.DrawBufferMode drawBufferMode)
        {
            getDelegateFor<glDrawBuffer>(ref glDrawBufferDelegate)((uint)drawBufferMode);
        }

        /// <summary>
        /// Render primitives from array data. Uses OpenGL.GL_UNSIGNED_INT as the data type.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to	render. Symbolic constants OpenGL.POINTS, OpenGL.LINE_STRIP, OpenGL.LINE_LOOP, OpenGL.LINES, OpenGL.TRIANGLE_STRIP, OpenGL.TRIANGLE_FAN, OpenGL.TRIANGLES, OpenGL.QUAD_STRIP, OpenGL.QUADS, and OpenGL.POLYGON are accepted.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="indices">Specifies a pointer to the location where the indices are stored.</param>
        public void DrawElements(uint mode, int count, uint[] indices)
        {
            var pinned = GCHandle.Alloc(indices, GCHandleType.Pinned);
            getDelegateFor<glDrawElements>(ref glDrawElementsDelegate)(mode, count, GL_UNSIGNED_INT, pinned.AddrOfPinnedObject());
            pinned.Free();
        }

        /// <summary>
        /// Render primitives from array data.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to	render. Symbolic constants OpenGL.GL_POINTS, OpenGL.GL_LINE_STRIP, OpenGL.GL_LINE_LOOP, OpenGL.GL_LINES, OpenGL.GL_TRIANGLE_STRIP, OpenGL.TRIANGLE_FAN, OpenGL.GL_TRIANGLES, OpenGL.GL_QUAD_STRIP, OpenGL.GL_QUADS, and OpenGL.GL_POLYGON are accepted.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in indices.	Must be one of OpenGL.GL_UNSIGNED_BYTE, OpenGL.GL_UNSIGNED_SHORT, or OpenGL.GL_UNSIGNED_INT.</param>
        /// <param name="indices">Specifies a pointer to the location where the indices are stored.</param>
        public void DrawElements(uint mode, int count, uint type, IntPtr indices)
        {
            getDelegateFor<glDrawElements>(ref glDrawElementsDelegate)(mode, count, type, indices);
        }

        /// <summary>
        /// Draws a rectangle of pixel data at the current raster position.
        /// </summary>
        /// <param name="width">Width of pixel data.</param>
        /// <param name="height">Height of pixel data.</param>
        /// <param name="format">Format of pixel data.</param>
        /// <param name="pixels">Pixel data buffer.</param>
        public void DrawPixels(int width, int height, uint format, float[] pixels)
        {
            var pinned = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            getDelegateFor<glDrawPixels>(ref glDrawPixelsDelegate)(width, height, format, GL_FLOAT, pinned.AddrOfPinnedObject());
            pinned.Free();
        }

        /// <summary>
        /// Draws a rectangle of pixel data at the current raster position.
        /// </summary>
        /// <param name="width">Width of pixel data.</param>
        /// <param name="height">Height of pixel data.</param>
        /// <param name="format">Format of pixel data.</param>
        /// <param name="pixels">Pixel data buffer.</param>
        public void DrawPixels(int width, int height, uint format, uint[] pixels)
        {
            var pinned = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            getDelegateFor<glDrawPixels>(ref glDrawPixelsDelegate)(width, height, format, GL_UNSIGNED_INT, pinned.AddrOfPinnedObject());
            pinned.Free();
        }

        /// <summary>
        /// Draws a rectangle of pixel data at the current raster position.
        /// </summary>
        /// <param name="width">Width of pixel data.</param>
        /// <param name="height">Height of pixel data.</param>
        /// <param name="format">Format of pixel data.</param>
        /// <param name="pixels">Pixel data buffer.</param>
        public void DrawPixels(int width, int height, uint format, ushort[] pixels)
        {
            var pinned = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            getDelegateFor<glDrawPixels>(ref glDrawPixelsDelegate)(width, height, format, GL_UNSIGNED_SHORT, pinned.AddrOfPinnedObject());
            pinned.Free();
        }

        /// <summary>
        /// Draws a rectangle of pixel data at the current raster position.
        /// </summary>
        /// <param name="width">Width of pixel data.</param>
        /// <param name="height">Height of pixel data.</param>
        /// <param name="format">Format of pixel data.</param>
        /// <param name="pixels">Pixel data buffer.</param>
        public void DrawPixels(int width, int height, uint format, byte[] pixels)
        {
            var pinned = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            getDelegateFor<glDrawPixels>(ref glDrawPixelsDelegate)(width, height, format, GL_UNSIGNED_BYTE, pinned.AddrOfPinnedObject());
            pinned.Free();
        }

        /// <summary>
        /// Draws a rectangle of pixel data at the current raster position.
        /// </summary>
        /// <param name="width">Width of pixel data.</param>
        /// <param name="height">Height of pixel data.</param>
        /// <param name="format">Format of pixel data.</param>
        /// <param name="type">The GL data type.</param>
        /// <param name="pixels">Pixel data buffer.</param>
        public void DrawPixels(int width, int height, uint format, uint type, IntPtr pixels)
        {
            getDelegateFor<glDrawPixels>(ref glDrawPixelsDelegate)(width, height, format, type, pixels);
        }

        /// <summary>
        /// Flag edges as either boundary or nonboundary.
        /// </summary>
        /// <param name="flag">Specifies the current edge flag	value, either OpenGL.TRUE or OpenGL.FALSE. The initial value is OpenGL.TRUE.</param>
		public void EdgeFlag(byte flag)
        {
            getDelegateFor<glEdgeFlag>(ref glEdgeFlagDelegate)(flag);
        }

        /// <summary>
        /// Define an array of edge flags.
        /// </summary>
        /// <param name="stride">Specifies the byte offset between consecutive edge flags. If stride is	0(the initial value), the edge	flags are understood to	be tightly packed in the array.</param>
        /// <param name="pointer">Specifies a pointer to the first edge flag in the array.</param>
		public void EdgeFlagPointer(int stride, int[] pointer)
        {
            getDelegateFor<glEdgeFlagPointer>(ref glEdgeFlagPointerDelegate)(stride, pointer);
        }

        /// <summary>
        /// Flag edges as either boundary or nonboundary.
        /// </summary>
        /// <param name="flag">Specifies a pointer to an array that contains a single boolean element,	which replaces the current edge	flag value.</param>
		public void EdgeFlag(byte[] flag)
        {
            getDelegateFor<glEdgeFlagv>(ref glEdgeFlagvDelegate)(flag);
        }

		/// <summary>
		/// Call this function to enable an OpenGL capability.
		/// </summary>
		/// <param name="cap">The capability you wish to enable.</param>
		public void Enable(uint cap)
		{
			getDelegateFor<glEnable>(ref glEnableDelegate)(cap);
		}

		/// <summary>
		/// This function enables one of the client state arrays, such as a vertex array.
		/// </summary>
		/// <param name="array">The array to enable.</param>
		public void EnableClientState(uint array)
		{
			getDelegateFor<glEnableClientState>(ref glEnableClientStateDelegate)(array);
		}

		/// <summary>
		/// This is not an imported OpenGL function, but very useful. If 'test' is
		/// true, cap is enabled, otherwise, it's disable.
		/// </summary>
		/// <param name="cap">The capability you want to enable.</param>
		/// <param name="test">The logical comparison.</param>
		public void EnableIf(uint cap, bool test)
		{
			if(test)	Enable(cap);
			else		Disable(cap);
		}

		/// <summary>
		/// Signals the End of drawing.
		/// </summary>
		public void End()
		{
			getDelegateFor<glEnd>(ref glEndDelegate)();
		}

		/// <summary>
		/// Ends the current display list compilation.
		/// </summary>
		public void EndList()
		{
			getDelegateFor<glEndList>(ref glEndListDelegate)();
		}

		/// <summary>
		/// Evaluate from the current evaluator.
		/// </summary>
		/// <param name="u">Domain coordinate.</param>
		public void EvalCoord1(double u)
		{
			getDelegateFor<glEvalCoord1d>(ref glEvalCoord1dDelegate)(u);
		}

        /// <summary>
        /// Evaluate from the current evaluator.
        /// </summary>
        /// <param name="u">Domain coordinate.</param>
		public void EvalCoord1(double[] u)
        {
            getDelegateFor<glEvalCoord1dv>(ref glEvalCoord1dvDelegate)(u);
        }

		/// <summary>
		/// Evaluate from the current evaluator.
		/// </summary>
		/// <param name="u">Domain coordinate.</param>
		public void EvalCoord1(float u)
		{
			getDelegateFor<glEvalCoord1f>(ref glEvalCoord1fDelegate)(u);
		}

        /// <summary>
        /// Evaluate from the current evaluator.
        /// </summary>
        /// <param name="u">Domain coordinate.</param>
		public void EvalCoord1(float[] u)
        {
            getDelegateFor<glEvalCoord1fv>(ref glEvalCoord1fvDelegate)(u);
        }

        /// <summary>
        /// Evaluate from the current evaluator.
        /// </summary>
        /// <param name="u">Domain coordinate.</param>
        /// <param name="v">Domain coordinate.</param>
		public void EvalCoord2(double u, double v)
        {
            getDelegateFor<glEvalCoord2d>(ref glEvalCoord2dDelegate)(u, v);
        }

        /// <summary>
        /// Evaluate from the current evaluator.
        /// </summary>
        /// <param name="u">Domain coordinate.</param>
        public void EvalCoord2(double[] u)
        {
            getDelegateFor<glEvalCoord2dv>(ref glEvalCoord2dvDelegate)(u);
        }

        /// <summary>
        /// Evaluate from the current evaluator.
        /// </summary>
        /// <param name="u">Domain coordinate.</param>
        /// <param name="v">Domain coordinate.</param>
        public void EvalCoord2(float u, float v)
        {
            getDelegateFor<glEvalCoord2f>(ref glEvalCoord2fDelegate)(u, v);
        }

        /// <summary>
        /// Evaluate from the current evaluator.
        /// </summary>
        /// <param name="u">Domain coordinate.</param>
        public void EvalCoord2(float[] u)
        {
            getDelegateFor<glEvalCoord2fv>(ref glEvalCoord2fvDelegate)(u);
        }

		/// <summary>
		/// Evaluates a 'mesh' from the current evaluators.
		/// </summary>
		/// <param name="mode">Drawing mode, can be POINT or LINE.</param>
		/// <param name="i1">Beginning of range.</param>
		/// <param name="i2">End of range.</param>
		public void EvalMesh1(uint mode, int i1, int i2)
		{
			getDelegateFor<glEvalMesh1>(ref glEvalMesh1Delegate)(mode, i1, i2);
		}
		/// <summary>
		/// Evaluates a 'mesh' from the current evaluators.
		/// </summary>
		/// <param name="mode">Drawing mode, fill, point or line.</param>
		/// <param name="i1">Beginning of range.</param>
		/// <param name="i2">End of range.</param>
		/// <param name="j1">Beginning of range.</param>
		/// <param name="j2">End of range.</param>
		public void EvalMesh2(uint mode, int i1, int i2, int j1, int j2)
		{
			getDelegateFor<glEvalMesh2>(ref glEvalMesh2Delegate)(mode, i1, i2, j1, j2);
		}

        /// <summary>
        /// Generate and evaluate a single point in a mesh.
        /// </summary>
        /// <param name="i">The integer value for grid domain variable i.</param>
		public void EvalPoint1(int i)
        {
            getDelegateFor<glEvalPoint1>(ref glEvalPoint1Delegate)(i);
        }

        /// <summary>
        /// Generate and evaluate a single point in a mesh.
        /// </summary>
        /// <param name="i">The integer value for grid domain variable i.</param>
        /// <param name="j">The integer value for grid domain variable j.</param>
		public void EvalPoint2(int i, int j)
        {
            getDelegateFor<glEvalPoint2>(ref glEvalPoint2Delegate)(i, j);
        }

		/// <summary>
		/// This function sets the feedback buffer, that will receive feedback data.
		/// </summary>
		/// <param name="size">Size of the buffer.</param>
		/// <param name="type">Type of data in the buffer.</param>
		/// <param name="buffer">The buffer itself.</param>
		public void FeedbackBuffer(int size, uint type, float[] buffer)
		{
			getDelegateFor<glFeedbackBuffer>(ref glFeedbackBufferDelegate)(size, type, buffer);
		}

		/// <summary>
		/// This function is similar to flush, but in a sense does it more, as it
		/// executes all commands aon both the client and the server.
		/// </summary>
		public void Finish()
		{
			getDelegateFor<glFinish>(ref glFinishDelegate)();
		}

		/// <summary>
		/// This forces OpenGL to execute any commands you have given it.
		/// </summary>
		public void Flush()
		{
			getDelegateFor<glFlush>(ref glFlushDelegate)();
		}

		/// <summary>
		/// Sets a fog parameter.
		/// </summary>
		/// <param name="pname">The parameter to set.</param>
		/// <param name="param">The value to set it to.</param>
		public void Fog(uint pname, float param)
		{
			getDelegateFor<glFogf>(ref glFogfDelegate)(pname, param);
		}

		/// <summary>
		/// Sets a fog parameter.
		/// </summary>
		/// <param name="pname">The parameter to set.</param>
		/// <param name="parameters">The values to set it to.</param>
		public void Fog(uint pname, float[] parameters)
		{
			getDelegateFor<glFogfv>(ref glFogfvDelegate)(pname, parameters);
		}

		/// <summary>
		/// Sets a fog parameter.
		/// </summary>
		/// <param name="pname">The parameter to set.</param>
		/// <param name="param">The value to set it to.</param>
		public void Fog(uint pname, int param)
		{
			getDelegateFor<glFogi>(ref glFogiDelegate)(pname, param);
		}

		/// <summary>
		/// Sets a fog parameter.
		/// </summary>
		/// <param name="pname">The parameter to set.</param>
		/// <param name="parameters">The values to set it to.</param>
		public void Fog(uint pname, int[] parameters)
		{
			getDelegateFor<glFogiv>(ref glFogivDelegate)(pname, parameters);
		}

		/// <summary>
		/// This function sets what defines a front face.
		/// </summary>
		/// <param name="mode">Winding mode, counter clockwise by default.</param>
		public void FrontFace(uint mode)
		{
			getDelegateFor<glFrontFace>(ref glFrontFaceDelegate)(mode);
		}

		/// <summary>
		/// This function creates a frustrum transformation and mulitplies it to the current
		/// matrix(which in most cases should be the projection matrix).
		/// </summary>
		/// <param name="left">Left clip position.</param>
		/// <param name="right">Right clip position.</param>
		/// <param name="bottom">Bottom clip position.</param>
		/// <param name="top">Top clip position.</param>
		/// <param name="zNear">Near clip position.</param>
		/// <param name="zFar">Far clip position.</param>
		public void Frustum(double left, double right, double bottom, 
			double top, double zNear, double zFar)
		{
			getDelegateFor<glFrustum>(ref glFrustumDelegate)(left, right, bottom, top, zNear, zFar);
		}

		/// <summary>
		/// This function generates 'range' number of contiguos display list indices.
		/// </summary>
		/// <param name="range">The number of lists to generate.</param>
		/// <returns>The first list.</returns>
		public uint GenLists(int range)
		{
			uint list = getDelegateFor<glGenLists>(ref glGenListsDelegate)(range);
			return list;
		}

		/// <summary>
		/// Create a set of unique texture names.
		/// </summary>
		/// <param name="n">Number of names to create.</param>
		/// <param name="textures">Array to store the texture names.</param>
		public void GenTextures(int n, uint[] textures)
		{
			getDelegateFor<glGenTextures>(ref glGenTexturesDelegate)(n, textures);
		}

		/// <summary>
		/// This function queries OpenGL for data, and puts it in the buffer supplied.
		/// </summary>
		/// <param name="pname">The parameter to query.</param>
		/// <param name="parameters"></param>
		public void GetBooleanv(uint pname, byte[] parameters)
		{
			getDelegateFor<glGetBooleanv>(ref glGetBooleanvDelegate)(pname, parameters);
		}

        /// <summary>
        /// This function queries OpenGL for data, and puts it in the buffer supplied.
        /// </summary>
        /// <param name="pname">The parameter to query.</param>
        /// <param name="parameters"></param>
        public void GetBooleanv(Enumerations.GetTarget pname, byte[] parameters)
        {
            getDelegateFor<glGetBooleanv>(ref glGetBooleanvDelegate)((uint)pname, parameters);
        }

        /// <summary>
        /// Return the coefficients of the specified clipping plane.
        /// </summary>
        /// <param name="plane">Specifies a	clipping plane.	 The number of clipping planes depends on the implementation, but at least six clipping planes are supported. They are identified by symbolic names of the form OpenGL.CLIP_PLANEi where 0 Less Than i Less Than OpenGL.MAX_CLIP_PLANES.</param>
        /// <param name="equation">Returns four double-precision values that are the coefficients of the plane equation of plane in eye coordinates. The initial value is(0, 0, 0, 0).</param>
		public void GetClipPlane(uint plane, double[] equation)
        {
            getDelegateFor<glGetClipPlane>(ref glGetClipPlaneDelegate)(plane, equation);
        }

		/// <summary>
		/// This function queries OpenGL for data, and puts it in the buffer supplied.
		/// </summary>
		/// <param name="pname">The parameter to query.</param>
		/// <param name="parameters">The buffer to put that data into.</param>
		public void GetDouble(uint pname, double[] parameters)
		{
			getDelegateFor<glGetDoublev>(ref glGetDoublevDelegate)(pname, parameters);
		}

        /// <summary>
        /// This function queries OpenGL for data, and puts it in the buffer supplied.
        /// </summary>
        /// <param name="pname">The parameter to query.</param>
        /// <param name="parameters">The buffer to put that data into.</param>
        public void GetDouble(Enumerations.GetTarget pname, double[] parameters)
        {
            getDelegateFor<glGetDoublev>(ref glGetDoublevDelegate)((uint)pname, parameters);
        }

		/// <summary>
		/// Get the current OpenGL error code.
		/// </summary>
		/// <returns>The current OpenGL error code.</returns>
		public uint GetError()
		{
			return getDelegateFor<glGetError>(ref glGetErrorDelegate)();
		}

        /// <summary>
        /// Get the current OpenGL error code.
        /// </summary>
        /// <returns>The current OpenGL error code.</returns>
        public Enumerations.ErrorCode GetErrorCode()
        {
            return(Enumerations.ErrorCode)getDelegateFor<glGetError>(ref glGetErrorDelegate)();
        }

		/// <summary>
		/// This this function to query OpenGL values.
		/// </summary>
		/// <param name="pname">The parameter to query.</param>
		/// <param name="parameters">The parameters</param>
		public void GetFloat(uint pname, float[] parameters)
		{
			getDelegateFor<glGetFloatv>(ref glGetFloatvDelegate)(pname, parameters);
		}

        /// <summary>
        /// This this function to query OpenGL values.
        /// </summary>
        /// <param name="pname">The parameter to query.</param>
        /// <param name="parameters">The parameters</param>
        public void GetFloat(Enumerations.GetTarget pname, float[] parameters)
        {
            getDelegateFor<glGetFloatv>(ref glGetFloatvDelegate)((uint)pname, parameters);
        }

		/// <summary>
		/// Use this function to query OpenGL parameter values.
		/// </summary>
		/// <param name="pname">The Parameter to query</param>
		/// <param name="parameters">An array to put the values into.</param>
		public void GetInteger(uint pname, int[] parameters)
		{
			getDelegateFor<glGetIntegerv>(ref glGetIntegervDelegate)(pname, parameters);
		}

        /// <summary>
        /// Use this function to query OpenGL parameter values.
        /// </summary>
        /// <param name="pname">The Parameter to query</param>
        /// <param name="parameters">An array to put the values into.</param>
        public void GetInteger(Enumerations.GetTarget pname, int[] parameters)
        {
            getDelegateFor<glGetIntegerv>(ref glGetIntegervDelegate)((uint)pname, parameters);
        }

        /// <summary>
        /// Return light source parameter values.
        /// </summary>
        /// <param name="light">Specifies a light source. The number of possible lights depends on the implementation, but at least eight lights are supported. They are identified by symbolic names of the form OpenGL.LIGHTi where i ranges from 0 to the value of OpenGL.GL_MAX_LIGHTS - 1.</param>
        /// <param name="pname">Specifies a light source parameter for light.</param>
        /// <param name="parameters">Returns the requested data.</param>
		public void GetLight(uint light, uint pname, float[] parameters)
        {
            getDelegateFor<glGetLightfv>(ref glGetLightfvDelegate)(light, pname, parameters);
        }

        /// <summary>
        /// Return light source parameter values.
        /// </summary>
        /// <param name="light">Specifies a light source. The number of possible lights depends on the implementation, but at least eight lights are supported. They are identified by symbolic names of the form OpenGL.LIGHTi where i ranges from 0 to the value of OpenGL.GL_MAX_LIGHTS - 1.</param>
        /// <param name="pname">Specifies a light source parameter for light.</param>
        /// <param name="parameters">Returns the requested data.</param>
        public void GetLight(uint light, uint pname, int[] parameters)
        {
            getDelegateFor<glGetLightiv>(ref glGetLightivDelegate)(light, pname, parameters);
        }

        /// <summary>
        /// Return evaluator parameters.
        /// </summary>
        /// <param name="target">Specifies the symbolic name of a map.</param>
        /// <param name="query">Specifies which parameter to return.</param>
        /// <param name="v">Returns the requested data.</param>
		public void GetMap(uint target, uint query, double[] v)
        {
            getDelegateFor<glGetMapdv>(ref glGetMapdvDelegate)(target, query, v);
        }

        /// <summary>
        /// Return evaluator parameters.
        /// </summary>
        /// <param name="target">Specifies the symbolic name of a map.</param>
        /// <param name="query">Specifies which parameter to return.</param>
        /// <param name="v">Returns the requested data.</param>
        public void GetMap(Enumerations.GetMapTarget target, uint query, double[] v)
        {
            getDelegateFor<glGetMapdv>(ref glGetMapdvDelegate)((uint)target, query, v);
        }

        /// <summary>
        /// Return evaluator parameters.
        /// </summary>
        /// <param name="target">Specifies the symbolic name of a map.</param>
        /// <param name="query">Specifies which parameter to return.</param>
        /// <param name="v">Returns the requested data.</param>
        public void GetMap(Enumerations.GetMapTarget target, uint query, float[] v)
        {
            getDelegateFor<glGetMapfv>(ref glGetMapfvDelegate)((uint)target, query, v);
        }

        /// <summary>
        /// Return evaluator parameters.
        /// </summary>
        /// <param name="target">Specifies the symbolic name of a map.</param>
        /// <param name="query">Specifies which parameter to return.</param>
        /// <param name="v">Returns the requested data.</param>
		public void GetMap(uint target, uint query, float[] v)
        {
            getDelegateFor<glGetMapfv>(ref glGetMapfvDelegate)(target, query, v);
        }

        /// <summary>
        /// Return evaluator parameters.
        /// </summary>
        /// <param name="target">Specifies the symbolic name of a map.</param>
        /// <param name="query">Specifies which parameter to return.</param>
        /// <param name="v">Returns the requested data.</param>
        public void GetMap(Enumerations.GetMapTarget target, uint query, int[] v)
        {
            getDelegateFor<glGetMapiv>(ref glGetMapivDelegate)((uint)target, query, v);
        }

        /// <summary>
        /// Return evaluator parameters.
        /// </summary>
        /// <param name="target">Specifies the symbolic name of a map.</param>
        /// <param name="query">Specifies which parameter to return.</param>
        /// <param name="v">Returns the requested data.</param>
		public void GetMap(uint target, uint query, int[] v)
        {
            getDelegateFor<glGetMapiv>(ref glGetMapivDelegate)(target, query, v);
        }

        /// <summary>
        /// Return material parameters.
        /// </summary>
        /// <param name="face">Specifies which of the two materials is being queried. OpenGL.FRONT or OpenGL.BACK are accepted, representing the front and back materials, respectively.</param>
        /// <param name="pname">Specifies the material parameter to return.</param>
        /// <param name="parameters">Returns the requested data.</param>
        public void GetMaterial(uint face, uint pname, float[] parameters)
        {
            getDelegateFor<glGetMaterialfv>(ref glGetMaterialfvDelegate)(face, pname, parameters);
        }

        /// <summary>
        /// Return material parameters.
        /// </summary>
        /// <param name="face">Specifies which of the two materials is being queried. OpenGL.FRONT or OpenGL.BACK are accepted, representing the front and back materials, respectively.</param>
        /// <param name="pname">Specifies the material parameter to return.</param>
        /// <param name="parameters">Returns the requested data.</param>
        public void GetMaterial(uint face, uint pname, int[] parameters)
        {
            getDelegateFor<glGetMaterialiv>(ref glGetMaterialivDelegate)(face, pname, parameters);
        }

        /// <summary>
        /// Return the specified pixel map.
        /// </summary>
        /// <param name="map">Specifies the	name of	the pixel map to return.</param>
        /// <param name="values">Returns the pixel map	contents.</param>
		public void GetPixelMap(uint map, float[] values)
        {
            getDelegateFor<glGetPixelMapfv>(ref glGetPixelMapfvDelegate)(map, values);
        }

        /// <summary>
        /// Return the specified pixel map.
        /// </summary>
        /// <param name="map">Specifies the	name of	the pixel map to return.</param>
        /// <param name="values">Returns the pixel map	contents.</param>
		public void GetPixelMap(uint map, uint[] values)
        {
            getDelegateFor<glGetPixelMapuiv>(ref glGetPixelMapuivDelegate)(map, values);
        }

        /// <summary>
        /// Return the specified pixel map.
        /// </summary>
        /// <param name="map">Specifies the	name of	the pixel map to return.</param>
        /// <param name="values">Returns the pixel map	contents.</param>
		public void GetPixelMap(uint map, ushort[] values)
        {
            getDelegateFor<glGetPixelMapusv>(ref glGetPixelMapusvDelegate)(map, values);
        }

        /// <summary>
        /// Return the address of the specified pointer.
        /// </summary>
        /// <param name="pname">Specifies the array or buffer pointer to be returned.</param>
        /// <param name="parameters">Returns the pointer value specified by parameters.</param>
		public void GetPointerv(uint pname, int[] parameters)
        {
            getDelegateFor<glGetPointerv>(ref glGetPointervDelegate)(pname, parameters);
        }

        /// <summary>
        /// Return the polygon stipple pattern.
        /// </summary>
        /// <param name="mask">Returns the stipple pattern. The initial value is all 1's.</param>
		public void GetPolygonStipple(byte[] mask)
        {
            getDelegateFor<glGetPolygonStipple>(ref glGetPolygonStippleDelegate)(mask);
        }

        /// <summary>
        /// Return a string	describing the current GL connection.
        /// </summary>
        /// <param name="name">Specifies a symbolic constant, one of OpenGL.VENDOR, OpenGL.RENDERER, OpenGL.VERSION, or OpenGL.EXTENSIONS.</param>
        /// <returns>Pointer to the specified string.</returns>
		public string GetString(uint name)
		{
            var chars = getDelegateFor<glGetString>(ref glGetStringDelegate)(name);
			return chars == IntPtr.Zero ? null : Marshal.PtrToStringAnsi(chars);
		}

        /// <summary>
        /// Return texture environment parameters.
        /// </summary>
        /// <param name="target">Specifies a texture environment.  Must be OpenGL.TEXTURE_ENV.</param>
        /// <param name="pname">Specifies the	symbolic name of a texture environment parameter.  Accepted values are OpenGL.TEXTURE_ENV_MODE, and OpenGL.TEXTURE_ENV_COLOR.</param>
        /// <param name="parameters">Returns the requested	data.</param>
		public void GetTexEnv(uint target, uint pname, float[] parameters)
        {
            getDelegateFor<glGetTexEnvfv>(ref glGetTexEnvfvDelegate)(target, pname, parameters);
        }

        /// <summary>
        /// Return texture environment parameters.
        /// </summary>
        /// <param name="target">Specifies a texture environment.  Must be OpenGL.TEXTURE_ENV.</param>
        /// <param name="pname">Specifies the	symbolic name of a texture environment parameter.  Accepted values are OpenGL.TEXTURE_ENV_MODE, and OpenGL.TEXTURE_ENV_COLOR.</param>
        /// <param name="parameters">Returns the requested	data.</param>
        public void GetTexEnv(uint target, uint pname, int[] parameters)
        {
            getDelegateFor<glGetTexEnviv>(ref glGetTexEnvivDelegate)(target, pname, parameters);
        }

        /// <summary>
        /// Control the generation of texture coordinates.
        /// </summary>
        /// <param name="coord">Specifies a texture coordinate. Must be one of OpenGL.S, OpenGL.T, OpenGL.R, or OpenGL.Q.</param>
        /// <param name="pname">Specifies the symbolic name of the texture-coordinate generation function. Must be OpenGL.TEXTURE_GEN_MODE.</param>
        /// <param name="parameters">Specifies a single-valued texture generation parameter, one of OpenGL.OBJECT_LINEAR, OpenGL.EYE_LINEAR, or OpenGL.SPHERE_MAP.</param>
        public void GetTexGen(uint coord, uint pname, double[] parameters) 
        {
            getDelegateFor<glGetTexGendv>(ref glGetTexGendvDelegate)(coord, pname, parameters);
        }

        /// <summary>
        /// Control the generation of texture coordinates.
        /// </summary>
        /// <param name="coord">Specifies a texture coordinate. Must be one of OpenGL.S, OpenGL.T, OpenGL.R, or OpenGL.Q.</param>
        /// <param name="pname">Specifies the symbolic name of the texture-coordinate generation function. Must be OpenGL.TEXTURE_GEN_MODE.</param>
        /// <param name="parameters">Specifies a single-valued texture generation parameter, one of OpenGL.OBJECT_LINEAR, OpenGL.EYE_LINEAR, or OpenGL.SPHERE_MAP.</param>
        public void GetTexGen(uint coord, uint pname, float[] parameters)
        {
            getDelegateFor<glGetTexGenfv>(ref glGetTexGenfvDelegate)(coord, pname, parameters);
        }
        
        /// <summary>
        /// Control the generation of texture coordinates.
        /// </summary>
        /// <param name="coord">Specifies a texture coordinate. Must be one of OpenGL.S, OpenGL.T, OpenGL.R, or OpenGL.Q.</param>
        /// <param name="pname">Specifies the symbolic name of the texture-coordinate generation function. Must be OpenGL.TEXTURE_GEN_MODE.</param>
        /// <param name="parameters">Specifies a single-valued texture generation parameter, one of OpenGL.OBJECT_LINEAR, OpenGL.EYE_LINEAR, or OpenGL.SPHERE_MAP.</param>
        public void GetTexGen(uint coord, uint pname, int[] parameters)
        {
            getDelegateFor<glGetTexGeniv>(ref glGetTexGenivDelegate)(coord, pname, parameters);
        }

        /// <summary>
        /// Return a texture image.
        /// </summary>
        /// <param name="target">Specifies which texture is to	be obtained. OpenGL.TEXTURE_1D and OpenGL.TEXTURE_2D are accepted.</param>
        /// <param name="level">Specifies the level-of-detail number of the desired image.  Level	0 is the base image level.  Level n is the nth mipmap reduction image.</param>
        /// <param name="format">Specifies a pixel format for the returned data.</param>
        /// <param name="type">Specifies a pixel type for the returned data.</param>
        /// <param name="pixels">Returns the texture image.  Should be	a pointer to an array of the type specified by type.</param>
		public void GetTexImage(uint target, int level, uint format, uint type, int[] pixels)
        {
            getDelegateFor<glGetTexImage>(ref glGetTexImageDelegate)(target, level, format, type, pixels);
        }

        /// <summary>
        /// Return texture parameter values for a specific level of detail.
        /// </summary>
        /// <param name="target">Specifies the	symbolic name of the target texture.</param>
        /// <param name="level">Specifies the level-of-detail	number of the desired image.  Level	0 is the base image level.  Level n is the nth mipmap reduction image.</param>
        /// <param name="pname">Specifies the symbolic name of a texture parameter.</param>
        /// <param name="parameters">Returns the requested	data.</param>
		public void GetTexLevelParameter(uint target, int level, uint pname, float[] parameters)
        {
            getDelegateFor<glGetTexLevelParameterfv>(ref glGetTexLevelParameterfvDelegate)(target, level, pname, parameters);
        }

        /// <summary>
        /// Return texture parameter values for a specific level of detail.
        /// </summary>
        /// <param name="target">Specifies the	symbolic name of the target texture.</param>
        /// <param name="level">Specifies the level-of-detail	number of the desired image.  Level	0 is the base image level.  Level n is the nth mipmap reduction image.</param>
        /// <param name="pname">Specifies the symbolic name of a texture parameter.</param>
        /// <param name="parameters">Returns the requested	data.</param>
        public void GetTexLevelParameter(uint target, int level, uint pname, int[] parameters)
        {
            getDelegateFor<glGetTexLevelParameteriv>(ref glGetTexLevelParameterivDelegate)(target, level, pname, parameters);
        }

        /// <summary>
        /// Return texture parameter values.
        /// </summary>
        /// <param name="target">Specifies the symbolic name of the target texture.</param>
        /// <param name="pname">Specifies the symbolic name of a texture parameter.</param>
        /// <param name="parameters">Returns the texture parameters.</param>
        public void GetTexParameter(uint target, uint pname, float[] parameters) 
        {
            getDelegateFor<glGetTexParameterfv>(ref glGetTexParameterfvDelegate)(target, pname, parameters);
        }
        /// <summary>
        /// Return texture parameter values.
        /// </summary>
        /// <param name="target">Specifies the symbolic name of the target texture.</param>
        /// <param name="pname">Specifies the symbolic name of a texture parameter.</param>
        /// <param name="parameters">Returns the texture parameters.</param>
        public void GetTexParameter(uint target, uint pname, int[] parameters)
        {
            getDelegateFor<glGetTexParameteriv>(ref glGetTexParameterivDelegate)(target, pname, parameters);
        }

        /// <summary>
        /// Specify implementation-specific hints.
        /// </summary>
        /// <param name="target">Specifies a symbolic constant indicating the behavior to be controlled.</param>
        /// <param name="mode">Specifies a symbolic constant indicating the desired behavior.</param>
		public void Hint(uint target, uint mode)
        {
            getDelegateFor<glHint>(ref glHintDelegate)(target, mode);
        }

        /// <summary>
        /// Specify implementation-specific hints.
        /// </summary>
        /// <param name="target">Specifies a symbolic constant indicating the behavior to be controlled.</param>
        /// <param name="mode">Specifies a symbolic constant indicating the desired behavior.</param>
        public void Hint(Enumerations.HintTarget target, Enumerations.HintMode mode)
        {
            getDelegateFor<glHint>(ref glHintDelegate)((uint)target,(uint)mode);
        }

        /// <summary>
        /// Control	the writing of individual bits in the color	index buffers.
        /// </summary>
        /// <param name="mask">Specifies a bit	mask to	enable and disable the writing of individual bits in the color index buffers. Initially, the mask is all 1's.</param>
		public void IndexMask(uint mask)
        {
            getDelegateFor<glIndexMask>(ref glIndexMaskDelegate)(mask);
        }

        /// <summary>
        /// Define an array of color indexes.
        /// </summary>
        /// <param name="type">Specifies the data type of each color index in the array.  Symbolic constants OpenGL.UNSIGNED_BYTE, OpenGL.SHORT, OpenGL.INT, OpenGL.FLOAT, and OpenGL.DOUBLE are accepted.</param>
        /// <param name="stride">Specifies the byte offset between consecutive color indexes.  If stride is 0(the initial value), the color indexes are understood	to be tightly packed in the array.</param>
        /// <param name="pointer">Specifies a pointer to the first index in the array.</param>
		public void IndexPointer(uint type, int stride, int[] pointer)
        {
            getDelegateFor<glIndexPointer>(ref glIndexPointerDelegate)(type, stride, pointer);
        }

        /// <summary>
        /// Set the current color index.
        /// </summary>
        /// <param name="c">Specifies the new value for the current color index.</param>
		public void Index(double c)
        {
            getDelegateFor<glIndexd>(ref glIndexdDelegate)(c);
        }

        /// <summary>
        /// Set the current color index.
        /// </summary>
        /// <param name="c">Specifies the new value for the current color index.</param>
        public void Index(double[] c)
        {
            getDelegateFor<glIndexdv>(ref glIndexdvDelegate)(c);
        }

        /// <summary>
        /// Set the current color index.
        /// </summary>
        /// <param name="c">Specifies the new value for the current color index.</param>
        public void Index(float c)
        {
            getDelegateFor<glIndexf>(ref glIndexfDelegate)(c);
        }

        /// <summary>
        /// Set the current color index.
        /// </summary>
        /// <param name="c">Specifies the new value for the current color index.</param>
        public void Index(float[] c)
        {
            getDelegateFor<glIndexfv>(ref glIndexfvDelegate)(c);
        }

        /// <summary>
        /// Set the current color index.
        /// </summary>
        /// <param name="c">Specifies the new value for the current color index.</param>
        public void Index(int c)
        {
            getDelegateFor<glIndexi>(ref glIndexiDelegate)(c);
        }

        /// <summary>
        /// Set the current color index.
        /// </summary>
        /// <param name="c">Specifies the new value for the current color index.</param>
        public void Index(int[] c)
        {
            getDelegateFor<glIndexiv>(ref glIndexivDelegate)(c);
        }

        /// <summary>
        /// Set the current color index.
        /// </summary>
        /// <param name="c">Specifies the new value for the current color index.</param>
        public void Index(short c)
        {
            getDelegateFor<glIndexs>(ref glIndexsDelegate)(c);
        }

        /// <summary>
        /// Set the current color index.
        /// </summary>
        /// <param name="c">Specifies the new value for the current color index.</param>
        public void Index(short[] c)
        {
            getDelegateFor<glIndexsv>(ref glIndexsvDelegate)(c);
        }

        /// <summary>
        /// Set the current color index.
        /// </summary>
        /// <param name="c">Specifies the new value for the current color index.</param>
        public void Index(byte c)
        {
            getDelegateFor<glIndexub>(ref glIndexubDelegate)(c);
        }

        /// <summary>
        /// Set the current color index.
        /// </summary>
        /// <param name="c">Specifies the new value for the current color index.</param>
        public void Index(byte[] c)
        {
            getDelegateFor<glIndexubv>(ref glIndexubvDelegate)(c);
        }

		/// <summary>
		/// This function initialises the select buffer names.
		/// </summary>
		public void InitNames()
		{
			getDelegateFor<glInitNames>(ref glInitNamesDelegate)();
		}

        /// <summary>
        /// Simultaneously specify and enable several interleaved arrays.
        /// </summary>
        /// <param name="format">Specifies the type of array to enable.</param>
        /// <param name="stride">Specifies the offset in bytes between each aggregate array element.</param>
        /// <param name="pointer">The array.</param>
		public void InterleavedArrays(uint format, int stride, int[] pointer)
        {
            getDelegateFor<glInterleavedArrays>(ref glInterleavedArraysDelegate)(format, stride, pointer);
        }

		/// <summary>
		/// Use this function to query if a certain OpenGL function is enabled or not.
		/// </summary>
		/// <param name="cap">The capability to test.</param>
		/// <returns>True if the capability is enabled, otherwise, false.</returns>
		public bool IsEnabled(uint cap)
		{
			byte e = getDelegateFor<glIsEnabled>(ref glIsEnabledDelegate)(cap);
			return e != 0;
		}

		/// <summary>
		/// This function determines whether a specified value is a display list.
		/// </summary>
		/// <param name="list">The value to test.</param>
		/// <returns>TRUE if it is a list, FALSE otherwise.</returns>
		public byte IsList(uint list)
		{
			byte islist = getDelegateFor<glIsList>(ref glIsListDelegate)(list);
			return islist;
		}

        /// <summary>
        /// Determine if a name corresponds	to a texture.
        /// </summary>
        /// <param name="texture">Specifies a value that may be the name of a texture.</param>
        /// <returns>True if texture is a texture object.</returns>
		public byte IsTexture(uint texture)
        {
            byte returnValue = getDelegateFor<glIsTexture>(ref glIsTextureDelegate)(texture);
            return returnValue;
        }

		/// <summary>
		/// This function sets a parameter of the lighting model.
		/// </summary>
		/// <param name="pname">The name of the parameter.</param>
		/// <param name="param">The parameter to set it to.</param>
		public void LightModel(uint pname, float param)
		{
			getDelegateFor<glLightModelf>(ref glLightModelfDelegate)(pname, param);
		}

		/// <summary>
		/// This function sets a parameter of the lighting model.
		/// </summary>
		/// <param name="pname">The name of the parameter.</param>
		/// <param name="param">The parameter to set it to.</param>
        public void LightModel(Enumerations.LightModelParameter pname, float param)
		{
			getDelegateFor<glLightModelf>(ref glLightModelfDelegate)((uint)pname, param);
		}

		/// <summary>
		/// This function sets a parameter of the lighting model.
		/// </summary>
		/// <param name="pname">The name of the parameter.</param>
		/// <param name="parameters">The parameter to set it to.</param>
		public void LightModel(uint pname, float[] parameters)
		{
			getDelegateFor<glLightModelfv>(ref glLightModelfvDelegate)(pname, parameters);
		}

		/// <summary>
		/// This function sets a parameter of the lighting model.
		/// </summary>
		/// <param name="pname">The name of the parameter.</param>
		/// <param name="parameters">The parameter to set it to.</param>
        public void LightModel(Enumerations.LightModelParameter pname, float[] parameters)
		{
			getDelegateFor<glLightModelfv>(ref glLightModelfvDelegate)((uint)pname, parameters);
		}

		/// <summary>
		/// This function sets a parameter of the lighting model.
		/// </summary>
		/// <param name="pname">The name of the parameter.</param>
		/// <param name="param">The parameter to set it to.</param>
		public void LightModel(uint pname, int param)
		{
			getDelegateFor<glLightModeli>(ref glLightModeliDelegate)(pname, param);
		}

		/// <summary>
		/// This function sets a parameter of the lighting model.
		/// </summary>
		/// <param name="pname">The name of the parameter.</param>
		/// <param name="param">The parameter to set it to.</param>
        public void LightModel(Enumerations.LightModelParameter pname, int param)
		{
			getDelegateFor<glLightModeli>(ref glLightModeliDelegate)((uint)pname, param);
		}

		/// <summary>
		/// This function sets a parameter of the lighting model.
		/// </summary>
		/// <param name="pname">The name of the parameter.</param>
		/// <param name="parameters">The parameter to set it to.</param>
		public void LightModel(uint pname, int[] parameters)
		{
			getDelegateFor<glLightModeliv>(ref glLightModelivDelegate)(pname, parameters);
		}

		/// <summary>
		/// This function sets a parameter of the lighting model.
		/// </summary>
		/// <param name="pname">The name of the parameter.</param>
		/// <param name="parameters">The parameter to set it to.</param>
        public void LightModel(Enumerations.LightModelParameter pname, int[] parameters)
		{
			getDelegateFor<glLightModeliv>(ref glLightModelivDelegate)((uint)pname, parameters);
		}

		/// <summary>
		/// Set the parameter(pname) of the light 'light'.
		/// </summary>
		/// <param name="light">The light you wish to set parameters for.</param>
		/// <param name="pname">The parameter you want to set.</param>
		/// <param name="param">The value that you want to set the parameter to.</param>
		public void Light(uint light, uint pname, float param)
		{
			getDelegateFor<glLightf>(ref glLightfDelegate)(light, pname, param);
		}

        /// <summary>
        /// Set the parameter(pname) of the light 'light'.
        /// </summary>
        /// <param name="light">The light you wish to set parameters for.</param>
        /// <param name="pname">The parameter you want to set.</param>
        /// <param name="param">The value that you want to set the parameter to.</param>
        public void Light(Enumerations.LightName light, Enumerations.LightParameter pname, float param)
        {
            getDelegateFor<glLightf>(ref glLightfDelegate)((uint)light,(uint)pname, param);
        }

		/// <summary>
		/// Set the parameter(pname) of the light 'light'.
		/// </summary>
		/// <param name="light">The light you wish to set parameters for.</param>
		/// <param name="pname">The parameter you want to set.</param>
		/// <param name="parameters">The value that you want to set the parameter to.</param>
		public void Light(uint light, uint pname, float[] parameters)
		{
			getDelegateFor<glLightfv>(ref glLightfvDelegate)(light, pname, parameters);
		}
        
        /// <summary>
        /// Set the parameter(pname) of the light 'light'.
        /// </summary>
        /// <param name="light">The light you wish to set parameters for.</param>
        /// <param name="pname">The parameter you want to set.</param>
        /// <param name="parameters">The value that you want to set the parameter to.</param>
        public void Light(Enumerations.LightName light, Enumerations.LightParameter pname, float[] parameters)
        {
            getDelegateFor<glLightfv>(ref glLightfvDelegate)((uint)light,(uint)pname, parameters);
        }

		/// <summary>
		/// Set the parameter(pname) of the light 'light'.
		/// </summary>
		/// <param name="light">The light you wish to set parameters for.</param>
		/// <param name="pname">The parameter you want to set.</param>
		/// <param name="param">The value that you want to set the parameter to.</param>
		public void Light(uint light, uint pname, int param)
		{
			getDelegateFor<glLighti>(ref glLightiDelegate)(light, pname, param);
		}

        /// <summary>
        /// Set the parameter(pname) of the light 'light'.
        /// </summary>
        /// <param name="light">The light you wish to set parameters for.</param>
        /// <param name="pname">The parameter you want to set.</param>
        /// <param name="param">The value that you want to set the parameter to.</param>
        public void Light(Enumerations.LightName light, Enumerations.LightParameter pname, int param)
        {
            getDelegateFor<glLighti>(ref glLightiDelegate)((uint)light,(uint)pname, param);
        }

        /// <summary>
        /// Set the parameter(pname) of the light 'light'.
        /// </summary>
        /// <param name="light">The light you wish to set parameters for.</param>
        /// <param name="pname">The parameter you want to set.</param>
        /// <param name="parameters">The parameters.</param>
		public void Light(uint light, uint pname, int[] parameters)
		{
			getDelegateFor<glLightiv>(ref glLightivDelegate)(light, pname, parameters);
		}

        /// <summary>
        /// Set the parameter(pname) of the light 'light'.
        /// </summary>
        /// <param name="light">The light you wish to set parameters for.</param>
        /// <param name="pname">The parameter you want to set.</param>
        /// <param name="parameters">The parameters.</param>
        public void Light(Enumerations.LightName light, Enumerations.LightParameter pname, int[] parameters)
        {
            getDelegateFor<glLightiv>(ref glLightivDelegate)((uint)light,(uint)pname, parameters);
        }

        /// <summary>
        /// Specify the line stipple pattern.
        /// </summary>
        /// <param name="factor">Specifies a multiplier for each bit in the line stipple pattern.  If factor is 3, for example, each bit in the pattern is used three times before the next	bit in the pattern is used. factor is clamped to the range	[1, 256] and defaults to 1.</param>
        /// <param name="pattern">Specifies a 16-bit integer whose bit	pattern determines which fragments of a line will be drawn when	the line is rasterized.	 Bit zero is used first; the default pattern is all 1's.</param>
        public void LineStipple(int factor, ushort pattern)
        {
            getDelegateFor<glLineStipple>(ref glLineStippleDelegate)(factor, pattern);
        }

		/// <summary>
		/// Set's the current width of lines.
		/// </summary>
		/// <param name="width">New line width to set.</param>
		public void LineWidth(float width)
		{
			getDelegateFor<glLineWidth>(ref glLineWidthDelegate)(width);
		}

        /// <summary>
        /// Set the display-list base for glCallLists.
        /// </summary>
        /// <param name="listbase">Specifies an integer offset that will be added to glCallLists offsets to generate display-list names. The initial value is 0.</param>
		public void ListBase(uint listbase)
        {
            getDelegateFor<glListBase>(ref glListBaseDelegate)(listbase);
        }

		/// <summary>
		/// Call this function to load the identity matrix into the current matrix stack.
		/// </summary>
		public void LoadIdentity()
		{
			getDelegateFor<glLoadIdentity>(ref glLoadIdentityDelegate)();
		}

        /// <summary>
        /// Replace the current matrix with the specified matrix.
        /// </summary>
        /// <param name="m">Specifies a pointer to 16 consecutive values, which are used as the elements of a 4x4 column-major matrix.</param>
		public void LoadMatrix(double[] m)
        {
            getDelegateFor<glLoadMatrixd>(ref glLoadMatrixdDelegate)(m);
        }

        /// <summary>
        /// Replace the current matrix with the specified matrix.
        /// </summary>
        /// <param name="m">Specifies a pointer to 16 consecutive values, which are used as the elements of a 4x4 column-major matrix.</param>
        public void LoadMatrixf(float[] m)
        {
            getDelegateFor<glLoadMatrixf>(ref glLoadMatrixfDelegate)(m);
        }

		/// <summary>
		/// This function replaces the name at the top of the selection names stack
		/// with 'name'.
		/// </summary>
		/// <param name="name">The name to replace it with.</param>
		public void LoadName(uint name)
		{
			getDelegateFor<glLoadName>(ref glLoadNameDelegate)(name);
		}

        /// <summary>
        /// Specify a logical pixel operation for color index rendering.
        /// </summary>
        /// <param name="opcode">Specifies a symbolic constant	that selects a logical operation.</param>
		public void LogicOp(uint opcode)
        {
            getDelegateFor<glLogicOp>(ref glLogicOpDelegate)(opcode);
        }

        /// <summary>
        /// Specify a logical pixel operation for color index rendering.
        /// </summary>
        /// <param name="logicOp">Specifies a symbolic constant	that selects a logical operation.</param>
        public void LogicOp(Enumerations.LogicOp logicOp)
        {
            getDelegateFor<glLogicOp>(ref glLogicOpDelegate)((uint)logicOp);
        }

		/// <summary>
		/// Defines a 1D evaluator.
		/// </summary>
		/// <param name="target">What the control points represent(e.g. MAP1_VERTEX_3).</param>
		/// <param name="u1">Range of the variable 'u'.</param>
		/// <param name="u2">Range of the variable 'u'.</param>
		/// <param name="stride">Offset between beginning of one control point, and beginning of next.</param>
		/// <param name="order">The degree plus one, should agree with the number of control points.</param>
		/// <param name="points">The data for the points.</param>
		public void Map1(uint target, double u1, double u2, int stride, int order, double[] points)
		{
			getDelegateFor<glMap1d>(ref glMap1dDelegate)(target, u1, u2, stride, order, points);
		}

		/// <summary>
		/// Defines a 1D evaluator.
		/// </summary>
		/// <param name="target">What the control points represent(e.g. MAP1_VERTEX_3).</param>
		/// <param name="u1">Range of the variable 'u'.</param>
		/// <param name="u2">Range of the variable 'u'.</param>
		/// <param name="stride">Offset between beginning of one control point, and beginning of next.</param>
		/// <param name="order">The degree plus one, should agree with the number of control points.</param>
		/// <param name="points">The data for the points.</param>
		public void Map1(uint target, float u1, float u2, int stride, int order, float[] points)
		{
			getDelegateFor<glMap1f>(ref glMap1fDelegate)(target, u1, u2, stride, order, points);
		}

		/// <summary>
		/// Defines a 2D evaluator.
		/// </summary>
		/// <param name="target">What the control points represent(e.g. MAP2_VERTEX_3).</param>
		/// <param name="u1">Range of the variable 'u'.</param>
		/// <param name="u2">Range of the variable 'u.</param>
		/// <param name="ustride">Offset between beginning of one control point and the next.</param>
		/// <param name="uorder">The degree plus one.</param>
		/// <param name="v1">Range of the variable 'v'.</param>
		/// <param name="v2">Range of the variable 'v'.</param>
		/// <param name="vstride">Offset between beginning of one control point and the next.</param>
		/// <param name="vorder">The degree plus one.</param>
		/// <param name="points">The data for the points.</param>
		public void Map2(uint target, double u1, double u2, int ustride, int uorder, double v1, double v2, int vstride, int vorder, double[] points)
		{
			getDelegateFor<glMap2d>(ref glMap2dDelegate)(target, u1, u2, ustride, uorder, v1, v2, vstride, vorder, points);
		}

		/// <summary>
		/// Defines a 2D evaluator.
		/// </summary>
		/// <param name="target">What the control points represent(e.g. MAP2_VERTEX_3).</param>
		/// <param name="u1">Range of the variable 'u'.</param>
		/// <param name="u2">Range of the variable 'u.</param>
		/// <param name="ustride">Offset between beginning of one control point and the next.</param>
		/// <param name="uorder">The degree plus one.</param>
		/// <param name="v1">Range of the variable 'v'.</param>
		/// <param name="v2">Range of the variable 'v'.</param>
		/// <param name="vstride">Offset between beginning of one control point and the next.</param>
		/// <param name="vorder">The degree plus one.</param>
		/// <param name="points">The data for the points.</param>
		public void Map2(uint target, float u1, float u2, int ustride, int uorder, float v1, float v2, int vstride, int vorder, float[] points)
		{
			getDelegateFor<glMap2f>(ref glMap2fDelegate)(target, u1, u2, ustride, uorder, v1, v2, vstride, vorder, points);
		}

		/// <summary>
		/// This function defines a grid that goes from u1 to u1 in n steps, evenly spaced.
		/// </summary>
		/// <param name="un">Number of steps.</param>
		/// <param name="u1">Range of variable 'u'.</param>
		/// <param name="u2">Range of variable 'u'.</param>
		public void MapGrid1(int un, double u1, double u2)
		{
			getDelegateFor<glMapGrid1d>(ref glMapGrid1dDelegate)(un, u1, u2);
		}

		/// <summary>
		/// This function defines a grid that goes from u1 to u1 in n steps, evenly spaced.
		/// </summary>
		/// <param name="un">Number of steps.</param>
		/// <param name="u1">Range of variable 'u'.</param>
		/// <param name="u2">Range of variable 'u'.</param>
		public void MapGrid1(int un, float u1, float u2)
		{
			getDelegateFor<glMapGrid1f>(ref glMapGrid1fDelegate)(un, u1, u2);
		}

		/// <summary>
		/// This function defines a grid that goes from u1 to u1 in n steps, evenly spaced,
		/// and the same for v.
		/// </summary>
		/// <param name="un">Number of steps.</param>
		/// <param name="u1">Range of variable 'u'.</param>
		/// <param name="u2">Range of variable 'u'.</param>
		/// <param name="vn">Number of steps.</param>
		/// <param name="v1">Range of variable 'v'.</param>
		/// <param name="v2">Range of variable 'v'.</param>
		public void MapGrid2(int un, double u1, double u2, int vn, double v1, double v2)
		{
			getDelegateFor<glMapGrid2d>(ref glMapGrid2dDelegate)(un, u1, u2, vn, v1, v2);
		}

		/// <summary>
		/// This function defines a grid that goes from u1 to u1 in n steps, evenly spaced,
		/// and the same for v.
		/// </summary>
		/// <param name="un">Number of steps.</param>
		/// <param name="u1">Range of variable 'u'.</param>
		/// <param name="u2">Range of variable 'u'.</param>
		/// <param name="vn">Number of steps.</param>
		/// <param name="v1">Range of variable 'v'.</param>
		/// <param name="v2">Range of variable 'v'.</param>
		public void MapGrid2(int un, float u1, float u2, int vn, float v1, float v2)
		{
			getDelegateFor<glMapGrid2f>(ref glMapGrid2fDelegate)(un, u1, u2, vn, v1, v2);
		}

		/// <summary>
		/// This function sets a material parameter.
		/// </summary>
		/// <param name="face">What faces is this parameter for(i.e front/back etc).</param>
		/// <param name="pname">What parameter you want to set.</param>
		/// <param name="param">The value to set 'pname' to.</param>
		public void Material(uint face, uint pname, float param)
		{
			getDelegateFor<glMaterialf>(ref glMaterialfDelegate)(face, pname, param);
		}

		/// <summary>
		/// This function sets a material parameter.
		/// </summary>
		/// <param name="face">What faces is this parameter for(i.e front/back etc).</param>
		/// <param name="pname">What parameter you want to set.</param>
		/// <param name="parameters">The value to set 'pname' to.</param>
		public void Material(uint face, uint pname, float[] parameters)
		{
			getDelegateFor<glMaterialfv>(ref glMaterialfvDelegate)(face, pname, parameters);
		}

		/// <summary>
		/// This function sets a material parameter.
		/// </summary>
		/// <param name="face">What faces is this parameter for(i.e front/back etc).</param>
		/// <param name="pname">What parameter you want to set.</param>
		/// <param name="param">The value to set 'pname' to.</param>
		public void Material(uint face, uint pname, int param)
		{
			getDelegateFor<glMateriali>(ref glMaterialiDelegate)(face, pname, param);
		}

		/// <summary>
		/// This function sets a material parameter.
		/// </summary>
		/// <param name="face">What faces is this parameter for(i.e front/back etc).</param>
		/// <param name="pname">What parameter you want to set.</param>
		/// <param name="parameters">The value to set 'pname' to.</param>
		public void Material(uint face, uint pname, int[] parameters)
		{
			getDelegateFor<glMaterialiv>(ref glMaterialivDelegate)(face, pname, parameters);
		}

		/// <summary>
		/// Set the current matrix mode(the matrix that matrix operations will be 
		/// performed on).
		/// </summary>
		/// <param name="mode">The mode, normally PROJECTION or MODELVIEW.</param>
		public void MatrixMode(uint mode)
		{
			getDelegateFor<glMatrixMode>(ref glMatrixModeDelegate)(mode);
		}

		/// <summary>
		/// Set the current matrix mode(the matrix that matrix operations will be 
		/// performed on).
		/// </summary>
		/// <param name="mode">The mode, normally PROJECTION or MODELVIEW.</param>
        public void MatrixMode(Enumerations.MatrixMode mode)
		{
			getDelegateFor<glMatrixMode>(ref glMatrixModeDelegate)((uint)mode);
		}

        /// <summary>
        /// Multiply the current matrix with the specified matrix.
        /// </summary>
        /// <param name="m">Points to 16 consecutive values that are used as the elements of a 4x4 column-major matrix.</param>
		public void MultMatrix(double[] m)
        {
            getDelegateFor<glMultMatrixd>(ref glMultMatrixdDelegate)(m);
        }

        /// <summary>
        /// Multiply the current matrix with the specified matrix.
        /// </summary>
        /// <param name="m">Points to 16 consecutive values that are used as the elements of a 4x4 column-major matrix.</param>
		public void MultMatrix(float[] m)
        {
            getDelegateFor<glMultMatrixf>(ref glMultMatrixfDelegate)(m);
        }

		/// <summary>
		/// This function starts compiling a new display list.
		/// </summary>
		/// <param name="list">The list to compile.</param>
		/// <param name="mode">Either COMPILE or COMPILE_AND_EXECUTE.</param>
		public void NewList(uint list, uint mode)
		{
			getDelegateFor<glNewList>(ref glNewListDelegate)(list, mode);
		}

        /// <summary>
        /// Set the current normal.
        /// </summary>
        /// <param name="nx">Normal Coordinate.</param>
        /// <param name="ny">Normal Coordinate.</param>
        /// <param name="nz">Normal Coordinate.</param>
		public void Normal(byte nx, byte ny, byte nz)
        {
            getDelegateFor<glNormal3b>(ref glNormal3bDelegate)(nx, ny, nz);
        }

        /// <summary>
        /// This function sets the current normal.
        /// </summary>
        /// <param name="v">The normal.</param>
        public void Normal(byte[] v)
        {
            getDelegateFor<glNormal3bv>(ref glNormal3bvDelegate)(v);
        }

        /// <summary>
        /// Set the current normal.
        /// </summary>
        /// <param name="nx">Normal Coordinate.</param>
        /// <param name="ny">Normal Coordinate.</param>
        /// <param name="nz">Normal Coordinate.</param>
        public void Normal(double nx, double ny, double nz)
        {
            getDelegateFor<glNormal3d>(ref glNormal3dDelegate)(nx, ny, nz);
        }

        /// <summary>
        /// This function sets the current normal.
        /// </summary>
        /// <param name="v">The normal.</param>
        public void Normal(double[] v)
        {
            getDelegateFor<glNormal3dv>(ref glNormal3dvDelegate)(v);
        }

        /// <summary>
        /// Set the current normal.
        /// </summary>
        /// <param name="nx">Normal Coordinate.</param>
        /// <param name="ny">Normal Coordinate.</param>
        /// <param name="nz">Normal Coordinate.</param>
        public void Normal(float nx, float ny, float nz)
        {
            getDelegateFor<glNormal3f>(ref glNormal3fDelegate)(nx, ny, nz);
        }

		/// <summary>
		/// This function sets the current normal.
		/// </summary>
		/// <param name="v">The normal.</param>
		public void Normal(float[] v)
		{
			getDelegateFor<glNormal3fv>(ref glNormal3fvDelegate)(v);
        }

        /// <summary>
        /// Set the current normal.
        /// </summary>
        /// <param name="nx">Normal Coordinate.</param>
        /// <param name="ny">Normal Coordinate.</param>
        /// <param name="nz">Normal Coordinate.</param>
        public void Normal3i(int nx, int ny, int nz)
        {
            getDelegateFor<glNormal3i>(ref glNormal3iDelegate)(nx, ny, nz);
        }

        /// <summary>
        /// This function sets the current normal.
        /// </summary>
        /// <param name="v">The normal.</param>
        public void Normal(int[] v)
        {
            getDelegateFor<glNormal3iv>(ref glNormal3ivDelegate)(v);
        }

        /// <summary>
        /// Set the current normal.
        /// </summary>
        /// <param name="nx">Normal Coordinate.</param>
        /// <param name="ny">Normal Coordinate.</param>
        /// <param name="nz">Normal Coordinate.</param>
        public void Normal(short nx, short ny, short nz)
        {
            getDelegateFor<glNormal3s>(ref glNormal3sDelegate)(nx, ny, nz);
        }

        /// <summary>
        /// This function sets the current normal.
        /// </summary>
        /// <param name="v">The normal.</param>
        public void Normal(short[] v)
        {
            getDelegateFor<glNormal3sv>(ref glNormal3svDelegate)(v);
        }

        /// <summary>
        /// Set's the pointer to the normal array.
        /// </summary>
        /// <param name="type">The type of data.</param>
        /// <param name="stride">The space in bytes between each normal.</param>
        /// <param name="pointer">The normals.</param>
        public void NormalPointer(uint type, int stride, IntPtr pointer)
        {
            getDelegateFor<glNormalPointer>(ref glNormalPointerDelegate)(type, stride, pointer);
        }

		/// <summary>
		/// Set's the pointer to the normal array.
		/// </summary>
		/// <param name="stride">The space in bytes between each normal.</param>
		/// <param name="pointer">The normals.</param>
		public void NormalPointer(int stride, float[] pointer)
		{
            var pinned = GCHandle.Alloc(pointer, GCHandleType.Pinned);
			getDelegateFor<glNormalPointer>(ref glNormalPointerDelegate)(GL_FLOAT, stride, pinned.AddrOfPinnedObject());
            pinned.Free();
		}

		/// <summary>
		/// This function creates an orthographic projection matrix(i.e one with no 
		/// perspective) and multiplies it to the current matrix stack, which would
		/// normally be 'PROJECTION'.
		/// </summary>
		/// <param name="left">Left clipping plane.</param>
		/// <param name="right">Right clipping plane.</param>
		/// <param name="bottom">Bottom clipping plane.</param>
		/// <param name="top">Top clipping plane.</param>
		/// <param name="zNear">Near clipping plane.</param>
		/// <param name="zFar">Far clipping plane.</param>
		public void Ortho(double left, double right, double bottom, 
			double top, double zNear, double zFar)
		{
			getDelegateFor<glOrtho>(ref glOrthoDelegate)(left, right, bottom, top, zNear, zFar);
		}

        /// <summary>
        /// Place a marker in the feedback buffer.
        /// </summary>
        /// <param name="token">Specifies a marker value to be placed in the feedback buffer following a OpenGL.PASS_THROUGH_TOKEN.</param>
		public void PassThrough(float token)
        {
            getDelegateFor<glPassThrough>(ref glPassThroughDelegate)(token);
        }

        /// <summary>
        /// Set up pixel transfer maps.
        /// </summary>
        /// <param name="map">Specifies a symbolic	map name.</param>
        /// <param name="mapsize">Specifies the size of the map being defined.</param>
        /// <param name="values">Specifies an	array of mapsize values.</param>
		public void PixelMap(uint map, int mapsize, float[] values)
        {
            getDelegateFor<glPixelMapfv>(ref glPixelMapfvDelegate)(map, mapsize, values);
        }

        /// <summary>
        /// Set up pixel transfer maps.
        /// </summary>
        /// <param name="map">Specifies a symbolic	map name.</param>
        /// <param name="mapsize">Specifies the size of the map being defined.</param>
        /// <param name="values">Specifies an	array of mapsize values.</param>
        public void PixelMap(uint map, int mapsize, uint[] values)
        {
            getDelegateFor<glPixelMapuiv>(ref glPixelMapuivDelegate)(map, mapsize, values);
        }

        /// <summary>
        /// Set up pixel transfer maps.
        /// </summary>
        /// <param name="map">Specifies a symbolic	map name.</param>
        /// <param name="mapsize">Specifies the size of the map being defined.</param>
        /// <param name="values">Specifies an	array of mapsize values.</param>
        public void PixelMap(uint map, int mapsize, ushort[] values)
        {
            getDelegateFor<glPixelMapusv>(ref glPixelMapusvDelegate)(map, mapsize, values);
        }

        /// <summary>
        /// Set pixel storage modes.
        /// </summary>
        /// <param name="pname">Specifies the symbolic	name of	the parameter to be set.</param>
        /// <param name="param">Specifies the value that pname	is set to.</param>
		public void PixelStore(uint pname, float param)
        {
            getDelegateFor<glPixelStoref>(ref glPixelStorefDelegate)(pname, param);
        }

        /// <summary>
        /// Set pixel storage modes.
        /// </summary>
        /// <param name="pname">Specifies the symbolic	name of	the parameter to be set.</param>
        /// <param name="param">Specifies the value that pname	is set to.</param>
        public void PixelStore(uint pname, int param)
        {
            getDelegateFor<glPixelStorei>(ref glPixelStoreiDelegate)(pname, param);
        }

        /// <summary>
        /// Set pixel transfer modes.
        /// </summary>
        /// <param name="pname">Specifies the symbolic name of the pixel transfer parameter to be set.</param>
        /// <param name="param">Specifies the value that pname is set to.</param>
        public void PixelTransfer(uint pname, bool param)
        {
            int p = param ? 1 : 0;
            getDelegateFor<glPixelTransferi>(ref glPixelTransferiDelegate)(pname, p);
        }

        /// <summary>
        /// Set pixel transfer modes.
        /// </summary>
        /// <param name="pname">Specifies the symbolic name of the pixel transfer parameter to be set.</param>
        /// <param name="param">Specifies the value that pname is set to.</param>
        public void PixelTransfer(Enumerations.PixelTransferParameterName pname, bool param)
        {
            int p = param ? 1 : 0;
            getDelegateFor<glPixelTransferi>(ref glPixelTransferiDelegate)((uint)pname, p);
        }

        /// <summary>
        /// Set pixel transfer modes.
        /// </summary>
        /// <param name="pname">Specifies the symbolic name of the pixel transfer parameter to be set.</param>
        /// <param name="param">Specifies the value that pname is set to.</param>
		public void PixelTransfer(uint pname, float param)
        {
            getDelegateFor<glPixelTransferf>(ref glPixelTransferfDelegate)(pname, param);
        }

        /// <summary>
        /// Set pixel transfer modes.
        /// </summary>
        /// <param name="pname">Specifies the symbolic name of the pixel transfer parameter to be set.</param>
        /// <param name="param">Specifies the value that pname is set to.</param>
        public void PixelTransfer(Enumerations.PixelTransferParameterName pname, float param)
        {
            getDelegateFor<glPixelTransferf>(ref glPixelTransferfDelegate)((uint)pname, param);
        }

        /// <summary>
        /// Set pixel transfer modes.
        /// </summary>
        /// <param name="pname">Specifies the symbolic name of the pixel transfer parameter to be set.</param>
        /// <param name="param">Specifies the value that pname is set to.</param>
        public void PixelTransfer(uint pname, int param)
        {
            getDelegateFor<glPixelTransferi>(ref glPixelTransferiDelegate)(pname, param);
        }

        /// <summary>
        /// Set pixel transfer modes.
        /// </summary>
        /// <param name="pname">Specifies the symbolic name of the pixel transfer parameter to be set.</param>
        /// <param name="param">Specifies the value that pname is set to.</param>
        public void PixelTransfer(Enumerations.PixelTransferParameterName pname, int param)
        {
            getDelegateFor<glPixelTransferi>(ref glPixelTransferiDelegate)((uint)pname, param);
        }

        /// <summary>
        /// Specify	the pixel zoom factors.
        /// </summary>
        /// <param name="xfactor">Specify the x and y zoom factors for pixel write operations.</param>
        /// <param name="yfactor">Specify the x and y zoom factors for pixel write operations.</param>
		public void PixelZoom(float xfactor, float yfactor)
        {
            getDelegateFor<glPixelZoom>(ref glPixelZoomDelegate)(xfactor, yfactor);
        }

		/// <summary>
		/// The size of points to be rasterised.
		/// </summary>
		/// <param name="size">Size in pixels.</param>
		public void PointSize(float size)
		{
			getDelegateFor<glPointSize>(ref glPointSizeDelegate)(size);
		}

		/// <summary>
		/// This sets the current drawing mode of polygons(points, lines, filled).
		/// </summary>
		/// <param name="face">The faces this applies to(front, back or both).</param>
		/// <param name="mode">The mode to set to(points, lines, or filled).</param>
		public void PolygonMode(uint face, uint mode)
		{
			getDelegateFor<glPolygonMode>(ref glPolygonModeDelegate)(face, mode);
		}

        /// <summary>
        /// This sets the current drawing mode of polygons(points, lines, filled).
        /// </summary>
        /// <param name="face">The faces this applies to(front, back or both).</param>
        /// <param name="mode">The mode to set to(points, lines, or filled).</param>
        public void PolygonMode(Enumerations.FaceMode face, Enumerations.PolygonMode mode)
        {
            getDelegateFor<glPolygonMode>(ref glPolygonModeDelegate)((uint)face,(uint)mode);
        }

        /// <summary>
        /// Set	the scale and units used to calculate depth	values.
        /// </summary>
        /// <param name="factor">Specifies a scale factor that	is used	to create a variable depth offset for each polygon. The initial value is 0.</param>
        /// <param name="units">Is multiplied by an implementation-specific value to create a constant depth offset. The initial value is 0.</param>
		public void PolygonOffset(float factor, float units)
        {
            getDelegateFor<glPolygonOffset>(ref glPolygonOffsetDelegate)(factor, units);
        }

        /// <summary>
        /// Set the polygon stippling pattern.
        /// </summary>
        /// <param name="mask">Specifies a pointer to a 32x32 stipple pattern that will be unpacked from memory in the same way that glDrawPixels unpacks pixels.</param>
		public void PolygonStipple(byte[] mask)
        {
            getDelegateFor<glPolygonStipple>(ref glPolygonStippleDelegate)(mask);
        }

		/// <summary>
		/// This function restores the attribute stack to the state it was when
		/// PushAttrib was called.
		/// </summary>
		public void PopAttrib()
		{
			getDelegateFor<glPopAttrib>(ref glPopAttribDelegate)();
		}

        /// <summary>
        /// Pop the client attribute stack.
        /// </summary>
		public void PopClientAttrib()
        {
            getDelegateFor<glPopClientAttrib>(ref glPopClientAttribDelegate)();
        }

		/// <summary>
		/// Restore the previously saved state of the current matrix stack.
		/// </summary>
		public void PopMatrix()
		{            
			getDelegateFor<glPopMatrix>(ref glPopMatrixDelegate)();
		}

		/// <summary>
		/// This takes the top name off the selection names stack.
		/// </summary>
		public void PopName()
		{
			getDelegateFor<glPopName>(ref glPopNameDelegate)();
		}

        /// <summary>
        /// Set texture residence priority.
        /// </summary>
        /// <param name="n">Specifies the number of textures to be prioritized.</param>
        /// <param name="textures">Specifies an array containing the names of the textures to be prioritized.</param>
        /// <param name="priorities">Specifies	an array containing the	texture priorities. A priority given in an element of priorities applies to the	texture	named by the corresponding element of textures.</param>
		public void PrioritizeTextures(int n, uint[] textures, float[] priorities)
        {
            getDelegateFor<glPrioritizeTextures>(ref glPrioritizeTexturesDelegate)(n, textures, priorities);
        }

		/// <summary>
		/// Save the current state of the attribute groups specified by 'mask'.
		/// </summary>
		/// <param name="mask">The attibute groups to save.</param>
		public void PushAttrib(uint mask)
		{
			getDelegateFor<glPushAttrib>(ref glPushAttribDelegate)(mask);
		}

        /// <summary>
        /// Save the current state of the attribute groups specified by 'mask'.
        /// </summary>
        /// <param name="mask">The attibute groups to save.</param>
        public void PushAttrib(Enumerations.AttributeMask mask)
        {
            getDelegateFor<glPushAttrib>(ref glPushAttribDelegate)((uint)mask);
        }

        /// <summary>
        /// Push the client attribute stack.
        /// </summary>
        /// <param name="mask">Specifies a mask that indicates	which attributes to save.</param>
		public void PushClientAttrib(uint mask)
        {
            getDelegateFor<glPushClientAttrib>(ref glPushClientAttribDelegate)(mask);
        }

		/// <summary>
		/// Save the current state of the current matrix stack.
		/// </summary>
		public void PushMatrix()
		{
			getDelegateFor<glPushMatrix>(ref glPushMatrixDelegate)();
		}

		/// <summary>
		/// This function adds a new name to the selection buffer.
		/// </summary>
		/// <param name="name">The name to add.</param>
		public void PushName(uint name)
		{
			getDelegateFor<glPushName>(ref glPushNameDelegate)(name);
		}

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        public void RasterPos(double x, double y)
        {
            getDelegateFor<glRasterPos2d>(ref glRasterPos2dDelegate)(x, y);
        }

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="v">The coordinate.</param>
        public void RasterPos(double[] v) 
        {
            if(v.Length == 2)
                getDelegateFor<glRasterPos2dv>(ref glRasterPos2dvDelegate)(v);
            else if(v.Length == 3)
                getDelegateFor<glRasterPos3dv>(ref glRasterPos3dvDelegate)(v);
            else
                getDelegateFor<glRasterPos4dv>(ref glRasterPos4dvDelegate)(v);
        }

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        public void RasterPos(float x, float y)
        {
            getDelegateFor<glRasterPos2f>(ref glRasterPos2fDelegate)(x, y);
        }

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="v">The coordinate.</param>
        public void RasterPos(float[] v)
        {
            if(v.Length == 2)
                getDelegateFor<glRasterPos2fv>(ref glRasterPos2fvDelegate)(v);
            else if(v.Length == 3)
                getDelegateFor<glRasterPos3fv>(ref glRasterPos3fvDelegate)(v);
            else
                getDelegateFor<glRasterPos4fv>(ref glRasterPos4fvDelegate)(v);
        }

		/// <summary>
		/// This function sets the current raster position.
		/// </summary>
		/// <param name="x">X coordinate.</param>
		/// <param name="y">Y coordinate.</param>
		public void RasterPos(int x, int y)
		{
			getDelegateFor<glRasterPos2i>(ref glRasterPos2iDelegate)(x, y);
        }

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="v">The coordinate.</param>
        public void RasterPos(int[] v)
        {
            if(v.Length == 2)
                getDelegateFor<glRasterPos2iv>(ref glRasterPos2ivDelegate)(v);
            else if(v.Length == 3)
                getDelegateFor<glRasterPos3iv>(ref glRasterPos3ivDelegate)(v);
            else
                getDelegateFor<glRasterPos4iv>(ref glRasterPos4ivDelegate)(v);
        }

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        public void RasterPos(short x, short y)
        {
            getDelegateFor<glRasterPos2s>(ref glRasterPos2sDelegate)(x, y);
        }

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="v">The coordinate.</param>
        public void RasterPos(short[] v)
        {
            if(v.Length == 2)
                getDelegateFor<glRasterPos2sv>(ref glRasterPos2svDelegate)(v);
            else if(v.Length == 3)
                getDelegateFor<glRasterPos3sv>(ref glRasterPos3svDelegate)(v);
            else
                getDelegateFor<glRasterPos4sv>(ref glRasterPos4svDelegate)(v);
        }

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="z">Z coordinate.</param>
        public void RasterPos(double x, double y, double z)
        {
            getDelegateFor<glRasterPos3d>(ref glRasterPos3dDelegate)(x, y, z);
        }

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="z">Z coordinate.</param>
        public void RasterPos(float x, float y, float z)
        {
            getDelegateFor<glRasterPos3f>(ref glRasterPos3fDelegate)(x, y, z);
        }

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="z">Z coordinate.</param>
        public void RasterPos(int x, int y, int z)
        {
            getDelegateFor<glRasterPos3i>(ref glRasterPos3iDelegate)(x, y, z);
        }

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="z">Z coordinate.</param>
        public void RasterPos(short x, short y, short z)
        {
            getDelegateFor<glRasterPos3s>(ref glRasterPos3sDelegate)(x, y, z);
        }

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="z">Z coordinate.</param>
        /// <param name="w">W coordinate.</param>
        public void RasterPos(double x, double y, double z, double w)
        {
            getDelegateFor<glRasterPos4d>(ref glRasterPos4dDelegate)(x, y, z, w);
        }

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="z">Z coordinate.</param>
        /// <param name="w">W coordinate.</param>
        public void RasterPos(float x, float y, float z, float w)
        {
            getDelegateFor<glRasterPos4f>(ref glRasterPos4fDelegate)(x, y, z, w);
        }

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="z">Z coordinate.</param>
        /// <param name="w">W coordinate.</param>
        public void RasterPos(int x, int y, int z, int w)
        {
            getDelegateFor<glRasterPos4i>(ref glRasterPos4iDelegate)(x, y, z, w);
        }

        /// <summary>
        /// This function sets the current raster position.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <param name="z">Z coordinate.</param>
        /// <param name="w">W coordinate.</param>
        public void RasterPos(short x, short y, short z, short w)
        {
            getDelegateFor<glRasterPos4s>(ref glRasterPos4sDelegate)(x, y, z, w);
        }

        /// <summary>
        /// Select	a color	buffer source for pixels.
        /// </summary>
        /// <param name="mode">Specifies a color buffer.  Accepted values are OpenGL.FRONT_LEFT, OpenGL.FRONT_RIGHT, OpenGL.BACK_LEFT, OpenGL.BACK_RIGHT, OpenGL.FRONT, OpenGL.BACK, OpenGL.LEFT, OpenGL.GL_RIGHT, and OpenGL.AUXi, where i is between 0 and OpenGL.AUX_BUFFERS - 1.</param>
		public void ReadBuffer(uint mode)
        {
            getDelegateFor<glReadBuffer>(ref glReadBufferDelegate)(mode);
        }

        /// <summary>
        /// Reads a block of pixels from the frame buffer.
        /// </summary>
        /// <param name="x">Top-Left X value.</param>
        /// <param name="y">Top-Left Y value.</param>
        /// <param name="width">Width of block to read.</param>
        /// <param name="height">Height of block to read.</param>
        /// <param name="format">Specifies the format of the pixel data. The following symbolic values are accepted: OpenGL.COLOR_INDEX, OpenGL.STENCIL_INDEX, OpenGL.DEPTH_COMPONENT, OpenGL.RED, OpenGL.GREEN, OpenGL.BLUE, OpenGL.ALPHA, OpenGL.RGB, OpenGL.RGBA, OpenGL.LUMINANCE and OpenGL.LUMINANCE_ALPHA.</param>
        /// <param name="pixels">Storage for the pixel data received.</param>
		public void ReadPixels(int x, int y, int width, int height, uint format, byte[] pixels)
        {
            var pinned = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            getDelegateFor<glReadPixels>(ref glReadPixelsDelegate)(x, y, width, height, format, GL_UNSIGNED_BYTE, pinned.AddrOfPinnedObject());
            pinned.Free();
        }

        /// <summary>
        /// Reads a block of pixels from the frame buffer.
        /// </summary>
        /// <param name="x">Top-Left X value.</param>
        /// <param name="y">Top-Left Y value.</param>
        /// <param name="width">Width of block to read.</param>
        /// <param name="height">Height of block to read.</param>
        /// <param name="format">Specifies the format of the pixel data. The following symbolic values are accepted: OpenGL.COLOR_INDEX, OpenGL.STENCIL_INDEX, OpenGL.DEPTH_COMPONENT, OpenGL.RED, OpenGL.GREEN, OpenGL.BLUE, OpenGL.ALPHA, OpenGL.RGB, OpenGL.RGBA, OpenGL.LUMINANCE and OpenGL.LUMINANCE_ALPHA.</param>
        /// <param name="type">Specifies the data type of the pixel data.Must be one of OpenGL.UNSIGNED_BYTE, OpenGL.BYTE, OpenGL.BITMAP, OpenGL.UNSIGNED_SHORT, OpenGL.SHORT, OpenGL.UNSIGNED_INT, OpenGL.INT or OpenGL.FLOAT.</param>
        /// <param name="pixels">Storage for the pixel data received.</param>
        public void ReadPixels(int x, int y, int width, int height, uint format, uint type, IntPtr pixels)
        {
            getDelegateFor<glReadPixels>(ref glReadPixelsDelegate)(x, y, width, height, format, type, pixels);
        }

		/// <summary>
		/// Draw a rectangle from two coordinates(top-left and bottom-right).
		/// </summary>
		/// <param name="x1">Top-Left X value.</param>
		/// <param name="y1">Top-Left Y value.</param>
		/// <param name="x2">Bottom-Right X Value.</param>
		/// <param name="y2">Bottom-Right Y Value.</param>
		public void Rect(double x1, double y1, double x2, double y2)
		{
			getDelegateFor<glRectd>(ref glRectdDelegate)(x1, y1, x2, y2);
		}

		/// <summary>
		/// Draw a rectangle from two coordinates, expressed as arrays, e.g
		/// Rect(new float[] {0, 0}, new float[] {10, 10});
		/// </summary>
		/// <param name="v1">Top-Left point.</param>
		/// <param name="v2">Bottom-Right point.</param>
		public void Rect(double[] v1, double[] v2)
		{
			getDelegateFor<glRectdv>(ref glRectdvDelegate)(v1, v2);
		}

		/// <summary>
		/// Draw a rectangle from two coordinates(top-left and bottom-right).
		/// </summary>
		/// <param name="x1">Top-Left X value.</param>
		/// <param name="y1">Top-Left Y value.</param>
		/// <param name="x2">Bottom-Right X Value.</param>
		/// <param name="y2">Bottom-Right Y Value.</param>
		public void Rect(float x1, float y1, float x2, float y2)
		{
			getDelegateFor<glRectf>(ref glRectfDelegate)(x1, y1, x2, y2);
		}

		/// <summary>
		/// Draw a rectangle from two coordinates, expressed as arrays, e.g
		/// Rect(new float[] {0, 0}, new float[] {10, 10});
		/// </summary>
		/// <param name="v1">Top-Left point.</param>
		/// <param name="v2">Bottom-Right point.</param>
		public void Rect(float[] v1, float[] v2)
		{
			getDelegateFor<glRectfv>(ref glRectfvDelegate)(v1, v2);
		}

		/// <summary>
		/// Draw a rectangle from two coordinates(top-left and bottom-right).
		/// </summary>
		/// <param name="x1">Top-Left X value.</param>
		/// <param name="y1">Top-Left Y value.</param>
		/// <param name="x2">Bottom-Right X Value.</param>
		/// <param name="y2">Bottom-Right Y Value.</param>
		public void Rect(int x1, int y1, int x2, int y2)
		{
			getDelegateFor<glRecti>(ref glRectiDelegate)(x1, y1, x2, y2);
		}

		/// <summary>
		/// Draw a rectangle from two coordinates, expressed as arrays, e.g
		/// Rect(new float[] {0, 0}, new float[] {10, 10});
		/// </summary>
		/// <param name="v1">Top-Left point.</param>
		/// <param name="v2">Bottom-Right point.</param>
		public void Rect(int[] v1, int[] v2)
		{
			getDelegateFor<glRectiv>(ref glRectivDelegate)(v1, v2);
		}

		/// <summary>
		/// Draw a rectangle from two coordinates(top-left and bottom-right).
		/// </summary>
		/// <param name="x1">Top-Left X value.</param>
		/// <param name="y1">Top-Left Y value.</param>
		/// <param name="x2">Bottom-Right X Value.</param>
		/// <param name="y2">Bottom-Right Y Value.</param>
		public void Rect(short x1, short y1, short x2, short y2)
		{
			getDelegateFor<glRects>(ref glRectsDelegate)(x1, y1, x2, y2);
		}

		/// <summary>
		/// Draw a rectangle from two coordinates, expressed as arrays, e.g
		/// Rect(new float[] {0, 0}, new float[] {10, 10});
		/// </summary>
		/// <param name="v1">Top-Left point.</param>
		/// <param name="v2">Bottom-Right point.</param>
		public void Rect(short[] v1, short[] v2)
		{
			getDelegateFor<glRectsv>(ref glRectsvDelegate)(v1, v2);
		}

		/// <summary>
		/// This function sets the current render mode(render, feedback or select).
		/// </summary>
		/// <param name="mode">The Render mode(RENDER, SELECT or FEEDBACK).</param>
		/// <returns>The hits that selection or feedback caused..</returns>
		public int RenderMode(uint mode)
		{
			int hits = getDelegateFor<glRenderMode>(ref glRenderModeDelegate)(mode);
			return hits;
		}

        /// <summary>
        /// This function sets the current render mode(render, feedback or select).
        /// </summary>
        /// <param name="mode">The Render mode(RENDER, SELECT or FEEDBACK).</param>
        /// <returns>The hits that selection or feedback caused..</returns>
        public int RenderMode(Enumerations.RenderingMode mode)
        {
            int hits = getDelegateFor<glRenderMode>(ref glRenderModeDelegate)((uint)mode);
            return hits;
        }

		/// <summary>
		/// This function applies a rotation transformation to the current matrix.
		/// </summary>
		/// <param name="angle">The angle to rotate.</param>
		/// <param name="x">Amount along x.</param>
		/// <param name="y">Amount along y.</param>
		/// <param name="z">Amount along z.</param>
		public void Rotate(double angle, double x, double y, double z)
		{
			getDelegateFor<glRotated>(ref glRotatedDelegate)(angle, x, y, z);
		}

		/// <summary>
		/// This function applies a rotation transformation to the current matrix.
		/// </summary>
		/// <param name="angle">The angle to rotate.</param>
		/// <param name="x">Amount along x.</param>
		/// <param name="y">Amount along y.</param>
		/// <param name="z">Amount along z.</param>
		public void Rotate(float angle, float x, float y, float z)
		{
			getDelegateFor<glRotatef>(ref glRotatefDelegate)(angle, x, y, z);
		}

		/// <summary>
		/// This function quickly does three rotations, one about each axis, with the
		/// given angles(it's not an OpenGL function, but very useful).
		/// </summary>
		/// <param name="anglex">The angle to rotate about x.</param>
		/// <param name="angley">The angle to rotate about y.</param>
		/// <param name="anglez">The angle to rotate about z.</param>
		public void Rotate(float anglex, float angley, float anglez)
		{
			getDelegateFor<glRotatef>(ref glRotatefDelegate)(anglex, 1, 0, 0);
			getDelegateFor<glRotatef>(ref glRotatefDelegate)(angley, 0, 1, 0);
			getDelegateFor<glRotatef>(ref glRotatefDelegate)(anglez, 0, 0, 1);
		}

		/// <summary>
		/// This function applies a scale transformation to the current matrix.
		/// </summary>
		/// <param name="x">The amount to scale along x.</param>
		/// <param name="y">The amount to scale along y.</param>
		/// <param name="z">The amount to scale along z.</param>
		public void Scale(double x, double y, double z)
		{
			getDelegateFor<glScaled>(ref glScaledDelegate)(x, y, z);
		}

		/// <summary>
		/// This function applies a scale transformation to the current matrix.
		/// </summary>
		/// <param name="x">The amount to scale along x.</param>
		/// <param name="y">The amount to scale along y.</param>
		/// <param name="z">The amount to scale along z.</param>
		public void Scale(float x, float y, float z)
		{
			getDelegateFor<glScalef>(ref glScalefDelegate)(x, y, z);
		}

        /// <summary>
        /// Define the scissor box.
        /// </summary>
        /// <param name="x">Specify the lower left corner of the scissor box. Initially(0, 0).</param>
        /// <param name="y">Specify the lower left corner of the scissor box. Initially(0, 0).</param>
        /// <param name="width">Specify the width and height of the scissor box. When a GL context is first attached to a window, width and height are set to the dimensions of that window.</param>
        /// <param name="height">Specify the width and height of the scissor box. When a GL context is first attached to a window, width and height are set to the dimensions of that window.</param>
		public void Scissor(int x, int y, int width, int height)
        {
            getDelegateFor<glScissor>(ref glScissorDelegate)(x, y, width, height);
        }

		/// <summary>
		/// This function sets the current select buffer.
		/// </summary>
		/// <param name="size">The size of the buffer you are passing.</param>
		/// <param name="buffer">The buffer itself.</param>
		public void SelectBuffer(int size, uint[] buffer)
		{
			getDelegateFor<glSelectBuffer>(ref glSelectBufferDelegate)(size, buffer);
		}

        /// <summary>
        /// Select flat or smooth shading.
        /// </summary>
        /// <param name="mode">Specifies a symbolic value representing a shading technique. Accepted values are OpenGL.FLAT and OpenGL.SMOOTH. The default is OpenGL.SMOOTH.</param>
		public void ShadeModel(uint mode)
        {
            getDelegateFor<glShadeModel>(ref glShadeModelDelegate)(mode);
        }

        /// <summary>
        /// Select flat or smooth shading.
        /// </summary>
        /// <param name="mode">Specifies a symbolic value representing a shading technique. Accepted values are OpenGL.FLAT and OpenGL.SMOOTH. The default is OpenGL.SMOOTH.</param>
        public void ShadeModel(Enumerations.ShadeModel mode)
        {
            getDelegateFor<glShadeModel>(ref glShadeModelDelegate)((uint)mode);
        }

		/// <summary>
		/// This function sets the current stencil buffer function.
		/// </summary>
		/// <param name="func">The function type.</param>
		/// <param name="reference">The function reference.</param>
		/// <param name="mask">The function mask.</param>
		public void StencilFunc(uint func, int reference, uint mask)
		{
			getDelegateFor<glStencilFunc>(ref glStencilFuncDelegate)(func, reference, mask);
		}

        /// <summary>
        /// This function sets the current stencil buffer function.
        /// </summary>
        /// <param name="func">The function type.</param>
        /// <param name="reference">The function reference.</param>
        /// <param name="mask">The function mask.</param>
        public void StencilFunc(Enumerations.StencilFunction func, int reference, uint mask)
        {
            getDelegateFor<glStencilFunc>(ref glStencilFuncDelegate)((uint)func, reference, mask);
        }

		/// <summary>
		/// This function sets the stencil buffer mask.
		/// </summary>
		/// <param name="mask">The mask.</param>
		public void StencilMask(uint mask)
		{
			getDelegateFor<glStencilMask>(ref glStencilMaskDelegate)(mask);
		}

		/// <summary>
		/// This function sets the stencil buffer operation.
		/// </summary>
		/// <param name="fail">Fail operation.</param>
		/// <param name="zfail">Depth fail component.</param>
		/// <param name="zpass">Depth pass component.</param>
		public void StencilOp(uint fail, uint zfail, uint zpass)
		{
			getDelegateFor<glStencilOp>(ref glStencilOpDelegate)(fail, zfail, zpass);
		}

		/// <summary>
		/// This function sets the stencil buffer operation.
		/// </summary>
		/// <param name="fail">Fail operation.</param>
		/// <param name="zfail">Depth fail component.</param>
		/// <param name="zpass">Depth pass component.</param>
        public void StencilOp(Enumerations.StencilOperation fail, Enumerations.StencilOperation zfail, Enumerations.StencilOperation zpass)
		{
			getDelegateFor<glStencilOp>(ref glStencilOpDelegate)((uint)fail,(uint)zfail,(uint)zpass);
		}

		/// <summary>
		/// This function sets the current texture coordinates.
		/// </summary>
		/// <param name="s">Texture Coordinate.</param>
		public void TexCoord(double s)
		{
			getDelegateFor<glTexCoord1d>(ref glTexCoord1dDelegate)(s);
		}

		/// <summary>
		/// This function sets the current texture coordinates.
		/// </summary>
		/// <param name="v">Array of 1,2,3 or 4 Texture Coordinates.</param>
		public void TexCoord(double[] v)
		{
			if(v.Length == 1)
				getDelegateFor<glTexCoord1dv>(ref glTexCoord1dvDelegate)(v);
			else if(v.Length == 2)
				getDelegateFor<glTexCoord2dv>(ref glTexCoord2dvDelegate)(v);
			else if(v.Length == 3)
				getDelegateFor<glTexCoord3dv>(ref glTexCoord3dvDelegate)(v);
			else if(v.Length == 4)
				getDelegateFor<glTexCoord4dv>(ref glTexCoord4dvDelegate)(v);
		}

		/// <summary>
		/// This function sets the current texture coordinates.
		/// </summary>
		/// <param name="s">Texture Coordinate.</param>
		public void TexCoord(float s)
		{
			getDelegateFor<glTexCoord1f>(ref glTexCoord1fDelegate)(s);
		}

		/// <summary>
		/// This function sets the current texture coordinates. WARNING: if you
		/// can call something more explicit, like TexCoord2f then call that, it's
		/// much faster.
		/// </summary>
		/// <param name="v">Array of 1,2,3 or 4 Texture Coordinates.</param>
		public void TexCoord(float[] v)
		{
			if(v.Length == 1)
				getDelegateFor<glTexCoord1fv>(ref glTexCoord1fvDelegate)(v);
			else if(v.Length == 2)
				getDelegateFor<glTexCoord2fv>(ref glTexCoord2fvDelegate)(v);
			else if(v.Length == 3)
				getDelegateFor<glTexCoord3fv>(ref glTexCoord3fvDelegate)(v);
			else if(v.Length == 4)
				getDelegateFor<glTexCoord4fv>(ref glTexCoord4fvDelegate)(v);
		}

		/// <summary>
		/// This function sets the current texture coordinates.
		/// </summary>
		/// <param name="s">Texture Coordinate.</param>
		public void TexCoord(int s)
		{
			getDelegateFor<glTexCoord1i>(ref glTexCoord1iDelegate)(s);
		}

		/// <summary>
		/// This function sets the current texture coordinates.
		/// </summary>
		/// <param name="v">Array of 1,2,3 or 4 Texture Coordinates.</param>
		public void TexCoord(int[] v)
		{
			if(v.Length == 1)
				getDelegateFor<glTexCoord1iv>(ref glTexCoord1ivDelegate)(v);
			else if(v.Length == 2)
				getDelegateFor<glTexCoord2iv>(ref glTexCoord2ivDelegate)(v);
			else if(v.Length == 3)
				getDelegateFor<glTexCoord3iv>(ref glTexCoord3ivDelegate)(v);
			else if(v.Length == 4)
				getDelegateFor<glTexCoord4iv>(ref glTexCoord4ivDelegate)(v);
		}

		/// <summary>
		/// This function sets the current texture coordinates.
		/// </summary>
		/// <param name="s">Texture Coordinate.</param>
		public void TexCoord(short s)
		{
			getDelegateFor<glTexCoord1s>(ref glTexCoord1sDelegate)(s);
		}

		/// <summary>
		/// This function sets the current texture coordinates.
		/// </summary>
		/// <param name="v">Array of 1,2,3 or 4 Texture Coordinates.</param>
		public void TexCoord(short[] v)
		{
			if(v.Length == 1)
				getDelegateFor<glTexCoord1sv>(ref glTexCoord1svDelegate)(v);
			else if(v.Length == 2)
				getDelegateFor<glTexCoord2sv>(ref glTexCoord2svDelegate)(v);
			else if(v.Length == 3)
				getDelegateFor<glTexCoord3sv>(ref glTexCoord3svDelegate)(v);
			else if(v.Length == 4)
				getDelegateFor<glTexCoord4sv>(ref glTexCoord4svDelegate)(v);
		}

		/// <summary>
		/// This function sets the current texture coordinates.
		/// </summary>
		/// <param name="s">Texture Coordinate.</param>
		/// <param name="t">Texture Coordinate.</param>
		public void TexCoord(double s, double t)
		{
			getDelegateFor<glTexCoord2d>(ref glTexCoord2dDelegate)(s, t);
		}

		/// <summary>
		/// This function sets the current texture coordinates.
		/// </summary>
		/// <param name="s">Texture Coordinate.</param>
		/// <param name="t">Texture Coordinate.</param>
		public void TexCoord(float s, float t)
		{
			getDelegateFor<glTexCoord2f>(ref glTexCoord2fDelegate)(s, t);
		}

		/// <summary>
		/// This function sets the current texture coordinates.
		/// </summary>
		/// <param name="s">Texture Coordinate.</param>
		/// <param name="t">Texture Coordinate.</param>
		public void TexCoord(int s, int t)
		{
			getDelegateFor<glTexCoord2i>(ref glTexCoord2iDelegate)(s, t);
		}

		/// <summary>
		/// This function sets the current texture coordinates.
		/// </summary>
		/// <param name="s">Texture Coordinate.</param>
		/// <param name="t">Texture Coordinate.</param>
		public void TexCoord(short s, short t)
		{
			getDelegateFor<glTexCoord2s>(ref glTexCoord2sDelegate)(s, t);
		}

        /// <summary>
        /// This function sets the current texture coordinates.
        /// </summary>
        /// <param name="s">Texture Coordinate.</param>
        /// <param name="t">Texture Coordinate.</param>
        /// <param name="r">Texture Coordinate.</param>
        public void TexCoord(double s, double t, double r)
        {
            getDelegateFor<glTexCoord3d>(ref glTexCoord3dDelegate)(s, t, r);
        }

        /// <summary>
        /// This function sets the current texture coordinates.
        /// </summary>
        /// <param name="s">Texture Coordinate.</param>
        /// <param name="t">Texture Coordinate.</param>
        /// <param name="r">Texture Coordinate.</param>
        public void TexCoord(float s, float t, float r)
        {
            getDelegateFor<glTexCoord3f>(ref glTexCoord3fDelegate)(s, t, r);
        }

        /// <summary>
        /// This function sets the current texture coordinates.
        /// </summary>
        /// <param name="s">Texture Coordinate.</param>
        /// <param name="t">Texture Coordinate.</param>
        /// <param name="r">Texture Coordinate.</param>
        public void TexCoord(int s, int t, int r)
        {
            getDelegateFor<glTexCoord3i>(ref glTexCoord3iDelegate)(s, t, r);
        }

        /// <summary>
        /// This function sets the current texture coordinates.
        /// </summary>
        /// <param name="s">Texture Coordinate.</param>
        /// <param name="t">Texture Coordinate.</param>
        /// <param name="r">Texture Coordinate.</param>
        public void TexCoord(short s, short t, short r)
        {
            getDelegateFor<glTexCoord3s>(ref glTexCoord3sDelegate)(s, t, r);
        }

        /// <summary>
        /// This function sets the current texture coordinates.
        /// </summary>
        /// <param name="s">Texture Coordinate.</param>
        /// <param name="t">Texture Coordinate.</param>
        /// <param name="r">Texture Coordinate.</param>
        /// <param name="q">Texture Coordinate.</param>
        public void TexCoord(double s, double t, double r, double q)
        {
            getDelegateFor<glTexCoord4d>(ref glTexCoord4dDelegate)(s, t, r, q);
        }

        /// <summary>
        /// This function sets the current texture coordinates.
        /// </summary>
        /// <param name="s">Texture Coordinate.</param>
        /// <param name="t">Texture Coordinate.</param>
        /// <param name="r">Texture Coordinate.</param>
        /// <param name="q">Texture Coordinate.</param>
        public void TexCoord(float s, float t, float r, float q)
        {
            getDelegateFor<glTexCoord4f>(ref glTexCoord4fDelegate)(s, t, r, q);
        }

        /// <summary>
        /// This function sets the current texture coordinates.
        /// </summary>
        /// <param name="s">Texture Coordinate.</param>
        /// <param name="t">Texture Coordinate.</param>
        /// <param name="r">Texture Coordinate.</param>
        /// <param name="q">Texture Coordinate.</param>
        public void TexCoord(int s, int t, int r, int q)
        {
            getDelegateFor<glTexCoord4i>(ref glTexCoord4iDelegate)(s, t, r, q);
        }

        /// <summary>
        /// This function sets the current texture coordinates.
        /// </summary>
        /// <param name="s">Texture Coordinate.</param>
        /// <param name="t">Texture Coordinate.</param>
        /// <param name="r">Texture Coordinate.</param>
        /// <param name="q">Texture Coordinate.</param>
        public void TexCoord(short s, short t, short r, short q)
        {
            getDelegateFor<glTexCoord4s>(ref glTexCoord4sDelegate)(s, t, r, q);
        }

        /// <summary>
        /// This function sets the texture coord array.
        /// </summary>
        /// <param name="size">The number of coords per set.</param>
        /// <param name="type">The type of data.</param>
        /// <param name="stride">The number of bytes between coords.</param>
        /// <param name="pointer">The coords.</param>
        public void TexCoordPointer(int size, uint type, int stride, IntPtr pointer)
        {
            getDelegateFor<glTexCoordPointer>(ref glTexCoordPointerDelegate)(size, type, stride, pointer);
        }

		/// <summary>
		/// This function sets the texture coord array.
		/// </summary>
		/// <param name="size">The number of coords per set.</param>
		/// <param name="stride">The number of bytes between coords.</param>
		/// <param name="pointer">The coords.</param>
		public void TexCoordPointer(int size, int stride, float[] pointer)
		{
            var pinned = GCHandle.Alloc(pointer, GCHandleType.Pinned);
			getDelegateFor<glTexCoordPointer>(ref glTexCoordPointerDelegate)(size, GL_FLOAT, stride, pinned.AddrOfPinnedObject());
            pinned.Free();
		}

        /// <summary>
        /// Set texture environment parameters.
        /// </summary>
        /// <param name="target">Specifies a texture environment. Must be OpenGL.TEXTURE_ENV.</param>
        /// <param name="pname">Specifies the symbolic name of a single-valued texture environment parameter. Must be OpenGL.TEXTURE_ENV_MODE.</param>
        /// <param name="param">Specifies a single symbolic constant, one of OpenGL.MODULATE, OpenGL.DECAL, OpenGL.BLEND, or OpenGL.REPLACE.</param>
        public void TexEnv(uint target, uint pname, float param)
        {
            getDelegateFor<glTexEnvf>(ref glTexEnvfDelegate)(target, pname, param);
        }

        /// <summary>
        /// Set texture environment parameters.
        /// </summary>
        /// <param name="target">Specifies a texture environment. Must be OpenGL.TEXTURE_ENV.</param>
        /// <param name="pname">Specifies the symbolic name of a texture environment parameter. Accepted values are OpenGL.TEXTURE_ENV_MODE and OpenGL.TEXTURE_ENV_COLOR.</param>
        /// <param name="parameters">Specifies a pointer to a parameter array that contains either a single symbolic constant or an RGBA color.</param>
        public void TexEnv(uint target, uint pname, float[] parameters)
        {
            getDelegateFor<glTexEnvfv>(ref glTexEnvfvDelegate)(target, pname, parameters);
        }

        /// <summary>
        /// Set texture environment parameters.
        /// </summary>
        /// <param name="target">Specifies a texture environment. Must be OpenGL.TEXTURE_ENV.</param>
        /// <param name="pname">Specifies the symbolic name of a single-valued texture environment parameter. Must be OpenGL.TEXTURE_ENV_MODE.</param>
        /// <param name="param">Specifies a single symbolic constant, one of OpenGL.MODULATE, OpenGL.DECAL, OpenGL.BLEND, or OpenGL.REPLACE.</param>
        public void TexEnv(uint target, uint pname, int param)
        {
            getDelegateFor<glTexEnvi>(ref glTexEnviDelegate)(target, pname, param);
        }

        /// <summary>
        /// Set texture environment parameters.
        /// </summary>
        /// <param name="target">Specifies a texture environment. Must be OpenGL.TEXTURE_ENV.</param>
        /// <param name="pname">Specifies the symbolic name of a texture environment parameter. Accepted values are OpenGL.TEXTURE_ENV_MODE and OpenGL.TEXTURE_ENV_COLOR.</param>
        /// <param name="parameters">Specifies a pointer to a parameter array that contains either a single symbolic constant or an RGBA color.</param>
        public void TexEnv(uint target, uint pname, int[] parameters)
        {
            getDelegateFor<glTexEnviv>(ref glTexEnvivDelegate)(target, pname, parameters);
        }

        /// <summary>
        /// Control the generation of texture coordinates.
        /// </summary>
        /// <param name="coord">Specifies a texture coordinate. Must be one of OpenGL.S, OpenGL.T, OpenGL.R, or OpenGL.Q.</param>
        /// <param name="pname">Specifies the symbolic name of the texture-coordinate generation function. Must be OpenGL.TEXTURE_GEN_MODE.</param>
        /// <param name="param">Specifies a single-valued texture generation parameter, one of OpenGL.OBJECT_LINEAR, OpenGL.GL_EYE_LINEAR, or OpenGL.SPHERE_MAP.</param>
		public void TexGen(uint coord, uint pname, double param)
        {
            getDelegateFor<glTexGend>(ref glTexGendDelegate)(coord, pname, param);
        }

        /// <summary>
        /// Control the generation of texture coordinates.
        /// </summary>
        /// <param name="coord">Specifies a texture coordinate. Must be one of OpenGL.S, OpenGL.T, OpenGL.R, or OpenGL.Q.</param>
        /// <param name="pname">Specifies the symbolic name of the texture-coordinate generation function or function parameters. Must be OpenGL.TEXTURE_GEN_MODE, OpenGL.OBJECT_PLANE, or OpenGL.EYE_PLANE.</param>
        /// <param name="parameters">Specifies a pointer to an array of texture generation parameters. If pname is OpenGL.TEXTURE_GEN_MODE, then the array must contain a single symbolic constant, one of OpenGL.OBJECT_LINEAR, OpenGL.EYE_LINEAR, or OpenGL.SPHERE_MAP. Otherwise, params holds the coefficients for the texture-coordinate generation function specified by pname.</param>
        public void TexGen(uint coord, uint pname, double[] parameters) 
        {
            getDelegateFor<glTexGendv>(ref glTexGendvDelegate)(coord, pname, parameters);
        }

        /// <summary>
        /// Control the generation of texture coordinates.
        /// </summary>
        /// <param name="coord">Specifies a texture coordinate. Must be one of OpenGL.S, OpenGL.T, OpenGL.R, or OpenGL.Q.</param>
        /// <param name="pname">Specifies the symbolic name of the texture-coordinate generation function. Must be OpenGL.TEXTURE_GEN_MODE.</param>
        /// <param name="param">Specifies a single-valued texture generation parameter, one of OpenGL.OBJECT_LINEAR, OpenGL.GL_EYE_LINEAR, or OpenGL.SPHERE_MAP.</param>
        public void TexGen(uint coord, uint pname, float param)
        {
            getDelegateFor<glTexGenf>(ref glTexGenfDelegate)(coord, pname, param);
        }

        /// <summary>
        /// Control the generation of texture coordinates.
        /// </summary>
        /// <param name="coord">Specifies a texture coordinate. Must be one of OpenGL.S, OpenGL.T, OpenGL.R, or OpenGL.Q.</param>
        /// <param name="pname">Specifies the symbolic name of the texture-coordinate generation function or function parameters. Must be OpenGL.TEXTURE_GEN_MODE, OpenGL.OBJECT_PLANE, or OpenGL.EYE_PLANE.</param>
        /// <param name="parameters">Specifies a pointer to an array of texture generation parameters. If pname is OpenGL.TEXTURE_GEN_MODE, then the array must contain a single symbolic constant, one of OpenGL.OBJECT_LINEAR, OpenGL.EYE_LINEAR, or OpenGL.SPHERE_MAP. Otherwise, params holds the coefficients for the texture-coordinate generation function specified by pname.</param>
        public void TexGen(uint coord, uint pname, float[] parameters)
        {
            getDelegateFor<glTexGenfv>(ref glTexGenfvDelegate)(coord, pname, parameters);
        }

        /// <summary>
        /// Control the generation of texture coordinates.
        /// </summary>
        /// <param name="coord">Specifies a texture coordinate. Must be one of OpenGL.S, OpenGL.T, OpenGL.R, or OpenGL.Q.</param>
        /// <param name="pname">Specifies the symbolic name of the texture-coordinate generation function. Must be OpenGL.TEXTURE_GEN_MODE.</param>
        /// <param name="param">Specifies a single-valued texture generation parameter, one of OpenGL.OBJECT_LINEAR, OpenGL.GL_EYE_LINEAR, or OpenGL.SPHERE_MAP.</param>
        public void TexGen(uint coord, uint pname, int param)
        {
            getDelegateFor<glTexGeni>(ref glTexGeniDelegate)(coord, pname, param);
        }

        /// <summary>
        /// Control the generation of texture coordinates.
        /// </summary>
        /// <param name="coord">Specifies a texture coordinate. Must be one of OpenGL.S, OpenGL.T, OpenGL.R, or OpenGL.Q.</param>
        /// <param name="pname">Specifies the symbolic name of the texture-coordinate generation function or function parameters. Must be OpenGL.TEXTURE_GEN_MODE, OpenGL.OBJECT_PLANE, or OpenGL.EYE_PLANE.</param>
        /// <param name="parameters">Specifies a pointer to an array of texture generation parameters. If pname is OpenGL.TEXTURE_GEN_MODE, then the array must contain a single symbolic constant, one of OpenGL.OBJECT_LINEAR, OpenGL.EYE_LINEAR, or OpenGL.SPHERE_MAP. Otherwise, params holds the coefficients for the texture-coordinate generation function specified by pname.</param>
        public void TexGen(uint coord, uint pname, int[] parameters)
        {
            getDelegateFor<glTexGeniv>(ref glTexGenivDelegate)(coord, pname, parameters);
        }

		/// <summary>
		/// This function sets the image for the currently binded texture.
		/// </summary>
		/// <param name="target">The type of texture, TEXTURE_2D or PROXY_TEXTURE_2D.</param>
		/// <param name="level">For mip-map textures, ordinary textures should be '0'.</param>
		/// <param name="internalformat">The format of the data you are want OpenGL to create, e.g  RGB16.</param>
		/// <param name="width">The width of the texture image(must be a power of 2, e.g 64).</param>
		/// <param name="border">The width of the border(0 or 1).</param>
		/// <param name="format">The format of the data you are passing, e.g. RGBA.</param>
		/// <param name="pixels">The actual pixel data.</param>
		public void TexImage1D(uint target, int level, uint internalformat, int width, int border, uint format, byte[] pixels)
		{
            var pinned = GCHandle.Alloc(pixels, GCHandleType.Pinned);
			getDelegateFor<glTexImage1D>(ref glTexImage1DDelegate)(target, level, internalformat, width, border, format, GL_UNSIGNED_BYTE, pinned.AddrOfPinnedObject());
            pinned.Free();
		}

		/// <summary>
		/// This function sets the image for the currently binded texture.
		/// </summary>
		/// <param name="target">The type of texture, TEXTURE_2D or PROXY_TEXTURE_2D.</param>
		/// <param name="level">For mip-map textures, ordinary textures should be '0'.</param>
		/// <param name="internalformat">The format of the data you are want OpenGL to create, e.g  RGB16.</param>
		/// <param name="width">The width of the texture image(must be a power of 2, e.g 64).</param>
		/// <param name="border">The width of the border(0 or 1).</param>
		/// <param name="format">The format of the data you are passing, e.g. RGBA.</param>
		/// <param name="pixels">The actual pixel data.</param>
		public void TexImage1D(uint target, int level, uint internalformat, int width, int border, uint format, sbyte[] pixels)
		{
            var pinned = GCHandle.Alloc(pixels, GCHandleType.Pinned);
		    getDelegateFor<glTexImage1D>(ref glTexImage1DDelegate)(target, level, internalformat, width, border, format, GL_BYTE, pinned.AddrOfPinnedObject());
            pinned.Free();
		}

        /// <summary>
        /// This function sets the image for the currently binded texture.
        /// </summary>
        /// <param name="target">The type of texture, TEXTURE_2D or PROXY_TEXTURE_2D.</param>
        /// <param name="level">For mip-map textures, ordinary textures should be '0'.</param>
        /// <param name="internalformat">The format of the data you are want OpenGL to create, e.g  RGB16.</param>
        /// <param name="width">The width of the texture image(must be a power of 2, e.g 64).</param>
        /// <param name="border">The width of the border(0 or 1).</param>
        /// <param name="format">The format of the data you are passing, e.g. RGBA.</param>
        /// <param name="pixels">The actual pixel data.</param>
        public void TexImage1D(uint target, int level, uint internalformat, int width, int border, uint format, ushort[] pixels)
        {
            var pinned = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            getDelegateFor<glTexImage1D>(ref glTexImage1DDelegate)(target, level, internalformat, width, border, format, GL_UNSIGNED_SHORT, pinned.AddrOfPinnedObject());
            pinned.Free();
        }

        /// <summary>
        /// This function sets the image for the currently binded texture.
        /// </summary>
        /// <param name="target">The type of texture, TEXTURE_2D or PROXY_TEXTURE_2D.</param>
        /// <param name="level">For mip-map textures, ordinary textures should be '0'.</param>
        /// <param name="internalformat">The format of the data you are want OpenGL to create, e.g  RGB16.</param>
        /// <param name="width">The width of the texture image(must be a power of 2, e.g 64).</param>
        /// <param name="border">The width of the border(0 or 1).</param>
        /// <param name="format">The format of the data you are passing, e.g. RGBA.</param>
        /// <param name="pixels">The actual pixel data.</param>
        public void TexImage1D(uint target, int level, uint internalformat, int width, int border, uint format, short[] pixels)
        {
            var pinned = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            getDelegateFor<glTexImage1D>(ref glTexImage1DDelegate)(target, level, internalformat, width, border, format, GL_SHORT, pinned.AddrOfPinnedObject());
            pinned.Free();
        }

        /// <summary>
        /// This function sets the image for the currently binded texture.
        /// </summary>
        /// <param name="target">The type of texture, TEXTURE_2D or PROXY_TEXTURE_2D.</param>
        /// <param name="level">For mip-map textures, ordinary textures should be '0'.</param>
        /// <param name="internalformat">The format of the data you are want OpenGL to create, e.g  RGB16.</param>
        /// <param name="width">The width of the texture image(must be a power of 2, e.g 64).</param>
        /// <param name="border">The width of the border(0 or 1).</param>
        /// <param name="format">The format of the data you are passing, e.g. RGBA.</param>
        /// <param name="pixels">The actual pixel data.</param>
        public void TexImage1D(uint target, int level, uint internalformat, int width, int border, uint format, uint[] pixels)
        {
            var pinned = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            getDelegateFor<glTexImage1D>(ref glTexImage1DDelegate)(target, level, internalformat, width, border, format, GL_UNSIGNED_INT, pinned.AddrOfPinnedObject());
            pinned.Free();
        }

        /// <summary>
        /// This function sets the image for the currently binded texture.
        /// </summary>
        /// <param name="target">The type of texture, TEXTURE_2D or PROXY_TEXTURE_2D.</param>
        /// <param name="level">For mip-map textures, ordinary textures should be '0'.</param>
        /// <param name="internalformat">The format of the data you are want OpenGL to create, e.g  RGB16.</param>
        /// <param name="width">The width of the texture image(must be a power of 2, e.g 64).</param>
        /// <param name="border">The width of the border(0 or 1).</param>
        /// <param name="format">The format of the data you are passing, e.g. RGBA.</param>
        /// <param name="pixels">The actual pixel data.</param>
        public void TexImage1D(uint target, int level, uint internalformat, int width, int border, uint format, int[] pixels)
        {
            var pinned = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            getDelegateFor<glTexImage1D>(ref glTexImage1DDelegate)(target, level, internalformat, width, border, format, GL_INT, pinned.AddrOfPinnedObject());
            pinned.Free();
        }

        /// <summary>
        /// This function sets the image for the currently binded texture.
        /// </summary>
        /// <param name="target">The type of texture, TEXTURE_2D or PROXY_TEXTURE_2D.</param>
        /// <param name="level">For mip-map textures, ordinary textures should be '0'.</param>
        /// <param name="internalformat">The format of the data you are want OpenGL to create, e.g  RGB16.</param>
        /// <param name="width">The width of the texture image(must be a power of 2, e.g 64).</param>
        /// <param name="border">The width of the border(0 or 1).</param>
        /// <param name="format">The format of the data you are passing, e.g. RGBA.</param>
        /// <param name="pixels">The actual pixel data.</param>
        public void TexImage1D(uint target, int level, uint internalformat, int width, int border, uint format, float[] pixels)
        {
            var pinned = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            getDelegateFor<glTexImage1D>(ref glTexImage1DDelegate)(target, level, internalformat, width, border, format, GL_FLOAT, pinned.AddrOfPinnedObject());
            pinned.Free();
        }

        /// <summary>
        /// This function sets the image for the currently binded texture.
        /// </summary>
        /// <param name="target">The type of texture, TEXTURE_2D or PROXY_TEXTURE_2D.</param>
        /// <param name="level">For mip-map textures, ordinary textures should be '0'.</param>
        /// <param name="internalformat">The format of the data you are want OpenGL to create, e.g  RGB16.</param>
        /// <param name="width">The width of the texture image(must be a power of 2, e.g 64).</param>
        /// <param name="border">The width of the border(0 or 1).</param>
        /// <param name="format">The format of the data you are passing, e.g. RGBA.</param>
        /// <param name="type">The type of data you are passing, e.g GL_BYTE.</param>
        /// <param name="pixels">The actual pixel data.</param>
        public void TexImage1D(uint target, int level, uint internalformat, int width, int border, uint format, uint type, IntPtr pixels)
        {
            getDelegateFor<glTexImage1D>(ref glTexImage1DDelegate)(target, level, internalformat, width, border, format, type, pixels);
        }

		/// <summary>
		/// This function sets the image for the currently binded texture.
		/// </summary>
		/// <param name="target">The type of texture, TEXTURE_2D or PROXY_TEXTURE_2D.</param>
		/// <param name="level">For mip-map textures, ordinary textures should be '0'.</param>
		/// <param name="internalformat">The format of the data you are want OpenGL to create, e.g  RGB16.</param>
		/// <param name="width">The width of the texture image(must be a power of 2, e.g 64).</param>
		/// <param name="height">The height of the texture image(must be a power of 2, e.g 32).</param>
		/// <param name="border">The width of the border(0 or 1).</param>
		/// <param name="format">The format of the data you are passing, e.g. RGBA.</param>
		/// <param name="pixels">The actual pixel data.</param>
		public void TexImage2D(uint target, int level, uint internalformat, int width, int height, int border, uint format, byte[] pixels)
		{
            var pinned = GCHandle.Alloc(pixels, GCHandleType.Pinned);
			getDelegateFor<glTexImage2D>(ref glTexImage2DDelegate)(target, level, internalformat, width, height, border, format, GL_UNSIGNED_BYTE, pinned.AddrOfPinnedObject());
		}

        /// <summary>
        /// This function sets the image for the currently binded texture.
        /// </summary>
        /// <param name="target">The type of texture, TEXTURE_2D or PROXY_TEXTURE_2D.</param>
        /// <param name="level">For mip-map textures, ordinary textures should be '0'.</param>
        /// <param name="internalformat">The format of the data you are want OpenGL to create, e.g  RGB16.</param>
        /// <param name="width">The width of the texture image(must be a power of 2, e.g 64).</param>
        /// <param name="height">The height of the texture image(must be a power of 2, e.g 32).</param>
        /// <param name="border">The width of the border(0 or 1).</param>
        /// <param name="format">The format of the data you are passing, e.g. RGBA.</param>
        /// <param name="pixels">The actual pixel data.</param>
        public void TexImage2D(uint target, int level, uint internalformat, int width, int height, int border, uint format, sbyte[] pixels)
        {
            var pinned = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            getDelegateFor<glTexImage2D>(ref glTexImage2DDelegate)(target, level, internalformat, width, height, border, format, GL_BYTE, pinned.AddrOfPinnedObject());
            pinned.Free();
        }

        /// <summary>
        /// This function sets the image for the currently binded texture.
        /// </summary>
        /// <param name="target">The type of texture, TEXTURE_2D or PROXY_TEXTURE_2D.</param>
        /// <param name="level">For mip-map textures, ordinary textures should be '0'.</param>
        /// <param name="internalformat">The format of the data you are want OpenGL to create, e.g  RGB16.</param>
        /// <param name="width">The width of the texture image(must be a power of 2, e.g 64).</param>
        /// <param name="height">The height of the texture image(must be a power of 2, e.g 32).</param>
        /// <param name="border">The width of the border(0 or 1).</param>
        /// <param name="format">The format of the data you are passing, e.g. RGBA.</param>
        /// <param name="pixels">The actual pixel data.</param>
        public void TexImage2D(uint target, int level, uint internalformat, int width, int height, int border, uint format, ushort[] pixels)
        {
            var pinned = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            getDelegateFor<glTexImage2D>(ref glTexImage2DDelegate)(target, level, internalformat, width, height, border, format, GL_UNSIGNED_SHORT, pinned.AddrOfPinnedObject());
            pinned.Free();
        }

        /// <summary>
        /// This function sets the image for the currently binded texture.
        /// </summary>
        /// <param name="target">The type of texture, TEXTURE_2D or PROXY_TEXTURE_2D.</param>
        /// <param name="level">For mip-map textures, ordinary textures should be '0'.</param>
        /// <param name="internalformat">The format of the data you are want OpenGL to create, e.g  RGB16.</param>
        /// <param name="width">The width of the texture image(must be a power of 2, e.g 64).</param>
        /// <param name="height">The height of the texture image(must be a power of 2, e.g 32).</param>
        /// <param name="border">The width of the border(0 or 1).</param>
        /// <param name="format">The format of the data you are passing, e.g. RGBA.</param>
        /// <param name="pixels">The actual pixel data.</param>
        public void TexImage2D(uint target, int level, uint internalformat, int width, int height, int border, uint format, short[] pixels)
        {
            var pinned = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            getDelegateFor<glTexImage2D>(ref glTexImage2DDelegate)(target, level, internalformat, width, height, border, format, GL_SHORT, pinned.AddrOfPinnedObject());
            pinned.Free();
        }

        /// <summary>
        /// This function sets the image for the currently binded texture.
        /// </summary>
        /// <param name="target">The type of texture, TEXTURE_2D or PROXY_TEXTURE_2D.</param>
        /// <param name="level">For mip-map textures, ordinary textures should be '0'.</param>
        /// <param name="internalformat">The format of the data you are want OpenGL to create, e.g  RGB16.</param>
        /// <param name="width">The width of the texture image(must be a power of 2, e.g 64).</param>
        /// <param name="height">The height of the texture image(must be a power of 2, e.g 32).</param>
        /// <param name="border">The width of the border(0 or 1).</param>
        /// <param name="format">The format of the data you are passing, e.g. RGBA.</param>
        /// <param name="pixels">The actual pixel data.</param>
        public void TexImage2D(uint target, int level, uint internalformat, int width, int height, int border, uint format, uint[] pixels)
        {
            var pinned = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            getDelegateFor<glTexImage2D>(ref glTexImage2DDelegate)(target, level, internalformat, width, height, border, format, GL_UNSIGNED_INT, pinned.AddrOfPinnedObject());
            pinned.Free();
        }

        /// <summary>
        /// This function sets the image for the currently binded texture.
        /// </summary>
        /// <param name="target">The type of texture, TEXTURE_2D or PROXY_TEXTURE_2D.</param>
        /// <param name="level">For mip-map textures, ordinary textures should be '0'.</param>
        /// <param name="internalformat">The format of the data you are want OpenGL to create, e.g  RGB16.</param>
        /// <param name="width">The width of the texture image(must be a power of 2, e.g 64).</param>
        /// <param name="height">The height of the texture image(must be a power of 2, e.g 32).</param>
        /// <param name="border">The width of the border(0 or 1).</param>
        /// <param name="format">The format of the data you are passing, e.g. RGBA.</param>
        /// <param name="pixels">The actual pixel data.</param>
        public void TexImage2D(uint target, int level, uint internalformat, int width, int height, int border, uint format, int[] pixels)
        {
            var pinned = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            getDelegateFor<glTexImage2D>(ref glTexImage2DDelegate)(target, level, internalformat, width, height, border, format, GL_INT, pinned.AddrOfPinnedObject());
            pinned.Free();
        }

        /// <summary>
        /// This function sets the image for the currently binded texture.
        /// </summary>
        /// <param name="target">The type of texture, TEXTURE_2D or PROXY_TEXTURE_2D.</param>
        /// <param name="level">For mip-map textures, ordinary textures should be '0'.</param>
        /// <param name="internalformat">The format of the data you are want OpenGL to create, e.g  RGB16.</param>
        /// <param name="width">The width of the texture image(must be a power of 2, e.g 64).</param>
        /// <param name="height">The height of the texture image(must be a power of 2, e.g 32).</param>
        /// <param name="border">The width of the border(0 or 1).</param>
        /// <param name="format">The format of the data you are passing, e.g. RGBA.</param>
        /// <param name="pixels">The actual pixel data.</param>
        public void TexImage2D(uint target, int level, uint internalformat, int width, int height, int border, uint format, float[] pixels)
        {
            var pinned = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            getDelegateFor<glTexImage2D>(ref glTexImage2DDelegate)(target, level, internalformat, width, height, border, format, GL_FLOAT, pinned.AddrOfPinnedObject());
            pinned.Free();
        }

        /// <summary>
        /// This function sets the image for the currently binded texture.
        /// </summary>
        /// <param name="target">The type of texture, TEXTURE_2D or PROXY_TEXTURE_2D.</param>
        /// <param name="level">For mip-map textures, ordinary textures should be '0'.</param>
        /// <param name="internalformat">The format of the data you are want OpenGL to create, e.g  RGB16.</param>
        /// <param name="width">The width of the texture image(must be a power of 2, e.g 64).</param>
        /// <param name="height">The height of the texture image(must be a power of 2, e.g 32).</param>
        /// <param name="border">The width of the border(0 or 1).</param>
        /// <param name="format">The format of the data you are passing, e.g. RGBA.</param>
        /// <param name="type">The type of data you are passing, e.g GL_BYTE.</param>
        /// <param name="pixels">The actual pixel data.</param>
        public void TexImage2D(uint target, int level, uint internalformat, int width, int height, int border, uint format, uint type, IntPtr pixels)
        {
            getDelegateFor<glTexImage2D>(ref glTexImage2DDelegate)(target, level, internalformat, width, height, border, format, type, pixels);
        }

		/// <summary>
		///	This function sets the parameters for the currently binded texture object.
		/// </summary>
		/// <param name="target">The type of texture you are setting the parameter to, e.g. TEXTURE_2D</param>
		/// <param name="pname">The parameter to set.</param>
		/// <param name="param">The value to set it to.</param>
		public void TexParameter(uint target, uint pname, float param)
		{
			getDelegateFor<glTexParameterf>(ref glTexParameterfDelegate)(target, pname, param);
		}

        /// <summary>
        ///	This function sets the parameters for the currently binded texture object.
        /// </summary>
        /// <param name="target">The type of texture you are setting the parameter to, e.g. TEXTURE_2D</param>
        /// <param name="pname">The parameter to set.</param>
        /// <param name="param">The value to set it to.</param>
        public void TexParameter(Enumerations.TextureTarget target, Enumerations.TextureParameter pname, float param)
        {
            getDelegateFor<glTexParameterf>(ref glTexParameterfDelegate)((uint)target,(uint)pname, param);
        }

		/// <summary>
		///	This function sets the parameters for the currently binded texture object.
		/// </summary>
		/// <param name="target">The type of texture you are setting the parameter to, e.g. TEXTURE_2D</param>
		/// <param name="pname">The parameter to set.</param>
		/// <param name="parameters">The value to set it to.</param>
		public void TexParameter(uint target, uint pname, float[] parameters)
		{
			getDelegateFor<glTexParameterfv>(ref glTexParameterfvDelegate)(target, pname, parameters);
		}

        /// <summary>
        ///	This function sets the parameters for the currently binded texture object.
        /// </summary>
        /// <param name="target">The type of texture you are setting the parameter to, e.g. TEXTURE_2D</param>
        /// <param name="pname">The parameter to set.</param>
        /// <param name="parameters">The value to set it to.</param>
        public void TexParameter(Enumerations.TextureTarget target, Enumerations.TextureParameter pname, float[] parameters)
        {
            getDelegateFor<glTexParameterfv>(ref glTexParameterfvDelegate)((uint)target,(uint)pname, parameters);
        }

		/// <summary>
		///	This function sets the parameters for the currently binded texture object.
		/// </summary>
		/// <param name="target">The type of texture you are setting the parameter to, e.g. TEXTURE_2D</param>
		/// <param name="pname">The parameter to set.</param>
		/// <param name="param">The value to set it to.</param>
		public void TexParameter(uint target, uint pname, int param)
		{
			getDelegateFor<glTexParameteri>(ref glTexParameteriDelegate)(target, pname, param);
		}

        /// <summary>
        ///	This function sets the parameters for the currently binded texture object.
        /// </summary>
        /// <param name="target">The type of texture you are setting the parameter to, e.g. TEXTURE_2D</param>
        /// <param name="pname">The parameter to set.</param>
        /// <param name="param">The value to set it to.</param>
        public void TexParameter(Enumerations.TextureTarget target, Enumerations.TextureParameter pname, int param)
        {
            getDelegateFor<glTexParameteri>(ref glTexParameteriDelegate)((uint)target,(uint)pname, param);
        }

		/// <summary>
		///	This function sets the parameters for the currently binded texture object.
		/// </summary>
		/// <param name="target">The type of texture you are setting the parameter to, e.g. TEXTURE_2D</param>
		/// <param name="pname">The parameter to set.</param>
		/// <param name="parameters">The value to set it to.</param>
		public void TexParameter(uint target, uint pname, int[] parameters)
		{
			getDelegateFor<glTexParameteriv>(ref glTexParameterivDelegate)(target, pname, parameters);
		}

        /// <summary>
        ///	This function sets the parameters for the currently binded texture object.
        /// </summary>
        /// <param name="target">The type of texture you are setting the parameter to, e.g. TEXTURE_2D</param>
        /// <param name="pname">The parameter to set.</param>
        /// <param name="parameters">The value to set it to.</param>
        public void TexParameter(Enumerations.TextureTarget target, Enumerations.TextureParameter pname, int[] parameters)
        {
            getDelegateFor<glTexParameteriv>(ref glTexParameterivDelegate)((uint)target,(uint)pname, parameters);
        }

        /// <summary>
        /// Specify a two-dimensional texture subimage.
        /// </summary>
        /// <param name="target">Specifies the target texture. Must be OpenGL.TEXTURE_1D.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xoffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel	data.</param>
        /// <param name="pixels">Specifies a pointer to the image data in memory.</param>
        public void TexSubImage1D(uint target, int level, int xoffset, int width, uint format, uint type, int[] pixels)
        {
            getDelegateFor<glTexSubImage1D>(ref glTexSubImage1DDelegate)(target, level, xoffset, width, format, type, pixels);
        }

        /// <summary>
        /// Specify a two-dimensional texture subimage.
        /// </summary>
        /// <param name="target">Specifies the target texture. Must be OpenGL.TEXTURE_1D.</param>
        /// <param name="level">Specifies the level-of-detail number. Level 0 is the base image level. Level n is the nth mipmap reduction image.</param>
        /// <param name="xoffset">Specifies a texel offset in the x direction within the texture array.</param>
        /// <param name="yoffset">Specifies a texel offset in the y direction within the texture array.</param>
        /// <param name="width">Specifies the width of the texture subimage.</param>
        /// <param name="height">Specifies the height of the texture subimage.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type of the pixel	data.</param>
        /// <param name="pixels">Specifies a pointer to the image data in memory.</param>
        public void TexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, uint type, int[] pixels)
        {
            getDelegateFor<glTexSubImage2D>(ref glTexSubImage2DDelegate)(target, level, xoffset, yoffset, width, height, format, type, pixels);
        }

		/// <summary>
		/// This function applies a translation transformation to the current matrix.
		/// </summary>
		/// <param name="x">The amount to translate along the x axis.</param>
		/// <param name="y">The amount to translate along the y axis.</param>
		/// <param name="z">The amount to translate along the z axis.</param>
		public void Translate(double x, double y, double z)
		{
			getDelegateFor<glTranslated>(ref glTranslatedDelegate)(x, y, z);
		}

		/// <summary>
		/// This function applies a translation transformation to the current matrix.
		/// </summary>
		/// <param name="x">The amount to translate along the x axis.</param>
		/// <param name="y">The amount to translate along the y axis.</param>
		/// <param name="z">The amount to translate along the z axis.</param>
		public void Translate(float x, float y, float z)
		{
			getDelegateFor<glTranslatef>(ref glTranslatefDelegate)(x, y, z);
		}

		/// <summary>
		/// Set the current vertex(must be called between 'Begin' and 'End').
		/// </summary>
		/// <param name="x">X Value.</param>
		/// <param name="y">Y Value.</param>
		public void Vertex(double x, double y)
		{
			getDelegateFor<glVertex2d>(ref glVertex2dDelegate)(x, y);
		}

        /// <summary>
        /// Set the current vertex(must be called between 'Begin' and 'End').
        /// </summary>
        /// <param name="v">Specifies the coordinate.</param>
        public void Vertex(double[] v)
        {
            if(v.Length == 2)
                getDelegateFor<glVertex2dv>(ref glVertex2dvDelegate)(v);
            else if(v.Length == 3)
                getDelegateFor<glVertex3dv>(ref glVertex3dvDelegate)(v);
            else if(v.Length == 4)
                getDelegateFor<glVertex4dv>(ref glVertex4dvDelegate)(v);
        }

        /// <summary>
        /// Set the current vertex(must be called between 'Begin' and 'End').
        /// </summary>
        /// <param name="x">X Value.</param>
        /// <param name="y">Y Value.</param>
        public void Vertex(float x, float y)
        {
            getDelegateFor<glVertex2f>(ref glVertex2fDelegate)(x, y);
        }

        /// <summary>
        /// Set the current vertex(must be called between 'Begin' and 'End').
        /// </summary>
        /// <param name="x">X Value.</param>
        /// <param name="y">Y Value.</param>
        public void Vertex(int x, int y)
        {
            getDelegateFor<glVertex2i>(ref glVertex2iDelegate)(x, y);
        }

        /// <summary>
        /// Set the current vertex(must be called between 'Begin' and 'End').
        /// </summary>
        /// <param name="v">Specifies the coordinate.</param>
        public void Vertex(int[] v)
        {
            if(v.Length == 2)
                getDelegateFor<glVertex2iv>(ref glVertex2ivDelegate)(v);
            else if(v.Length == 3)
                getDelegateFor<glVertex3iv>(ref glVertex3ivDelegate)(v);
            else if(v.Length == 4)
                getDelegateFor<glVertex4iv>(ref glVertex4ivDelegate)(v);
        }

        /// <summary>
        /// Set the current vertex(must be called between 'Begin' and 'End').
        /// </summary>
        /// <param name="x">X Value.</param>
        /// <param name="y">Y Value.</param>
        public void Vertex(short x, short y)
        {
            getDelegateFor<glVertex2s>(ref glVertex2sDelegate)(x, y);
        }

        /// <summary>
        /// Set the current vertex(must be called between 'Begin' and 'End').
        /// </summary>
        /// <param name="v">Specifies the coordinate.</param>
        public void Vertex2sv(short[] v)
        {
            if(v.Length == 2)
                getDelegateFor<glVertex2sv>(ref glVertex2svDelegate)(v);
            else if(v.Length == 3)
                getDelegateFor<glVertex3sv>(ref glVertex3svDelegate)(v);
            else if(v.Length == 4)
                getDelegateFor<glVertex4sv>(ref glVertex4svDelegate)(v);
        }

		/// <summary>
		/// Set the current vertex(must be called between 'Begin' and 'End').
		/// </summary>
		/// <param name="x">X Value.</param>
		/// <param name="y">Y Value.</param>
		/// <param name="z">Z Value.</param>
		public void Vertex(double x, double y, double z)
		{
			getDelegateFor<glVertex3d>(ref glVertex3dDelegate)(x, y, z);
		}

		/// <summary>
		/// Set the current vertex(must be called between 'Begin' and 'End').
		/// </summary>
		/// <param name="x">X Value.</param>
		/// <param name="y">Y Value.</param>
		/// <param name="z">Z Value.</param>
		public void Vertex(float x, float y, float z)
		{
			getDelegateFor<glVertex3f>(ref glVertex3fDelegate)(x, y, z);
		}

		/// <summary>
		/// Sets the current vertex(must be called between 'Begin' and 'End').
		/// </summary>
		/// <param name="v">An array of 2, 3 or 4 floats.</param>
		public void Vertex(float[] v)
		{
			if(v.Length == 2)
				getDelegateFor<glVertex2fv>(ref glVertex2fvDelegate)(v);
			else if(v.Length == 3)
				getDelegateFor<glVertex3fv>(ref glVertex3fvDelegate)(v);
			else if(v.Length == 4)
				getDelegateFor<glVertex4fv>(ref glVertex4fvDelegate)(v);
		}

        /// <summary>
        /// Set the current vertex(must be called between 'Begin' and 'End').
        /// </summary>
        /// <param name="x">X Value.</param>
        /// <param name="y">Y Value.</param>
        /// <param name="z">Z Value.</param>
        public void Vertex(int x, int y, int z)
        {
            getDelegateFor<glVertex3i>(ref glVertex3iDelegate)(x, y, z);
        }

        /// <summary>
        /// Set the current vertex(must be called between 'Begin' and 'End').
        /// </summary>
        /// <param name="x">X Value.</param>
        /// <param name="y">Y Value.</param>
        /// <param name="z">Z Value.</param>
        public void Vertex(short x, short y, short z)
        {
            getDelegateFor<glVertex3s>(ref glVertex3sDelegate)(x, y, z);
        }

        /// <summary>
        /// Set the current vertex(must be called between 'Begin' and 'End').
        /// </summary>
        /// <param name="x">X Value.</param>
        /// <param name="y">Y Value.</param>
        /// <param name="z">Z Value.</param>
        /// <param name="w">W Value.</param>
        public void Vertex4d(double x, double y, double z, double w)
        {
            getDelegateFor<glVertex4d>(ref glVertex4dDelegate)(x, y, z, w);
        }

        /// <summary>
        /// Set the current vertex(must be called between 'Begin' and 'End').
        /// </summary>
        /// <param name="x">X Value.</param>
        /// <param name="y">Y Value.</param>
        /// <param name="z">Z Value.</param>
        /// <param name="w">W Value.</param>
        public void Vertex4f(float x, float y, float z, float w)
        {
            getDelegateFor<glVertex4f>(ref glVertex4fDelegate)(x, y, z, w);
        }

        /// <summary>
        /// Set the current vertex(must be called between 'Begin' and 'End').
        /// </summary>
        /// <param name="x">X Value.</param>
        /// <param name="y">Y Value.</param>
        /// <param name="z">Z Value.</param>
        /// <param name="w">W Value.</param>
        public void Vertex4i(int x, int y, int z, int w)
        {
            getDelegateFor<glVertex4i>(ref glVertex4iDelegate)(x, y, z, w);
        }

        /// <summary>
        /// Set the current vertex(must be called between 'Begin' and 'End').
        /// </summary>
        /// <param name="x">X Value.</param>
        /// <param name="y">Y Value.</param>
        /// <param name="z">Z Value.</param>
        /// <param name="w">W Value.</param>
        public void Vertex4s(short x, short y, short z, short w)
        {
            getDelegateFor<glVertex4s>(ref glVertex4sDelegate)(x, y, z, w);
        }

		/// <summary>
		/// This function sets the address of the vertex pointer array.
		/// </summary>
		/// <param name="size">The number of coords per vertex.</param>
		/// <param name="type">The data type.</param>
		/// <param name="stride">The byte offset between vertices.</param>
		/// <param name="pointer">The array.</param>
		public void VertexPointer(int size, uint type, int stride, IntPtr pointer)
		{
			getDelegateFor<glVertexPointer>(ref glVertexPointerDelegate)(size, type, stride, pointer);
		}

        /// <summary>
        /// This function sets the address of the vertex pointer array.
        /// </summary>
        /// <param name="size">The number of coords per vertex.</param>
        /// <param name="stride">The byte offset between vertices.</param>
        /// <param name="pointer">The array.</param>
        public void VertexPointer(int size, int stride, short[] pointer)
        {
            var pinned = GCHandle.Alloc(pointer, GCHandleType.Pinned);
            getDelegateFor<glVertexPointer>(ref glVertexPointerDelegate)(size, GL_SHORT, stride, pinned.AddrOfPinnedObject());
            pinned.Free();
        }

        /// <summary>
        /// This function sets the address of the vertex pointer array.
        /// </summary>
        /// <param name="size">The number of coords per vertex.</param>
        /// <param name="stride">The byte offset between vertices.</param>
        /// <param name="pointer">The array.</param>
        public void VertexPointer(int size, int stride, int[] pointer)
        {
            var pinned = GCHandle.Alloc(pointer, GCHandleType.Pinned);
            getDelegateFor<glVertexPointer>(ref glVertexPointerDelegate)(size, GL_INT, stride, pinned.AddrOfPinnedObject());
            pinned.Free();
        }

        /// <summary>
        /// This function sets the address of the vertex pointer array.
        /// </summary>
        /// <param name="size">The number of coords per vertex.</param>
        /// <param name="stride">The byte offset between vertices.</param>
        /// <param name="pointer">The array.</param>
        public void VertexPointer(int size, int stride, float[] pointer)
        {
            var pinned = GCHandle.Alloc(pointer, GCHandleType.Pinned);
            getDelegateFor<glVertexPointer>(ref glVertexPointerDelegate)(size, GL_FLOAT, stride, pinned.AddrOfPinnedObject());
            pinned.Free();
        }

        /// <summary>
        /// This function sets the address of the vertex pointer array.
        /// </summary>
        /// <param name="size">The number of coords per vertex.</param>
        /// <param name="stride">The byte offset between vertices.</param>
        /// <param name="pointer">The array.</param>
        public void VertexPointer(int size, int stride, double[] pointer)
        {
            var pinned = GCHandle.Alloc(pointer, GCHandleType.Pinned);
            getDelegateFor<glVertexPointer>(ref glVertexPointerDelegate)(size, GL_DOUBLE, stride, pinned.AddrOfPinnedObject());
            pinned.Free();
        }

		/// <summary>
		/// This sets the viewport of the current Render Context. Normally x and y are 0
		/// and the width and height are just those of the control/graphics you are drawing
		/// to.
		/// </summary>
		/// <param name="x">Top-Left point of the viewport.</param>
		/// <param name="y">Top-Left point of the viewport.</param>
		/// <param name="width">Width of the viewport.</param>
		/// <param name="height">Height of the viewport.</param>
		public void Viewport(int x, int y, int width, int height)
		{
			getDelegateFor<glViewport>(ref glViewportDelegate)(x, y, width, height);
		}

		#endregion

        #region GLU 1.2

        //   Version
		public const uint GLU_VERSION_1_1                 = 1;
		public const uint GLU_VERSION_1_2                 = 1;

		//   Errors:(return value 0 = no error)
		public const uint GLU_INVALID_ENUM        = 100900;
		public const uint GLU_INVALID_VALUE       = 100901;
		public const uint GLU_OUT_OF_MEMORY       = 100902;
		public const uint GLU_INCOMPATIBLE_GL_VERSION    = 100903;

		//   StringName
		public const uint GLU_VERSION             = 100800;
		public const uint GLU_EXTENSIONS          = 100801;

		//   Boolean
		public const uint GLU_TRUE                = 1;
		public const uint GLU_FALSE               = 0;

        //  Quadric constants

		//   QuadricNormal
		public const uint GLU_SMOOTH              = 100000;
		public const uint GLU_FLAT                = 100001;
		public const uint GLU_NONE                = 100002;

		//   QuadricDrawStyle
		public const uint GLU_POINT               = 100010;
		public const uint GLU_LINE                = 100011;
		public const uint GLU_FILL                = 100012;
		public const uint GLU_SILHOUETTE          = 100013;

		//   QuadricOrientation
		public const uint GLU_OUTSIDE             = 100020;
		public const uint GLU_INSIDE              = 100021;

		//  Tesselation constants
		public const double GLU_TESS_MAX_COORD             = 1.0e150;

		//   TessProperty
		public const uint GLU_TESS_WINDING_RULE           =100140;
		public const uint GLU_TESS_BOUNDARY_ONLY          =100141;
		public const uint GLU_TESS_TOLERANCE              =100142;

		//   TessWinding
		public const uint GLU_TESS_WINDING_ODD            =100130;
		public const uint GLU_TESS_WINDING_NONZERO        =100131;
		public const uint GLU_TESS_WINDING_POSITIVE       =100132;
		public const uint GLU_TESS_WINDING_NEGATIVE       =100133;
		public const uint GLU_TESS_WINDING_ABS_GEQ_TWO    =100134;

		//   TessCallback
		public const uint GLU_TESS_BEGIN          =100100;
		public const uint GLU_TESS_VERTEX         =100101;
		public const uint GLU_TESS_END            =100102;
		public const uint GLU_TESS_ERROR          =100103;
		public const uint GLU_TESS_EDGE_FLAG      =100104;
		public const uint GLU_TESS_COMBINE        =100105;
		public const uint GLU_TESS_BEGIN_DATA     =100106;
		public const uint GLU_TESS_VERTEX_DATA    =100107;
		public const uint GLU_TESS_END_DATA       =100108;
		public const uint GLU_TESS_ERROR_DATA     =100109;
		public const uint GLU_TESS_EDGE_FLAG_DATA =100110;
		public const uint GLU_TESS_COMBINE_DATA   =100111;

		//   TessError
		public const uint GLU_TESS_ERROR1     =100151;
		public const uint GLU_TESS_ERROR2     =100152;
		public const uint GLU_TESS_ERROR3     =100153;
		public const uint GLU_TESS_ERROR4     =100154;
		public const uint GLU_TESS_ERROR5     =100155;
		public const uint GLU_TESS_ERROR6     =100156;
		public const uint GLU_TESS_ERROR7     =100157;
		public const uint GLU_TESS_ERROR8     =100158;

		public const uint GLU_TESS_MISSING_BEGIN_POLYGON  =100151;
		public const uint GLU_TESS_MISSING_BEGIN_CONTOUR  =100152;
		public const uint GLU_TESS_MISSING_END_POLYGON    =100153;
		public const uint GLU_TESS_MISSING_END_CONTOUR    =100154;
		public const uint GLU_TESS_COORD_TOO_LARGE        =100155;
		public const uint GLU_TESS_NEED_COMBINE_CALLBACK  =100156;

		//  NURBS constants

		//   NurbsProperty
		public const uint GLU_AUTO_LOAD_MATRIX    =100200;
		public const uint GLU_CULLING             =100201;
		public const uint GLU_SAMPLING_TOLERANCE  =100203;
		public const uint GLU_DISPLAY_MODE        =100204;
		public const uint GLU_PARAMETRIC_TOLERANCE        =100202;
		public const uint GLU_SAMPLING_METHOD             =100205;
		public const uint GLU_U_STEP                      =100206;
		public const uint GLU_V_STEP                      =100207;

		//   NurbsSampling
		public const uint GLU_PATH_LENGTH                 =100215;
		public const uint GLU_PARAMETRIC_ERROR            =100216;
		public const uint GLU_DOMAIN_DISTANCE             =100217;


		//   NurbsTrim
		public const uint GLU_MAP1_TRIM_2         =100210;
		public const uint GLU_MAP1_TRIM_3         =100211;

		//   NurbsDisplay
		//        GLU_FILL                100012
		public const uint GLU_OUTLINE_POLYGON     =100240;
		public const uint GLU_OUTLINE_PATCH       =100241;

		//   NurbsCallback
		//        GLU_ERROR               100103

		//   NurbsErrors
		public const uint GLU_NURBS_ERROR1        =100251;
		public const uint GLU_NURBS_ERROR2        =100252;
		public const uint GLU_NURBS_ERROR3        =100253;
		public const uint GLU_NURBS_ERROR4        =100254;
		public const uint GLU_NURBS_ERROR5        =100255;
		public const uint GLU_NURBS_ERROR6        =100256;
		public const uint GLU_NURBS_ERROR7        =100257;
		public const uint GLU_NURBS_ERROR8        =100258;
		public const uint GLU_NURBS_ERROR9        =100259;
		public const uint GLU_NURBS_ERROR10       =100260;
		public const uint GLU_NURBS_ERROR11       =100261;
		public const uint GLU_NURBS_ERROR12       =100262;
		public const uint GLU_NURBS_ERROR13       =100263;
		public const uint GLU_NURBS_ERROR14       =100264;
		public const uint GLU_NURBS_ERROR15       =100265;
		public const uint GLU_NURBS_ERROR16       =100266;
		public const uint GLU_NURBS_ERROR17       =100267;
		public const uint GLU_NURBS_ERROR18       =100268;
		public const uint GLU_NURBS_ERROR19       =100269;
		public const uint GLU_NURBS_ERROR20       =100270;
		public const uint GLU_NURBS_ERROR21       =100271;
		public const uint GLU_NURBS_ERROR22       =100272;
		public const uint GLU_NURBS_ERROR23       =100273;
		public const uint GLU_NURBS_ERROR24       =100274;
		public const uint GLU_NURBS_ERROR25       =100275;
		public const uint GLU_NURBS_ERROR26       =100276;
		public const uint GLU_NURBS_ERROR27       =100277;
		public const uint GLU_NURBS_ERROR28       =100278;
		public const uint GLU_NURBS_ERROR29       =100279;
		public const uint GLU_NURBS_ERROR30       =100280;
		public const uint GLU_NURBS_ERROR31       =100281;
		public const uint GLU_NURBS_ERROR32       =100282;
		public const uint GLU_NURBS_ERROR33       =100283;
		public const uint GLU_NURBS_ERROR34       =100284;
		public const uint GLU_NURBS_ERROR35       =100285;
		public const uint GLU_NURBS_ERROR36       =100286;
		public const uint GLU_NURBS_ERROR37       =100287;

		// Delegates
		private delegate IntPtr gluErrorString(uint errCode);
		private Delegate gluErrorStringDelegate;
		private delegate IntPtr gluGetString(int name);
		private Delegate gluGetStringDelegate;
		private delegate void gluOrtho2D(double left, double right, double bottom, double top);
		private Delegate gluOrtho2DDelegate;
		private delegate void gluPerspective(double fovy, double aspect, double zNear, double zFar);
		private Delegate gluPerspectiveDelegate;
		private delegate void gluPickMatrix(double x, double y, double width, double height, int[] viewport);
		private Delegate gluPickMatrixDelegate;
		private delegate void gluLookAt(double eyex, double eyey, double eyez, double centerx, double centery, double centerz, double upx, double upy, double upz);
		private Delegate gluLookAtDelegate;
		private delegate void gluProject(double objx, double objy, double objz, double[]  modelMatrix, double[]  projMatrix, int[] viewport, double[]  winx, double[] winy, double[] winz);
		private Delegate gluProjectDelegate;
		private delegate void gluUnProject(double winx, double winy, double winz, double[] modelMatrix, double[] projMatrix, int[] viewport, ref double objx, ref double objy, ref double objz);
		private Delegate gluUnProjectDelegate;
		private delegate void gluScaleImage(int format, int widthin, int heightin, int typein, int [] datain, int widthout, int heightout, int  typeout, int[] dataout);
		private Delegate gluScaleImageDelegate;
		private delegate void gluBuild1DMipmaps(uint target, uint components, int width, uint format, uint type, IntPtr data);
		private Delegate gluBuild1DMipmapsDelegate;
		private delegate void gluBuild2DMipmaps(uint target, uint components, int width, int height, uint format, uint type, IntPtr data);
		private Delegate gluBuild2DMipmapsDelegate;
		private delegate IntPtr gluNewQuadric();
		private Delegate gluNewQuadricDelegate;
		private delegate void gluDeleteQuadric(IntPtr state);
		private Delegate gluDeleteQuadricDelegate;
		private delegate void gluQuadricNormals(IntPtr quadObject, uint normals);
		private Delegate gluQuadricNormalsDelegate;
		private delegate void gluQuadricTexture(IntPtr quadObject, int textureCoords);
		private Delegate gluQuadricTextureDelegate;
		private delegate void gluQuadricOrientation(IntPtr quadObject, int orientation);
		private Delegate gluQuadricOrientationDelegate;
		private delegate void gluQuadricDrawStyle(IntPtr quadObject, uint drawStyle);
		private Delegate gluQuadricDrawStyleDelegate;
		private delegate void gluCylinder(IntPtr qobj,double baseRadius, double topRadius, double height,int slices,int stacks);
		private Delegate gluCylinderDelegate;
		private delegate void gluDisk(IntPtr qobj, double innerRadius,double outerRadius,int slices, int loops);
		private Delegate gluDiskDelegate;
		private delegate void gluPartialDisk(IntPtr qobj,double innerRadius,double outerRadius, int slices, int loops, double startAngle, double sweepAngle);
		private Delegate gluPartialDiskDelegate;
		private delegate void gluSphere(IntPtr qobj, double radius, int slices, int stacks);
		private Delegate gluSphereDelegate;
		private delegate IntPtr gluNewTess();
		private Delegate gluNewTessDelegate;
		private delegate void  gluDeleteTess(IntPtr tess);
		private Delegate  gluDeleteTessDelegate;
		private delegate void  gluTessBeginPolygon(IntPtr tess, IntPtr polygonData);
		private Delegate  gluTessBeginPolygonDelegate;
		private delegate void  gluTessBeginContour(IntPtr tess);
		private Delegate  gluTessBeginContourDelegate;
		private delegate void  gluTessVertex(IntPtr tess,double[] coords, double[] data);
		private Delegate  gluTessVertexDelegate;
		private delegate void  gluTessEndContour(IntPtr tess);
		private Delegate  gluTessEndContourDelegate;
		private delegate void  gluTessEndPolygon(IntPtr tess);
		private Delegate  gluTessEndPolygonDelegate;
		private delegate void  gluTessProperty(IntPtr tess,int which, double value);
		private Delegate  gluTessPropertyDelegate;
		private delegate void  gluTessNormal(IntPtr tess, double x,double y, double z);
		private Delegate  gluTessNormalDelegate;
		private delegate void  gluGetTessProperty(IntPtr tess,int which, double value);
		private Delegate  gluGetTessPropertyDelegate;
		private delegate IntPtr gluNewNurbsRenderer();
		private Delegate gluNewNurbsRendererDelegate;
		private delegate void gluDeleteNurbsRenderer(IntPtr nobj);
		private Delegate gluDeleteNurbsRendererDelegate;
		private delegate void gluBeginSurface(IntPtr nobj);
		private Delegate gluBeginSurfaceDelegate;
		private delegate void gluBeginCurve(IntPtr nobj);
		private Delegate gluBeginCurveDelegate;
		private delegate void gluEndCurve(IntPtr nobj);
		private Delegate gluEndCurveDelegate;
		private delegate void gluEndSurface(IntPtr nobj);
		private Delegate gluEndSurfaceDelegate;
		private delegate void gluBeginTrim(IntPtr nobj);
		private Delegate gluBeginTrimDelegate;
		private delegate void gluEndTrim(IntPtr nobj);
		private Delegate gluEndTrimDelegate;
		private delegate void gluPwlCurve(IntPtr nobj, int  count, float  array, int stride, uint type);
		private Delegate gluPwlCurveDelegate;
		private delegate void gluNurbsCurve(IntPtr nobj, int nknots, float[] knot, int  stride, float[] ctlarray, int  order, uint type);
		private Delegate gluNurbsCurveDelegate;
		private delegate void gluNurbsSurface(IntPtr nobj, int sknot_count, float[] sknot, int tknot_count, float[]  tknot, int  s_stride, int  t_stride, float[] ctlarray, int sorder, int  torder, uint type);
		private Delegate gluNurbsSurfaceDelegate;
		private delegate void gluLoadSamplingMatrices(IntPtr nobj, float[] modelMatrix, float[] projMatrix, int[] viewport);
		private Delegate gluLoadSamplingMatricesDelegate;
		private delegate void gluNurbsProperty(IntPtr nobj, int property, float value);
		private Delegate gluNurbsPropertyDelegate;
		private delegate void gluGetNurbsProperty(IntPtr nobj, int property, float  value);
		private Delegate gluGetNurbsPropertyDelegate;

        /// <summary>
        /// Produce an error string from a GL or GLU error code.
        /// </summary>
        /// <param name="errCode">Specifies a GL or GLU error code.</param>
        /// <returns>The OpenGL/GLU error string.</returns>
		public string ErrorString(uint errCode)
        {
            var chars = getDelegateFor<gluErrorString>(ref gluErrorStringDelegate)(errCode);
			return chars == IntPtr.Zero ? null : Marshal.PtrToStringAnsi(chars);
        }

        /// <summary>
        /// Return a string describing the GLU version or GLU extensions.
        /// </summary>
        /// <param name="name">Specifies a symbolic constant, one of OpenGL.VERSION, or OpenGL.EXTENSIONS.</param>
        /// <returns>The GLU string.</returns>
		public string GetString(int name)
        {
            var chars = getDelegateFor<gluGetString>(ref gluGetStringDelegate)(name);
			return chars == IntPtr.Zero ? null : Marshal.PtrToStringAnsi(chars);
        }

        /// <summary>
        /// Scale an image to an arbitrary size.
        /// </summary>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="widthin">Specify the width of the source image	that is	scaled.</param>
        /// <param name="heightin">Specify the height of the source image that is scaled.</param>
        /// <param name="typein">Specifies the data type for dataIn.</param>
        /// <param name="datain">Specifies a pointer to the source image.</param>
        /// <param name="widthout">Specify the width of the destination image.</param>
        /// <param name="heightout">Specify the height of the destination image.</param>
        /// <param name="typeout">Specifies the data type for dataOut.</param>
        /// <param name="dataout">Specifies a pointer to the destination image.</param>
		public void ScaleImage(int format, int widthin, int heightin, int typein, int[] datain, int widthout, int heightout, int typeout, int[] dataout)
        {
            getDelegateFor<gluScaleImage>(ref gluScaleImageDelegate)(format, widthin, heightin, typein, datain, widthout, heightout, typeout, dataout);
        }

        /// <summary>
        /// Create 1-D mipmaps.
        /// </summary>
        /// <param name="target">Specifies the target texture. Must be OpenGL.TEXTURE_1D.</param>
        /// <param name="components">Specifies the number of color components in the texture. Must be 1, 2, 3, or 4.</param>
        /// <param name="width">Specifies the width of the texture image.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type for data.</param>
        /// <param name="data">Specifies a pointer to the image data in memory.</param>
        public void Build1DMipmaps(uint target, uint components, int width, uint format, uint type, IntPtr data)
        {
            getDelegateFor<gluBuild1DMipmaps>(ref gluBuild1DMipmapsDelegate)(target, components, width, format, type, data);
        }

        /// <summary>
        /// Create 2-D mipmaps.
        /// </summary>
        /// <param name="target">Specifies the target texture. Must be OpenGL.TEXTURE_1D.</param>
        /// <param name="components">Specifies the number of color components in the texture. Must be 1, 2, 3, or 4.</param>
        /// <param name="width">Specifies the width of the texture image.</param>
        /// <param name="height">Specifies the height of the texture image.</param>
        /// <param name="format">Specifies the format of the pixel data.</param>
        /// <param name="type">Specifies the data type for data.</param>
        /// <param name="data">Specifies a pointer to the image data in memory.</param>
		public void Build2DMipmaps(uint target, uint components, int width, int height, uint format, uint type, IntPtr data)
        {
            getDelegateFor<gluBuild2DMipmaps>(ref gluBuild2DMipmapsDelegate)(target, components, width, height, format, type, data);
        }

        /// <summary>
        /// Draw a disk.
        /// </summary>
        /// <param name="qobj">Specifies the quadrics object(created with gluNewQuadric).</param>
        /// <param name="innerRadius">Specifies the	inner radius of	the disk(may be 0).</param>
        /// <param name="outerRadius">Specifies the	outer radius of	the disk.</param>
        /// <param name="slices">Specifies the	number of subdivisions around the z axis.</param>
        /// <param name="loops">Specifies the	number of concentric rings about the origin into which the disk is subdivided.</param>
		public void Disk(IntPtr qobj, double innerRadius, double outerRadius, int slices, int loops)
        {
            getDelegateFor<gluDisk>(ref gluDiskDelegate)(qobj, innerRadius, outerRadius, slices, loops);
        }

        /// <summary>
        /// Create a tessellation object.
        /// </summary>
        /// <returns>A new GLUtesselator poiner.</returns>
		public IntPtr NewTess()
        {
            IntPtr returnValue = getDelegateFor<gluNewTess>(ref gluNewTessDelegate)();
            return returnValue;
        }

        /// <summary>
        /// Delete a tesselator object.
        /// </summary>
        /// <param name="tess">The tesselator pointer.</param>
		public void DeleteTess(IntPtr tess)
        {
            getDelegateFor<gluDeleteTess>(ref gluDeleteTessDelegate)(tess);
        }

        /// <summary>
        /// Delimit a polygon description.
        /// </summary>
        /// <param name="tess">Specifies the tessellation object(created with gluNewTess).</param>
        /// <param name="polygonData">Specifies a pointer to user polygon data.</param>
		public void TessBeginPolygon(IntPtr tess, IntPtr polygonData)
        {
            getDelegateFor<gluTessBeginPolygon>(ref gluTessBeginPolygonDelegate)(tess, polygonData);
        }

        /// <summary>
        /// Delimit a contour description.
        /// </summary>
        /// <param name="tess">Specifies the tessellation object(created with gluNewTess).</param>
		public void TessBeginContour(IntPtr tess)
        {
            getDelegateFor<gluTessBeginContour>(ref gluTessBeginContourDelegate)(tess);
        }

        /// <summary>
        /// Specify a vertex on a polygon.
        /// </summary>
        /// <param name="tess">Specifies the tessellation object(created with gluNewTess).</param>
        /// <param name="coords">Specifies the location of the vertex.</param>
        /// <param name="data">Specifies an opaque	pointer	passed back to the program with the vertex callback(as specified by gluTessCallback).</param>
		public void TessVertex(IntPtr tess, double[] coords, double[] data)
        {
            getDelegateFor<gluTessVertex>(ref gluTessVertexDelegate)(tess, coords, data);
        }

        /// <summary>
        /// Delimit a contour description.
        /// </summary>
        /// <param name="tess">Specifies the tessellation object(created with gluNewTess).</param>
		public void TessEndContour(IntPtr tess)
        {
            getDelegateFor<gluTessEndContour>(ref gluTessEndContourDelegate)(tess);
        }

        /// <summary>
        /// Delimit a polygon description.
        /// </summary>
        /// <param name="tess">Specifies the tessellation object(created with gluNewTess).</param>
		public void TessEndPolygon(IntPtr tess)
        {
            getDelegateFor<gluTessEndPolygon>(ref gluTessEndPolygonDelegate)(tess);
        }

        /// <summary>
        /// Set a tessellation object property.
        /// </summary>
        /// <param name="tess">Specifies the tessellation object(created with gluNewTess).</param>
        /// <param name="which">Specifies the property to be set.</param>
        /// <param name="value">Specifies the value of	the indicated property.</param>
		public void TessProperty(IntPtr tess, int which, double value)
        {
            getDelegateFor<gluTessProperty>(ref gluTessPropertyDelegate)(tess, which, value);
        }

        /// <summary>
        /// Specify a normal for a polygon.
        /// </summary>
        /// <param name="tess">Specifies the tessellation object(created with gluNewTess).</param>
        /// <param name="x">Specifies the first component of the normal.</param>
        /// <param name="y">Specifies the second component of the normal.</param>
        /// <param name="z">Specifies the third component of the normal.</param>
		public void TessNormal(IntPtr tess, double x, double y, double z)
        {
            getDelegateFor<gluTessNormal>(ref gluTessNormalDelegate)(tess, x, y, z);
        }

        /// <summary>
        /// Set a tessellation object property.
        /// </summary>
        /// <param name="tess">Specifies the tessellation object(created with gluNewTess).</param>
        /// <param name="which">Specifies the property	to be set.</param>
        /// <param name="value">Specifies the value of	the indicated property.</param>
		public void GetTessProperty(IntPtr tess, int which, double value)
        {
            getDelegateFor<gluGetTessProperty>(ref gluGetTessPropertyDelegate)(tess, which, value);
        }

        /// <summary>
        /// Delimit a NURBS trimming loop definition.
        /// </summary>
        /// <param name="nobj">Specifies the NURBS object(created with gluNewNurbsRenderer).</param>
		public void BeginTrim(IntPtr nobj)
        {
            getDelegateFor<gluBeginTrim>(ref gluBeginTrimDelegate)(nobj);
        }

        /// <summary>
        /// Delimit a NURBS trimming loop definition.
        /// </summary>
        /// <param name="nobj">Specifies the NURBS object(created with gluNewNurbsRenderer).</param>
		public void EndTrim(IntPtr nobj)
        {
            getDelegateFor<gluEndTrim>(ref gluEndTrimDelegate)(nobj);
        }

        /// <summary>
        /// Describe a piecewise linear NURBS trimming curve.
        /// </summary>
        /// <param name="nobj">Specifies the NURBS object(created with gluNewNurbsRenderer).</param>
        /// <param name="count">Specifies the number of points on the curve.</param>
        /// <param name="array">Specifies an array containing the curve points.</param>
        /// <param name="stride">Specifies the offset(a number of single-precision floating-point values) between points on the curve.</param>
        /// <param name="type">Specifies the type of curve. Must be either OpenGL.MAP1_TRIM_2 or OpenGL.MAP1_TRIM_3.</param>
		public void PwlCurve(IntPtr nobj, int count, float array, int stride, uint type)
        {
            getDelegateFor<gluPwlCurve>(ref gluPwlCurveDelegate)(nobj, count, array, stride, type);
        }

        /// <summary>
        /// Load NURBS sampling and culling matrice.
        /// </summary>
        /// <param name="nobj">Specifies the NURBS object(created with gluNewNurbsRenderer).</param>
        /// <param name="modelMatrix">Specifies a modelview matrix(as from a glGetFloatv call).</param>
        /// <param name="projMatrix">Specifies a projection matrix(as from a glGetFloatv call).</param>
        /// <param name="viewport">Specifies a viewport(as from a glGetIntegerv call).</param>
		public void LoadSamplingMatrices(IntPtr nobj, float[] modelMatrix, float[] projMatrix, int[] viewport)
        {
            getDelegateFor<gluLoadSamplingMatrices>(ref gluLoadSamplingMatricesDelegate)(nobj, modelMatrix, projMatrix, viewport);
        }

        /// <summary>
        /// Get a NURBS property.
        /// </summary>
        /// <param name="nobj">Specifies the NURBS object(created with gluNewNurbsRenderer).</param>
        /// <param name="property">Specifies the property whose value is to be fetched.</param>
        /// <param name="value">Specifies a pointer to the location into which the value of the named property is written.</param>
		public void GetNurbsProperty(IntPtr nobj, int property, float value)
        {
            getDelegateFor<gluGetNurbsProperty>(ref gluGetNurbsPropertyDelegate)(nobj, property, value);
        }
		

		/// <summary>
		/// This function begins drawing a NURBS curve.
		/// </summary>
		/// <param name="nurbsObject">The NURBS object.</param>
        public void BeginCurve(IntPtr nurbsObject)
		{
			getDelegateFor<gluBeginCurve>(ref gluBeginCurveDelegate)(nurbsObject);
		}

		/// <summary>
		/// This function begins drawing a NURBS surface.
		/// </summary>
		/// <param name="nurbsObject">The NURBS object.</param>
		public void BeginSurface(IntPtr nurbsObject)
		{
			getDelegateFor<gluBeginSurface>(ref gluBeginSurfaceDelegate)(nurbsObject);
		}


		/// <summary>
		/// This function draws a sphere from the quadric object.
		/// </summary>
		/// <param name="qobj">The quadric object.</param>
		/// <param name="baseRadius">Radius at the base.</param>
		/// <param name="topRadius">Radius at the top.</param>
		/// <param name="height">Height of cylinder.</param>
		/// <param name="slices">Cylinder slices.</param>
		/// <param name="stacks">Cylinder stacks.</param>
		public void Cylinder(IntPtr qobj, double baseRadius, double topRadius, double height,int slices, int stacks)
		{
			getDelegateFor<gluCylinder>(ref gluCylinderDelegate)(qobj, baseRadius, topRadius, height, slices, stacks);
		}

		/// <summary>
		/// This function deletes the underlying glu nurbs renderer.
		/// </summary>
		/// <param name="nurbsObject">The pointer to the nurbs object.</param>
		public void DeleteNurbsRenderer(IntPtr nurbsObject)
		{
			getDelegateFor<gluDeleteNurbsRenderer>(ref gluDeleteNurbsRendererDelegate)(nurbsObject);
		}

		/// <summary>
		/// Call this function to delete an OpenGL Quadric object.
		/// </summary>
		/// <param name="quadric"></param>
		public void DeleteQuadric(IntPtr quadric)
		{
			getDelegateFor<gluDeleteQuadric>(ref gluDeleteQuadricDelegate)(quadric);
		}

        /// <summary>
        /// This function ends the drawing of a NURBS curve.
        /// </summary>
        /// <param name="nurbsObject">The nurbs object.</param>
		public void EndCurve(IntPtr nurbsObject)
		{
			getDelegateFor<gluEndCurve>(ref gluEndCurveDelegate)(nurbsObject);
		}

        /// <summary>
        /// This function ends the drawing of a NURBS surface.
        /// </summary>
        /// <param name="nurbsObject">The nurbs object.</param>
		public void EndSurface(IntPtr nurbsObject)
		{
			getDelegateFor<gluEndSurface>(ref gluEndSurfaceDelegate)(nurbsObject);
		}
		
		/// <summary>
		/// This function transforms the projection matrix so that it looks at a certain
		/// point, from a certain point.
		/// </summary>
		/// <param name="eyex">Position of the eye.</param>
		/// <param name="eyey">Position of the eye.</param>
		/// <param name="eyez">Position of the eye.</param>
		/// <param name="centerx">Point to look at.</param>
		/// <param name="centery">Point to look at.</param>
		/// <param name="centerz">Point to look at.</param>
		/// <param name="upx">'Up' Vector X Component.</param>
		/// <param name="upy">'Up' Vector Y Component.</param>
		/// <param name="upz">'Up' Vector Z Component.</param>
		public void LookAt(double eyex, double eyey, double eyez, 
			double centerx, double centery, double centerz, 
			double upx, double upy, double upz)
		{
			getDelegateFor<gluLookAt>(ref gluLookAtDelegate)(eyex, eyey, eyez, centerx, centery, centerz, upx, upy, upz);
		}

		/// <summary>
		/// This function creates a new glu NURBS renderer object.
		/// </summary>
		/// <returns>A Pointer to the NURBS renderer.</returns>
		public IntPtr NewNurbsRenderer()
		{
			IntPtr nurbs = getDelegateFor<gluNewNurbsRenderer>(ref gluNewNurbsRendererDelegate)();
			return nurbs;
		}

		/// <summary>
		/// This function creates a new OpenGL Quadric Object.
		/// </summary>
		/// <returns>The pointer to the Quadric Object.</returns>
		public IntPtr NewQuadric()
		{
			IntPtr quad = getDelegateFor<gluNewQuadric>(ref gluNewQuadricDelegate)();
			return quad;
		}

		/// <summary>
		/// This function defines a NURBS Curve.
		/// </summary>
		/// <param name="nurbsObject">The NURBS object.</param>
		/// <param name="knotsCount">The number of knots.</param>
		/// <param name="knots">The knots themselves.</param>
		/// <param name="stride">The stride, i.e. distance between vertices in the 
		/// control points array.</param>
		/// <param name="controlPointsArray">The array of control points.</param>
		/// <param name="order">The order of the polynomial.</param>
		/// <param name="type">The type of data to generate.</param>
		public void NurbsCurve(IntPtr nurbsObject, int knotsCount, float[] knots, 
			int stride, float[] controlPointsArray, int order, uint type)
		{
			getDelegateFor<gluNurbsCurve>(ref gluNurbsCurveDelegate)(nurbsObject, knotsCount, knots, stride, controlPointsArray,
				order, type);
		}

		/// <summary>
		/// This function sets a NURBS property.
		/// </summary>
		/// <param name="nurbsObject">The object to set the property for.</param>
		/// <param name="property">The property to set.</param>
		/// <param name="value">The new value of the property.</param>
		public void NurbsProperty(IntPtr nurbsObject, int property, float value)
		{
			getDelegateFor<gluNurbsProperty>(ref gluNurbsPropertyDelegate)(nurbsObject, property, value);
		}

		/// <summary>
		/// This function defines a NURBS surface.
		/// </summary>
		/// <param name="nurbsObject">The NURBS object.</param>
		/// <param name="sknotsCount">The sknots count.</param>
		/// <param name="sknots">The s-knots.</param>
		/// <param name="tknotsCount">The number of t-knots.</param>
		/// <param name="tknots">The t-knots.</param>
		/// <param name="sStride">The distance between s vertices.</param>
		/// <param name="tStride">The distance between t vertices.</param>
		/// <param name="controlPointsArray">The control points.</param>
		/// <param name="sOrder">The order of the s polynomial.</param>
		/// <param name="tOrder">The order of the t polynomial.</param>
		/// <param name="type">The type of data to generate.</param>
		public void NurbsSurface(IntPtr nurbsObject, int sknotsCount, float[] sknots, 
			int tknotsCount, float[] tknots, int sStride, int tStride, 
			float[] controlPointsArray, int sOrder, int tOrder, uint type)
		{
			getDelegateFor<gluNurbsSurface>(ref gluNurbsSurfaceDelegate)(nurbsObject, sknotsCount, sknots, tknotsCount, tknots,
				sStride, tStride, controlPointsArray, sOrder, tOrder, type);
		}
		
        /// <summary>
		/// This function creates an orthographic project based on a screen size.
		/// </summary>
		/// <param name="left">Left of the screen.(Normally 0).</param>
		/// <param name="right">Right of the screen.(Normally width).</param>
		/// <param name="bottom">Bottom of the screen(normally 0).</param>
		/// <param name="top">Top of the screen(normally height).</param>
		public void Ortho2D(double left, double right, double bottom, double top)
		{
			getDelegateFor<gluOrtho2D>(ref gluOrtho2DDelegate)(left, right, bottom, top);
		}

		/// <summary>
		/// This function draws a partial disk from the quadric object.
		/// </summary>
		/// <param name="qobj">The Quadric objec.t</param>
		/// <param name="innerRadius">Radius of the inside of the disk.</param>
		/// <param name="outerRadius">Radius of the outside of the disk.</param>
		/// <param name="slices">The slices.</param>
		/// <param name="loops">The loops.</param>
		/// <param name="startAngle">Starting angle.</param>
		/// <param name="sweepAngle">Sweep angle.</param>
		public void PartialDisk(IntPtr qobj,double innerRadius,double outerRadius, int slices, int loops, double startAngle, double sweepAngle)
		{
			getDelegateFor<gluPartialDisk>(ref gluPartialDiskDelegate)(qobj, innerRadius, outerRadius, slices, loops, startAngle, sweepAngle);
		}

		/// <summary>
		/// This function creates a perspective matrix and multiplies it to the current
		/// matrix stack(which in most cases should be 'PROJECTION').
		/// </summary>
		/// <param name="fovy">Field of view angle(human eye = 60 Degrees).</param>
		/// <param name="aspect">Apsect Ratio(width of screen divided by height of screen).</param>
		/// <param name="zNear">Near clipping plane(normally 1).</param>
		/// <param name="zFar">Far clipping plane.</param>
		public void Perspective(double fovy, double aspect, double zNear, double zFar)
		{
			getDelegateFor<gluPerspective>(ref gluPerspectiveDelegate)(fovy, aspect, zNear, zFar);
		}

		/// <summary>
		/// This function creates a 'pick matrix' normally used for selecting objects that
		/// are at a certain point on the screen.
		/// </summary>
		/// <param name="x">X Point.</param>
		/// <param name="y">Y Point.</param>
		/// <param name="width">Width of point to test(4 is normal).</param>
		/// <param name="height">Height of point to test(4 is normal).</param>
		/// <param name="viewport">The current viewport.</param>
		public void PickMatrix(double x, double y, double width, double height, int[] viewport)
		{
			getDelegateFor<gluPickMatrix>(ref gluPickMatrixDelegate)(x, y, width, height, viewport);
		}

		/// <summary>
		/// This function Maps the specified object coordinates into window coordinates.
		/// </summary>
		/// <param name="objx">The object's x coord.</param>
		/// <param name="objy">The object's y coord.</param>
		/// <param name="objz">The object's z coord.</param>
		/// <param name="modelMatrix">The modelview matrix.</param>
		/// <param name="projMatrix">The projection matrix.</param>
		/// <param name="viewport">The viewport.</param>
		/// <param name="winx">The window x coord.</param>
		/// <param name="winy">The Window y coord.</param>
		/// <param name="winz">The Window z coord.</param>
		public void Project(double objx, double objy, double objz, double[] modelMatrix, double[] projMatrix, int[] viewport, double[] winx, double[] winy, double[] winz)
		{
			getDelegateFor<gluProject>(ref gluProjectDelegate)(objx, objy, objz, modelMatrix, projMatrix, viewport, winx, winy, winz);
		}		

		/// <summary>
		/// This set's the Generate Normals propery of the specified Quadric object.
		/// </summary>
		/// <param name="quadricObject">The quadric object.</param>
		/// <param name="normals">The type of normals to generate.</param>
		public void QuadricNormals(IntPtr quadricObject, uint normals)
		{
			getDelegateFor<gluQuadricNormals>(ref gluQuadricNormalsDelegate)(quadricObject, normals);
		}

		/// <summary>
		/// This function sets the type of texture coordinates being generated by
		/// the specified quadric object.
		/// </summary>
		/// <param name="quadricObject">The quadric object.</param>
		/// <param name="textureCoords">The type of coordinates to generate.</param>
		public void QuadricTexture(IntPtr quadricObject, int textureCoords)
		{
			getDelegateFor<gluQuadricTexture>(ref gluQuadricTextureDelegate)(quadricObject, textureCoords);
		}

		/// <summary>
		/// This sets the orientation for the quadric object.
		/// </summary>
		/// <param name="quadricObject">The quadric object.</param>
		/// <param name="orientation">The orientation.</param>
		public void QuadricOrientation(IntPtr quadricObject, int orientation)
		{
			getDelegateFor<gluQuadricOrientation>(ref gluQuadricOrientationDelegate)(quadricObject, orientation);
		}

		/// <summary>
		/// This sets the current drawstyle for the Quadric Object.
		/// </summary>
		/// <param name="quadObject">The quadric object.</param>
		/// <param name="drawStyle">The draw style.</param>
		public void QuadricDrawStyle(IntPtr quadObject, uint drawStyle)
		{
			getDelegateFor<gluQuadricDrawStyle>(ref gluQuadricDrawStyleDelegate)(quadObject, drawStyle);
		}

		/// <summary>
		/// This function draws a sphere from a Quadric Object.
		/// </summary>
		/// <param name="qobj">The quadric object.</param>
		/// <param name="radius">Sphere radius.</param>
		/// <param name="slices">Slices of the sphere.</param>
		/// <param name="stacks">Stakcs of the sphere.</param>
		public void Sphere(IntPtr qobj, double radius, int slices, int stacks)
		{
			getDelegateFor<gluSphere>(ref gluSphereDelegate)(qobj, radius, slices, stacks);
		}


		/// <summary>
		/// This function turns a screen Coordinate into a world coordinate.
		/// </summary>
		/// <param name="winx">Screen Coordinate.</param>
		/// <param name="winy">Screen Coordinate.</param>
		/// <param name="winz">Screen Coordinate.</param>
		/// <param name="modelMatrix">Current ModelView matrix.</param>
		/// <param name="projMatrix">Current Projection matrix.</param>
		/// <param name="viewport">Current Viewport.</param>
		/// <param name="objx">The world coordinate.</param>
		/// <param name="objy">The world coordinate.</param>
		/// <param name="objz">The world coordinate.</param>
		public void UnProject(double winx, double winy, double winz, 
			double[] modelMatrix, double[] projMatrix, int[] viewport, 
			ref double objx, ref double objy, ref double objz)
		{
			getDelegateFor<gluUnProject>(ref gluUnProjectDelegate)(winx, winy, winz, modelMatrix, projMatrix, viewport,
				ref objx, ref objy, ref objz);
		}

		/// <summary>
		/// This is a convenience function. It calls UnProject with the current 
		/// viewport, modelview and persective matricies, saving you from getting them.
		/// To use you own matricies, all the other version of UnProject.
		/// </summary>
		/// <param name="winx">X Coordinate(Screen Coordinate).</param>
		/// <param name="winy">Y Coordinate(Screen Coordinate).</param>
		/// <param name="winz">Z Coordinate(Screen Coordinate).</param>
		/// <returns>The world coordinate.</returns>
		public double[] UnProject(double winx, double winy, double winz)
		{

			var modelview = new double[16];
			var projection = new double[16];
			var viewport = new int[4];
            GetDouble(GL_MODELVIEW_MATRIX, modelview);
            GetDouble(GL_PROJECTION_MATRIX, projection);
            GetInteger(GL_VIEWPORT, viewport);
            var result = new double[3];
            getDelegateFor<gluUnProject>(ref gluUnProjectDelegate)(winx, winy, winz, modelview, projection, viewport, ref result[0], ref result[1], ref result[2]);

			return result;
		}


		#endregion

        #region OpenGL 1.2

        //  Methods
        public void BlendColor(float red, float green, float blue, float alpha)
        {
            getDelegateFor<glBlendColor>(ref glBlendColorDelegate)(red, green, blue, alpha);
        }
        public void BlendEquation(uint mode)
        {
            getDelegateFor<glBlendEquation>(ref glBlendEquationDelegate)(mode);
        }
        public void DrawRangeElements(uint mode, uint start, uint end, int count, uint type, IntPtr indices)
        {
            getDelegateFor<glDrawRangeElements>(ref glDrawRangeElementsDelegate)(mode, start, end, count, type, indices);
        }
        public void TexImage3D(uint target, int level, int internalformat, int width, int height, int depth, int border, uint format, uint type, IntPtr pixels)
        {
            getDelegateFor<glTexImage3D>(ref glTexImage3DDelegate)(target, level, internalformat, width, height, depth, border, format, type, pixels);
        }
        public void TexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint type, IntPtr pixels)
        {
            getDelegateFor<glTexSubImage3D>(ref glTexSubImage3DDelegate)(target, level, xoffset, yoffset, zoffset, width, height, depth, format, type, pixels);
        }
        public void CopyTexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int x, int y, int width, int height)
        {
            getDelegateFor<glCopyTexSubImage3D>(ref glCopyTexSubImage3DDelegate)(target, level, xoffset, yoffset, zoffset, x, y, width, height);
        }

        //  Deprecated Methods
        [Obsolete]
        public void ColorTable(uint target, uint internalformat, int width, uint format, uint type, IntPtr table)
        {
            getDelegateFor<glColorTable>(ref glColorTableDelegate)(target, internalformat, width, format, type, table);
        }
        [Obsolete]
        public void ColorTableParameterfv(uint target, uint pname, float[] parameters)
        {
            getDelegateFor<glColorTableParameterfv>(ref glColorTableParameterfvDelegate)(target, pname, parameters);
        }
        [Obsolete]
        public void ColorTableParameteriv(uint target, uint pname, int[] parameters)
        {
            getDelegateFor<glColorTableParameteriv>(ref glColorTableParameterivDelegate)(target, pname, parameters);
        }
        [Obsolete]
        public void CopyColorTable(uint target, uint internalformat, int x, int y, int width)
        {
            getDelegateFor<glCopyColorTable>(ref glCopyColorTableDelegate)(target, internalformat, x, y, width);
        }
        [Obsolete]
        public void GetColorTable(uint target, uint format, uint type, IntPtr table)
        {
            getDelegateFor<glGetColorTable>(ref glGetColorTableDelegate)(target, format, type, table);
        }
        [Obsolete]
        public void GetColorTableParameter(uint target, uint pname, float[] parameters)
        {
            getDelegateFor<glGetColorTableParameterfv>(ref glGetColorTableParameterfvDelegate)(target, pname, parameters);
        }
        [Obsolete]
        public void GetColorTableParameter(uint target, uint pname, int[] parameters)
        {
            getDelegateFor<glGetColorTableParameteriv>(ref glGetColorTableParameterivDelegate)(target, pname, parameters);
        }
        [Obsolete]
        public void ColorSubTable(uint target, int start, int count, uint format, uint type, IntPtr data)
        {
            getDelegateFor<glColorSubTable>(ref glColorSubTableDelegate)(target, start, count, format, type, data);
        }
        [Obsolete]
        public void CopyColorSubTable(uint target, int start, int x, int y, int width)
        {
            getDelegateFor<glCopyColorSubTable>(ref glCopyColorSubTableDelegate)(target, start, x, y, width);
        }
        [Obsolete]
        public void ConvolutionFilter1D(uint target, uint internalformat, int width, uint format, uint type, IntPtr image)
        {
            getDelegateFor<glConvolutionFilter1D>(ref glConvolutionFilter1DDelegate)(target, internalformat, width, format, type, image);
        }
        [Obsolete]
        public void ConvolutionFilter2D(uint target, uint internalformat, int width, int height, uint format, uint type, IntPtr image)
        {
            getDelegateFor<glConvolutionFilter2D>(ref glConvolutionFilter2DDelegate)(target, internalformat, width, height, format, type, image);
        }
        [Obsolete]
        public void ConvolutionParameter(uint target, uint pname, float parameters)
        {
            getDelegateFor<glConvolutionParameterf>(ref glConvolutionParameterfDelegate)(target, pname, parameters);
        }
        [Obsolete]
        public void ConvolutionParameter(uint target, uint pname, float[] parameters)
        {
            getDelegateFor<glConvolutionParameterfv>(ref glConvolutionParameterfvDelegate)(target, pname, parameters);
        }
        [Obsolete]
        public void ConvolutionParameter(uint target, uint pname, int parameters)
        {
            getDelegateFor<glConvolutionParameteri>(ref glConvolutionParameteriDelegate)(target, pname, parameters);
        }
        [Obsolete]
        public void ConvolutionParameter(uint target, uint pname, int[] parameters)
        {
            getDelegateFor<glConvolutionParameteriv>(ref glConvolutionParameterivDelegate)(target, pname, parameters);
        }
        [Obsolete]
        public void CopyConvolutionFilter1D(uint target, uint internalformat, int x, int y, int width)
        {
            getDelegateFor<glCopyConvolutionFilter1D>(ref glCopyConvolutionFilter1DDelegate)(target, internalformat, x, y, width);
        }
        [Obsolete]
        public void CopyConvolutionFilter2D(uint target, uint internalformat, int x, int y, int width, int height)
        {
            getDelegateFor<glCopyConvolutionFilter2D>(ref glCopyConvolutionFilter2DDelegate)(target, internalformat, x, y, width, height);
        }
        [Obsolete]
        public void GetConvolutionFilter(uint target, uint format, uint type, IntPtr image)
        {
            getDelegateFor<glGetConvolutionFilter>(ref glGetConvolutionFilterDelegate)(target, format, type, image);
        }
        [Obsolete]
        public void GetConvolutionParameter(uint target, uint pname, float[] parameters)
        {
            getDelegateFor<glGetConvolutionParameterfv>(ref glGetConvolutionParameterfvDelegate)(target, pname, parameters);
        }
        [Obsolete]
        public void GetConvolutionParameter(uint target, uint pname, int[] parameters)
        {
            getDelegateFor<glGetConvolutionParameteriv>(ref glGetConvolutionParameterivDelegate)(target, pname, parameters);
        }
        [Obsolete]
        public void GetSeparableFilter(uint target, uint format, uint type, IntPtr row, IntPtr column, IntPtr span)
        {
            getDelegateFor<glGetSeparableFilter>(ref glGetSeparableFilterDelegate)(target, format, type, row, column, span);
        }
        [Obsolete]
        public void SeparableFilter2D(uint target, uint internalformat, int width, int height, uint format, uint type, IntPtr row, IntPtr column)
        {
            getDelegateFor<glSeparableFilter2D>(ref glSeparableFilter2DDelegate)(target, internalformat, width, height, format, type, row, column);
        }
        [Obsolete]
        public void GetHistogram(uint target, bool reset, uint format, uint type, IntPtr values)
        {
            getDelegateFor<glGetHistogram>(ref glGetHistogramDelegate)(target, reset, format, type, values);
        }
        [Obsolete]
        public void GetHistogramParameter(uint target, uint pname, float[] parameters)
        {
            getDelegateFor<glGetHistogramParameterfv>(ref glGetHistogramParameterfvDelegate)(target, pname, parameters);
        }
        [Obsolete]
        public void GetHistogramParameter(uint target, uint pname, int[] parameters)
        {
            getDelegateFor<glGetHistogramParameteriv>(ref glGetHistogramParameterivDelegate)(target, pname, parameters);
        }
        [Obsolete]
        public void GetMinmax(uint target, bool reset, uint format, uint type, IntPtr values)
        {
            getDelegateFor<glGetMinmax>(ref glGetMinmaxDelegate)(target, reset, format, type, values);
        }
        [Obsolete]
        public void GetMinmaxParameter(uint target, uint pname, float[] parameters)
        {
            getDelegateFor<glGetMinmaxParameterfv>(ref glGetMinmaxParameterfvDelegate)(target, pname, parameters);
        }
        [Obsolete]
        public void GetMinmaxParameter(uint target, uint pname, int[] parameters)
        {
            getDelegateFor<glGetMinmaxParameteriv>(ref glGetMinmaxParameterivDelegate)(target, pname, parameters);
        }
        [Obsolete]
        public void Histogram(uint target, int width, uint internalformat, bool sink)
        {
            getDelegateFor<glHistogram>(ref glHistogramDelegate)(target, width, internalformat, sink);
        }
        [Obsolete]
        public void Minmax(uint target, uint internalformat, bool sink)
        {
            getDelegateFor<glMinmax>(ref glMinmaxDelegate)(target, internalformat, sink);
        }
        [Obsolete]
        public void ResetHistogram(uint target)
        {
            getDelegateFor<glResetHistogram>(ref glResetHistogramDelegate)(target);
        }
        [Obsolete]
        public void ResetMinmax(uint target)
        {
            getDelegateFor<glResetMinmax>(ref glResetMinmaxDelegate)(target);
        }

        //  Delegates
        private delegate void glBlendColor(float red, float green, float blue, float alpha);
		private Delegate glBlendColorDelegate;
        private delegate void glBlendEquation(uint mode);
		private Delegate glBlendEquationDelegate;
        private delegate void glDrawRangeElements(uint mode, uint start, uint end, int count, uint type, IntPtr indices);
		private Delegate glDrawRangeElementsDelegate;
        private delegate void glTexImage3D(uint target, int level, int internalformat, int width, int height, int depth, int border, uint format, uint type, IntPtr pixels);
		private Delegate glTexImage3DDelegate;
        private delegate void glTexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, uint type, IntPtr pixels);
		private Delegate glTexSubImage3DDelegate;
        private delegate void glCopyTexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int x, int y, int width, int height);
		private Delegate glCopyTexSubImage3DDelegate;
        private delegate void glColorTable(uint target, uint internalformat, int width, uint format, uint type, IntPtr table);
		private Delegate glColorTableDelegate;
        private delegate void glColorTableParameterfv(uint target, uint pname, float[] parameters);
		private Delegate glColorTableParameterfvDelegate;
        private delegate void glColorTableParameteriv(uint target, uint pname, int[] parameters);
		private Delegate glColorTableParameterivDelegate;
        private delegate void glCopyColorTable(uint target, uint internalformat, int x, int y, int width);
		private Delegate glCopyColorTableDelegate;
        private delegate void glGetColorTable(uint target, uint format, uint type, IntPtr table);
		private Delegate glGetColorTableDelegate;
        private delegate void glGetColorTableParameterfv(uint target, uint pname, float[] parameters);
		private Delegate glGetColorTableParameterfvDelegate;
        private delegate void glGetColorTableParameteriv(uint target, uint pname, int[] parameters);
		private Delegate glGetColorTableParameterivDelegate;
        private delegate void glColorSubTable(uint target, int start, int count, uint format, uint type, IntPtr data);
		private Delegate glColorSubTableDelegate;
        private delegate void glCopyColorSubTable(uint target, int start, int x, int y, int width);
		private Delegate glCopyColorSubTableDelegate;
        private delegate void glConvolutionFilter1D(uint target, uint internalformat, int width, uint format, uint type, IntPtr image);
		private Delegate glConvolutionFilter1DDelegate;
        private delegate void glConvolutionFilter2D(uint target, uint internalformat, int width, int height, uint format, uint type, IntPtr image);
		private Delegate glConvolutionFilter2DDelegate;
        private delegate void glConvolutionParameterf(uint target, uint pname, float parameters);
		private Delegate glConvolutionParameterfDelegate;
        private delegate void glConvolutionParameterfv(uint target, uint pname, float[] parameters);
		private Delegate glConvolutionParameterfvDelegate;
        private delegate void glConvolutionParameteri(uint target, uint pname, int parameters);
		private Delegate glConvolutionParameteriDelegate;
        private delegate void glConvolutionParameteriv(uint target, uint pname, int[] parameters);
		private Delegate glConvolutionParameterivDelegate;
        private delegate void glCopyConvolutionFilter1D(uint target, uint internalformat, int x, int y, int width);
		private Delegate glCopyConvolutionFilter1DDelegate;
        private delegate void glCopyConvolutionFilter2D(uint target, uint internalformat, int x, int y, int width, int height);
		private Delegate glCopyConvolutionFilter2DDelegate;
        private delegate void glGetConvolutionFilter(uint target, uint format, uint type, IntPtr image);
		private Delegate glGetConvolutionFilterDelegate;
        private delegate void glGetConvolutionParameterfv(uint target, uint pname, float[] parameters);
		private Delegate glGetConvolutionParameterfvDelegate;
        private delegate void glGetConvolutionParameteriv(uint target, uint pname, int[] parameters);
		private Delegate glGetConvolutionParameterivDelegate;
        private delegate void glGetSeparableFilter(uint target, uint format, uint type, IntPtr row, IntPtr column, IntPtr span);
		private Delegate glGetSeparableFilterDelegate;
        private delegate void glSeparableFilter2D(uint target, uint internalformat, int width, int height, uint format, uint type, IntPtr row, IntPtr column);
		private Delegate glSeparableFilter2DDelegate;
        private delegate void glGetHistogram(uint target, bool reset, uint format, uint type, IntPtr values);
		private Delegate glGetHistogramDelegate;
        private delegate void glGetHistogramParameterfv(uint target, uint pname, float[] parameters);
		private Delegate glGetHistogramParameterfvDelegate;
        private delegate void glGetHistogramParameteriv(uint target, uint pname, int[] parameters);
		private Delegate glGetHistogramParameterivDelegate;
        private delegate void glGetMinmax(uint target, bool reset, uint format, uint type, IntPtr values);
		private Delegate glGetMinmaxDelegate;
        private delegate void glGetMinmaxParameterfv(uint target, uint pname, float[] parameters);
		private Delegate glGetMinmaxParameterfvDelegate;
        private delegate void glGetMinmaxParameteriv(uint target, uint pname, int[] parameters);
		private Delegate glGetMinmaxParameterivDelegate;
        private delegate void glHistogram(uint target, int width, uint internalformat, bool sink);
		private Delegate glHistogramDelegate;
        private delegate void glMinmax(uint target, uint internalformat, bool sink);
		private Delegate glMinmaxDelegate;
        private delegate void glResetHistogram(uint target);
		private Delegate glResetHistogramDelegate;
        private delegate void glResetMinmax(uint target);
		private Delegate glResetMinmaxDelegate;

        //  Constants
        public const uint GL_UNSIGNED_BYTE_3_3_2             = 0x8032;
        public const uint GL_UNSIGNED_SHORT_4_4_4_4          = 0x8033;
        public const uint GL_UNSIGNED_SHORT_5_5_5_1          = 0x8034;
        public const uint GL_UNSIGNED_INT_8_8_8_8            = 0x8035;
        public const uint GL_UNSIGNED_INT_10_10_10_2         = 0x8036;
        public const uint GL_TEXTURE_BINDING_3D              = 0x806A;
        public const uint GL_PACK_SKIP_IMAGES                = 0x806B;
        public const uint GL_PACK_IMAGE_HEIGHT               = 0x806C;
        public const uint GL_UNPACK_SKIP_IMAGES              = 0x806D;
        public const uint GL_UNPACK_IMAGE_HEIGHT             = 0x806E;
        public const uint GL_TEXTURE_3D                      = 0x806F;
        public const uint GL_PROXY_TEXTURE_3D                = 0x8070;
        public const uint GL_TEXTURE_DEPTH                   = 0x8071;
        public const uint GL_TEXTURE_WRAP_R                  = 0x8072;
        public const uint GL_MAX_3D_TEXTURE_SIZE             = 0x8073;
        public const uint GL_UNSIGNED_BYTE_2_3_3_REV         = 0x8362;
        public const uint GL_UNSIGNED_SHORT_5_6_5            = 0x8363;
        public const uint GL_UNSIGNED_SHORT_5_6_5_REV        = 0x8364;
        public const uint GL_UNSIGNED_SHORT_4_4_4_4_REV      = 0x8365;
        public const uint GL_UNSIGNED_SHORT_1_5_5_5_REV      = 0x8366;
        public const uint GL_UNSIGNED_INT_8_8_8_8_REV        = 0x8367;
        public const uint GL_UNSIGNED_INT_2_10_10_10_REV     = 0x8368;
        public const uint GL_BGR                             = 0x80E0;
        public const uint GL_BGRA                            = 0x80E1;
        public const uint GL_MAX_ELEMENTS_VERTICES           = 0x80E8;
        public const uint GL_MAX_ELEMENTS_INDICES            = 0x80E9;
        public const uint GL_CLAMP_TO_EDGE                   = 0x812F;
        public const uint GL_TEXTURE_MIN_LOD                 = 0x813A;
        public const uint GL_TEXTURE_MAX_LOD                 = 0x813B;
        public const uint GL_TEXTURE_BASE_LEVEL              = 0x813C;
        public const uint GL_TEXTURE_MAX_LEVEL               = 0x813D;
        public const uint GL_SMOOTH_POINT_SIZE_RANGE         = 0x0B12;
        public const uint GL_SMOOTH_POINT_SIZE_GRANULARITY   = 0x0B13;
        public const uint GL_SMOOTH_LINE_WIDTH_RANGE         = 0x0B22;
        public const uint GL_SMOOTH_LINE_WIDTH_GRANULARITY   = 0x0B23;
        public const uint GL_ALIASED_LINE_WIDTH_RANGE        = 0x846E;

        #endregion

        #region OpenGL 1.3

        //  Methods

        public void ActiveTexture(uint texture)
        {
            getDelegateFor<glActiveTexture>(ref glActiveTextureDelegate)(texture);
        }
        public void SampleCoverage(float value, bool invert)
        {
            getDelegateFor<glSampleCoverage>(ref glSampleCoverageDelegate)(value, invert);
        }
        public void CompressedTexImage3D(uint target, int level, uint internalformat, int width, int height, int depth, int border, int imageSize, IntPtr data)
        {
            getDelegateFor<glCompressedTexImage3D>(ref glCompressedTexImage3DDelegate)(target, level, internalformat, width, height, depth, border, imageSize, data);
        }
        public void CompressedTexImage2D(uint target, int level, uint internalformat, int width, int height, int border, int imageSize, IntPtr data)
        {
            getDelegateFor<glCompressedTexImage2D>(ref glCompressedTexImage2DDelegate)(target, level, internalformat, width, height, border, imageSize, data);
        }
        public void CompressedTexImage1D(uint target, int level, uint internalformat, int width, int border, int imageSize, IntPtr data)
        {
            getDelegateFor<glCompressedTexImage1D>(ref glCompressedTexImage1DDelegate)(target, level, internalformat, width, border, imageSize, data);
        }
        public void CompressedTexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, int imageSize, IntPtr data)
        {
            getDelegateFor<glCompressedTexSubImage3D>(ref glCompressedTexSubImage3DDelegate)(target, level, xoffset, yoffset, zoffset, width, height, depth, format, imageSize, data);
        }
        public void CompressedTexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, int imageSize, IntPtr data)
        {
            getDelegateFor<glCompressedTexSubImage2D>(ref glCompressedTexSubImage2DDelegate)(target, level, xoffset, yoffset, width, height, format, imageSize, data);
        }
        public void CompressedTexSubImage1D(uint target, int level, int xoffset, int width, uint format, int imageSize, IntPtr data)
        {
            getDelegateFor<glCompressedTexSubImage1D>(ref glCompressedTexSubImage1DDelegate)(target, level, xoffset, width, format, imageSize, data);
        }
        public void GetCompressedTexImage(uint target, int level, IntPtr img)
        {
            getDelegateFor<glGetCompressedTexImage>(ref glGetCompressedTexImageDelegate)(target, level, img);
        }

        //  Deprecated Methods
        [Obsolete]
        public void ClientActiveTexture(uint texture)
        {
            getDelegateFor<glClientActiveTexture>(ref glClientActiveTextureDelegate)(texture);
        }
        [Obsolete]
        public void MultiTexCoord1(uint target, double s)
        {
            getDelegateFor<glMultiTexCoord1d>(ref glMultiTexCoord1dDelegate)(target, s);
        }
        [Obsolete]
        public void MultiTexCoord1(uint target, double[] v)
        {
            getDelegateFor<glMultiTexCoord1dv>(ref glMultiTexCoord1dvDelegate)(target, v);
        }
        [Obsolete]
        public void MultiTexCoord1(uint target, float s)
        {
            getDelegateFor<glMultiTexCoord1f>(ref glMultiTexCoord1fDelegate)(target, s);
        }
        [Obsolete]
        public void MultiTexCoord1(uint target, float[] v)
        {
            getDelegateFor<glMultiTexCoord1fv>(ref glMultiTexCoord1fvDelegate)(target, v);
        }
        [Obsolete]
        public void MultiTexCoord1(uint target, int s)
        {
            getDelegateFor<glMultiTexCoord1i>(ref glMultiTexCoord1iDelegate)(target, s);
        }
        [Obsolete]
        public void MultiTexCoord1(uint target, int[] v)
        {
            getDelegateFor<glMultiTexCoord1iv>(ref glMultiTexCoord1ivDelegate)(target, v);
        }
        [Obsolete]
        public void MultiTexCoord1(uint target, short s)
        {
            getDelegateFor<glMultiTexCoord1s>(ref glMultiTexCoord1sDelegate)(target, s);
        }
        [Obsolete]
        public void MultiTexCoord1(uint target, short[] v)
        {
            getDelegateFor<glMultiTexCoord1sv>(ref glMultiTexCoord1svDelegate)(target, v);
        }
        [Obsolete]
        public void MultiTexCoord2(uint target, double s, double t)
        {
            getDelegateFor<glMultiTexCoord2d>(ref glMultiTexCoord2dDelegate)(target, s, t);
        }
        [Obsolete]
        public void MultiTexCoord2(uint target, double[] v)
        {
            getDelegateFor<glMultiTexCoord2dv>(ref glMultiTexCoord2dvDelegate)(target, v);
        }
        [Obsolete]
        public void MultiTexCoord2(uint target, float s, float t)
        {
            getDelegateFor<glMultiTexCoord2f>(ref glMultiTexCoord2fDelegate)(target, s, t);
        }
        [Obsolete]
        public void MultiTexCoord2(uint target, float[] v)
        {
            getDelegateFor<glMultiTexCoord2fv>(ref glMultiTexCoord2fvDelegate)(target, v);
        }
        [Obsolete]
        public void MultiTexCoord2(uint target, int s, int t)
        {
            getDelegateFor<glMultiTexCoord2i>(ref glMultiTexCoord2iDelegate)(target, s, t);
        }
        [Obsolete]
        public void MultiTexCoord2(uint target, int[] v)
        {
            getDelegateFor<glMultiTexCoord2iv>(ref glMultiTexCoord2ivDelegate)(target, v);
        }
        [Obsolete]
        public void MultiTexCoord2(uint target, short s, short t)
        {
            getDelegateFor<glMultiTexCoord2s>(ref glMultiTexCoord2sDelegate)(target, s, t);
        }
        [Obsolete]
        public void MultiTexCoord2(uint target, short[] v)
        {
            getDelegateFor<glMultiTexCoord2sv>(ref glMultiTexCoord2svDelegate)(target, v);
        }
        [Obsolete]
        public void MultiTexCoord3(uint target, double s, double t, double r)
        {
            getDelegateFor<glMultiTexCoord3d>(ref glMultiTexCoord3dDelegate)(target, s, t, r);
        }
        [Obsolete]
        public void MultiTexCoord3(uint target, double[] v)
        {
            getDelegateFor<glMultiTexCoord3dv>(ref glMultiTexCoord3dvDelegate)(target, v);
        }
        [Obsolete]
        public void MultiTexCoord3(uint target, float s, float t, float r)
        {
            getDelegateFor<glMultiTexCoord3f>(ref glMultiTexCoord3fDelegate)(target, s, t, r);
        }
        [Obsolete]
        public void MultiTexCoord3(uint target, float[] v)
        {
            getDelegateFor<glMultiTexCoord3fv>(ref glMultiTexCoord3fvDelegate)(target, v);
        }
        [Obsolete]
        public void MultiTexCoord3(uint target, int s, int t, int r)
        {
            getDelegateFor<glMultiTexCoord3i>(ref glMultiTexCoord3iDelegate)(target, s, t, r);
        }
        [Obsolete]
        public void MultiTexCoord3(uint target, int[] v)
        {
            getDelegateFor<glMultiTexCoord3iv>(ref glMultiTexCoord3ivDelegate)(target, v);
        }
        [Obsolete]
        public void MultiTexCoord3(uint target, short s, short t, short r)
        {
            getDelegateFor<glMultiTexCoord3s>(ref glMultiTexCoord3sDelegate)(target, s, t, r);
        }
        [Obsolete]
        public void MultiTexCoord3(uint target, short[] v)
        {
            getDelegateFor<glMultiTexCoord3sv>(ref glMultiTexCoord3svDelegate)(target, v);
        }
        [Obsolete]
        public void MultiTexCoord4(uint target, double s, double t, double r, double q)
        {
            getDelegateFor<glMultiTexCoord4d>(ref glMultiTexCoord4dDelegate)(target, s, t, r, q);
        }
        [Obsolete]
        public void MultiTexCoord4(uint target, double[] v)
        {
            getDelegateFor<glMultiTexCoord4dv>(ref glMultiTexCoord4dvDelegate)(target, v);
        }
        [Obsolete]
        public void MultiTexCoord4(uint target, float s, float t, float r, float q)
        {
            getDelegateFor<glMultiTexCoord4f>(ref glMultiTexCoord4fDelegate)(target, s, t, r, q);
        }
        [Obsolete]
        public void MultiTexCoord4(uint target, float[] v)
        {
            getDelegateFor<glMultiTexCoord4fv>(ref glMultiTexCoord4fvDelegate)(target, v);
        }
        [Obsolete]
        public void MultiTexCoord4(uint target, int s, int t, int r, int q)
        {
            getDelegateFor<glMultiTexCoord4i>(ref glMultiTexCoord4iDelegate)(target, s, t, r, q);
        }
        [Obsolete]
        public void MultiTexCoord4(uint target, int[] v)
        {
            getDelegateFor<glMultiTexCoord4iv>(ref glMultiTexCoord4ivDelegate)(target, v);
        }
        [Obsolete]
        public void MultiTexCoord4(uint target, short s, short t, short r, short q)
        {
            getDelegateFor<glMultiTexCoord4s>(ref glMultiTexCoord4sDelegate)(target, s, t, r, q);
        }
        [Obsolete]
        public void MultiTexCoord4(uint target, short[] v)
        {
            getDelegateFor<glMultiTexCoord4sv>(ref glMultiTexCoord4svDelegate)(target, v);
        }
        [Obsolete]
        public void LoadTransposeMatrix(float[] m)
        {
            getDelegateFor<glLoadTransposeMatrixf>(ref glLoadTransposeMatrixfDelegate)(m);
        }
        [Obsolete]
        public void LoadTransposeMatrix(double[] m)
        {
            getDelegateFor<glLoadTransposeMatrixd>(ref glLoadTransposeMatrixdDelegate)(m);
        }
        [Obsolete]
        public void MultTransposeMatrix(float[] m)
        {
            getDelegateFor<glMultTransposeMatrixf>(ref glMultTransposeMatrixfDelegate)(m);
        }
        [Obsolete]
        public void MultTransposeMatrix(double[] m)
        {
            getDelegateFor<glMultTransposeMatrixd>(ref glMultTransposeMatrixdDelegate)(m);
        }

        //  Delegates
        private delegate void glActiveTexture(uint texture);
		private Delegate glActiveTextureDelegate;
        private delegate void glSampleCoverage(float value, bool invert);
		private Delegate glSampleCoverageDelegate;
        private delegate void glCompressedTexImage3D(uint target, int level, uint internalformat, int width, int height, int depth, int border, int imageSize, IntPtr data);
		private Delegate glCompressedTexImage3DDelegate;
        private delegate void glCompressedTexImage2D(uint target, int level, uint internalformat, int width, int height, int border, int imageSize, IntPtr data);
		private Delegate glCompressedTexImage2DDelegate;
        private delegate void glCompressedTexImage1D(uint target, int level, uint internalformat, int width, int border, int imageSize, IntPtr data);
		private Delegate glCompressedTexImage1DDelegate;
        private delegate void glCompressedTexSubImage3D(uint target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, int imageSize, IntPtr data);
		private Delegate glCompressedTexSubImage3DDelegate;
        private delegate void glCompressedTexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, int imageSize, IntPtr data);
		private Delegate glCompressedTexSubImage2DDelegate;
        private delegate void glCompressedTexSubImage1D(uint target, int level, int xoffset, int width, uint format, int imageSize, IntPtr data);
		private Delegate glCompressedTexSubImage1DDelegate;
        private delegate void glGetCompressedTexImage(uint target, int level, IntPtr img);
		private Delegate glGetCompressedTexImageDelegate;

        private delegate void glClientActiveTexture(uint texture);
		private Delegate glClientActiveTextureDelegate;
        private delegate void glMultiTexCoord1d(uint target, double s);
		private Delegate glMultiTexCoord1dDelegate;
        private delegate void glMultiTexCoord1dv(uint target, double[] v);
		private Delegate glMultiTexCoord1dvDelegate;
        private delegate void glMultiTexCoord1f(uint target, float s);
		private Delegate glMultiTexCoord1fDelegate;
        private delegate void glMultiTexCoord1fv(uint target, float[] v);
		private Delegate glMultiTexCoord1fvDelegate;
        private delegate void glMultiTexCoord1i(uint target, int s);
		private Delegate glMultiTexCoord1iDelegate;
        private delegate void glMultiTexCoord1iv(uint target, int[] v);
		private Delegate glMultiTexCoord1ivDelegate;
        private delegate void glMultiTexCoord1s(uint target, short s);
		private Delegate glMultiTexCoord1sDelegate;
        private delegate void glMultiTexCoord1sv(uint target, short[] v);
		private Delegate glMultiTexCoord1svDelegate;
        private delegate void glMultiTexCoord2d(uint target, double s, double t);
		private Delegate glMultiTexCoord2dDelegate;
        private delegate void glMultiTexCoord2dv(uint target, double[] v);
		private Delegate glMultiTexCoord2dvDelegate;
        private delegate void glMultiTexCoord2f(uint target, float s, float t);
		private Delegate glMultiTexCoord2fDelegate;
        private delegate void glMultiTexCoord2fv(uint target, float[] v);
		private Delegate glMultiTexCoord2fvDelegate;
        private delegate void glMultiTexCoord2i(uint target, int s, int t);
		private Delegate glMultiTexCoord2iDelegate;
        private delegate void glMultiTexCoord2iv(uint target, int[] v);
		private Delegate glMultiTexCoord2ivDelegate;
        private delegate void glMultiTexCoord2s(uint target, short s, short t);
		private Delegate glMultiTexCoord2sDelegate;
        private delegate void glMultiTexCoord2sv(uint target, short[] v);
		private Delegate glMultiTexCoord2svDelegate;
        private delegate void glMultiTexCoord3d(uint target, double s, double t, double r);
		private Delegate glMultiTexCoord3dDelegate;
        private delegate void glMultiTexCoord3dv(uint target, double[] v);
		private Delegate glMultiTexCoord3dvDelegate;
        private delegate void glMultiTexCoord3f(uint target, float s, float t, float r);
		private Delegate glMultiTexCoord3fDelegate;
        private delegate void glMultiTexCoord3fv(uint target, float[] v);
		private Delegate glMultiTexCoord3fvDelegate;
        private delegate void glMultiTexCoord3i(uint target, int s, int t, int r);
		private Delegate glMultiTexCoord3iDelegate;
        private delegate void glMultiTexCoord3iv(uint target, int[] v);
		private Delegate glMultiTexCoord3ivDelegate;
        private delegate void glMultiTexCoord3s(uint target, short s, short t, short r);
		private Delegate glMultiTexCoord3sDelegate;
        private delegate void glMultiTexCoord3sv(uint target, short[] v);
		private Delegate glMultiTexCoord3svDelegate;
        private delegate void glMultiTexCoord4d(uint target, double s, double t, double r, double q);
		private Delegate glMultiTexCoord4dDelegate;
        private delegate void glMultiTexCoord4dv(uint target, double[] v);
		private Delegate glMultiTexCoord4dvDelegate;
        private delegate void glMultiTexCoord4f(uint target, float s, float t, float r, float q);
		private Delegate glMultiTexCoord4fDelegate;
        private delegate void glMultiTexCoord4fv(uint target, float[] v);
		private Delegate glMultiTexCoord4fvDelegate;
        private delegate void glMultiTexCoord4i(uint target, int s, int t, int r, int q);
		private Delegate glMultiTexCoord4iDelegate;
        private delegate void glMultiTexCoord4iv(uint target, int[] v);
		private Delegate glMultiTexCoord4ivDelegate;
        private delegate void glMultiTexCoord4s(uint target, short s, short t, short r, short q);
		private Delegate glMultiTexCoord4sDelegate;
        private delegate void glMultiTexCoord4sv(uint target, short[] v);
		private Delegate glMultiTexCoord4svDelegate;
        private delegate void glLoadTransposeMatrixf(float[] m);
		private Delegate glLoadTransposeMatrixfDelegate;
        private delegate void glLoadTransposeMatrixd(double[] m);
		private Delegate glLoadTransposeMatrixdDelegate;
        private delegate void glMultTransposeMatrixf(float[] m);
		private Delegate glMultTransposeMatrixfDelegate;
        private delegate void glMultTransposeMatrixd(double[] m);
		private Delegate glMultTransposeMatrixdDelegate;

        //  Constants
        public const uint GL_TEXTURE0                        = 0x84C0;
        public const uint GL_TEXTURE1                        = 0x84C1;
        public const uint GL_TEXTURE2                        = 0x84C2;
        public const uint GL_TEXTURE3                        = 0x84C3;
        public const uint GL_TEXTURE4                        = 0x84C4;
        public const uint GL_TEXTURE5                        = 0x84C5;
        public const uint GL_TEXTURE6                        = 0x84C6;
        public const uint GL_TEXTURE7                        = 0x84C7;
        public const uint GL_TEXTURE8                        = 0x84C8;
        public const uint GL_TEXTURE9                        = 0x84C9;
        public const uint GL_TEXTURE10                       = 0x84CA;
        public const uint GL_TEXTURE11                       = 0x84CB;
        public const uint GL_TEXTURE12                       = 0x84CC;
        public const uint GL_TEXTURE13                       = 0x84CD;
        public const uint GL_TEXTURE14                       = 0x84CE;
        public const uint GL_TEXTURE15                       = 0x84CF;
        public const uint GL_TEXTURE16                       = 0x84D0;
        public const uint GL_TEXTURE17                       = 0x84D1;
        public const uint GL_TEXTURE18                       = 0x84D2;
        public const uint GL_TEXTURE19                       = 0x84D3;
        public const uint GL_TEXTURE20                       = 0x84D4;
        public const uint GL_TEXTURE21                       = 0x84D5;
        public const uint GL_TEXTURE22                       = 0x84D6;
        public const uint GL_TEXTURE23                       = 0x84D7;
        public const uint GL_TEXTURE24                       = 0x84D8;
        public const uint GL_TEXTURE25                       = 0x84D9;
        public const uint GL_TEXTURE26                       = 0x84DA;
        public const uint GL_TEXTURE27                       = 0x84DB;
        public const uint GL_TEXTURE28                       = 0x84DC;
        public const uint GL_TEXTURE29                       = 0x84DD;
        public const uint GL_TEXTURE30                       = 0x84DE;
        public const uint GL_TEXTURE31                       = 0x84DF;
        public const uint GL_ACTIVE_TEXTURE                  = 0x84E0;
        public const uint GL_MULTISAMPLE                     = 0x809D;
        public const uint GL_SAMPLE_ALPHA_TO_COVERAGE        = 0x809E;
        public const uint GL_SAMPLE_ALPHA_TO_ONE             = 0x809F;
        public const uint GL_SAMPLE_COVERAGE                 = 0x80A0;
        public const uint GL_SAMPLE_BUFFERS                  = 0x80A8;
        public const uint GL_SAMPLES                         = 0x80A9;
        public const uint GL_SAMPLE_COVERAGE_VALUE           = 0x80AA;
        public const uint GL_SAMPLE_COVERAGE_INVERT          = 0x80AB;
        public const uint GL_TEXTURE_CUBE_MAP                = 0x8513;
        public const uint GL_TEXTURE_BINDING_CUBE_MAP        = 0x8514;
        public const uint GL_TEXTURE_CUBE_MAP_POSITIVE_X     = 0x8515;
        public const uint GL_TEXTURE_CUBE_MAP_NEGATIVE_X     = 0x8516;
        public const uint GL_TEXTURE_CUBE_MAP_POSITIVE_Y     = 0x8517;
        public const uint GL_TEXTURE_CUBE_MAP_NEGATIVE_Y     = 0x8518;
        public const uint GL_TEXTURE_CUBE_MAP_POSITIVE_Z     = 0x8519;
        public const uint GL_TEXTURE_CUBE_MAP_NEGATIVE_Z     = 0x851A;
        public const uint GL_PROXY_TEXTURE_CUBE_MAP          = 0x851B;
        public const uint GL_MAX_CUBE_MAP_TEXTURE_SIZE       = 0x851C;
        public const uint GL_COMPRESSED_RGB                  = 0x84ED;
        public const uint GL_COMPRESSED_RGBA                 = 0x84EE;
        public const uint GL_TEXTURE_COMPRESSION_HINT        = 0x84EF;
        public const uint GL_TEXTURE_COMPRESSED_IMAGE_SIZE   = 0x86A0;
        public const uint GL_TEXTURE_COMPRESSED              = 0x86A1;
        public const uint GL_NUM_COMPRESSED_TEXTURE_FORMATS  = 0x86A2;
        public const uint GL_COMPRESSED_TEXTURE_FORMATS      = 0x86A3;
        public const uint GL_CLAMP_TO_BORDER                 = 0x812D;

        #endregion
        
        #region OpenGL 1.4

        //  Methods
        public void BlendFuncSeparate(uint sfactorRGB, uint dfactorRGB, uint sfactorAlpha, uint dfactorAlpha)
        {
            getDelegateFor<glBlendFuncSeparate>(ref glBlendFuncSeparateDelegate)(sfactorRGB, dfactorRGB, sfactorAlpha, dfactorAlpha);
        }
        public void MultiDrawArrays(uint mode, int[] first, int[] count, int primcount)
        {
            getDelegateFor<glMultiDrawArrays>(ref glMultiDrawArraysDelegate)(mode, first, count, primcount);
        }
        public void MultiDrawElements(uint mode, int[] count, uint type, IntPtr indices, int primcount)
        {
            getDelegateFor<glMultiDrawElements>(ref glMultiDrawElementsDelegate)(mode, count, type, indices, primcount);
        }
        public void PointParameter(uint pname, float parameter)
        {
            getDelegateFor<glPointParameterf>(ref glPointParameterfDelegate)(pname, parameter);
        }
        public void PointParameter(uint pname, float[] parameters)
        {
            getDelegateFor<glPointParameterfv>(ref glPointParameterfvDelegate)(pname, parameters);
        }
        public void PointParameter(uint pname, int parameter)
        {
            getDelegateFor<glPointParameteri>(ref glPointParameteriDelegate)(pname, parameter);
        }
        public void PointParameter(uint pname, int[] parameters)
        {
            getDelegateFor<glPointParameteriv>(ref glPointParameterivDelegate)(pname, parameters);
        }
        
        //  Deprecated Methods
        [Obsolete]
        public void FogCoord(float coord)
        {
            getDelegateFor<glFogCoordf>(ref glFogCoordfDelegate)(coord);
        }
        [Obsolete]
        public void FogCoord(float[] coord)
        {
            getDelegateFor<glFogCoordfv>(ref glFogCoordfvDelegate)(coord);
        }
        [Obsolete]
        public void FogCoord(double coord)
        {
            getDelegateFor<glFogCoordd>(ref glFogCoorddDelegate)(coord);
        }
        [Obsolete]
        public void FogCoord(double[] coord)
        {
            getDelegateFor<glFogCoorddv>(ref glFogCoorddvDelegate)(coord);
        }
        [Obsolete]
        public void FogCoordPointer(uint type, int stride, IntPtr pointer)
        {
            getDelegateFor<glFogCoordPointer>(ref glFogCoordPointerDelegate)(type, stride, pointer);
        }
        [Obsolete]
        public void SecondaryColor3(sbyte red, sbyte green, sbyte blue)
        {
            getDelegateFor<glSecondaryColor3b>(ref glSecondaryColor3bDelegate)(red, green, blue);
        }
        [Obsolete]
        public void SecondaryColor3(sbyte[] v)
        {
            getDelegateFor<glSecondaryColor3bv>(ref glSecondaryColor3bvDelegate)(v);
        }
        [Obsolete]
        public void SecondaryColor3(double red, double green, double blue)
        {
            getDelegateFor<glSecondaryColor3d>(ref glSecondaryColor3dDelegate)(red, green, blue);
        }
        [Obsolete]
        public void SecondaryColor3(double[] v)
        {
            getDelegateFor<glSecondaryColor3dv>(ref glSecondaryColor3dvDelegate)(v);
        }
        [Obsolete]
        public void SecondaryColor3(float red, float green, float blue)
        {
            getDelegateFor<glSecondaryColor3f>(ref glSecondaryColor3fDelegate)(red, green, blue);
        }
        [Obsolete]
        public void SecondaryColor3(float[] v)
        {
            getDelegateFor<glSecondaryColor3fv>(ref glSecondaryColor3fvDelegate)(v);
        }
        [Obsolete]
        public void SecondaryColor3(int red, int green, int blue)
        {
            getDelegateFor<glSecondaryColor3i>(ref glSecondaryColor3iDelegate)(red, green, blue);
        }
        [Obsolete]
        public void SecondaryColor3(int[] v)
        {
            getDelegateFor<glSecondaryColor3iv>(ref glSecondaryColor3ivDelegate)(v);
        }
        [Obsolete]
        public void SecondaryColor3(short red, short green, short blue)
        {
            getDelegateFor<glSecondaryColor3s>(ref glSecondaryColor3sDelegate)(red, green, blue);
        }
        [Obsolete]
        public void SecondaryColor3(short[] v)
        {
            getDelegateFor<glSecondaryColor3sv>(ref glSecondaryColor3svDelegate)(v);
        }
        [Obsolete]
        public void SecondaryColor3(byte red, byte green, byte blue)
        {
            getDelegateFor<glSecondaryColor3ub>(ref glSecondaryColor3ubDelegate)(red, green, blue);
        }
        [Obsolete]
        public void SecondaryColor3(byte[] v)
        {
            getDelegateFor<glSecondaryColor3ubv>(ref glSecondaryColor3ubvDelegate)(v);
        }
        [Obsolete]
        public void SecondaryColor3(uint red, uint green, uint blue)
        {
            getDelegateFor<glSecondaryColor3ui>(ref glSecondaryColor3uiDelegate)(red, green, blue);
        }
        [Obsolete]
        public void SecondaryColor3(uint[] v)
        {
            getDelegateFor<glSecondaryColor3uiv>(ref glSecondaryColor3uivDelegate)(v);
        }
        [Obsolete]
        public void SecondaryColor3(ushort red, ushort green, ushort blue)
        {
            getDelegateFor<glSecondaryColor3us>(ref glSecondaryColor3usDelegate)(red, green, blue);
        }
        [Obsolete]
        public void SecondaryColor3(ushort[] v)
        {
            getDelegateFor<glSecondaryColor3usv>(ref glSecondaryColor3usvDelegate)(v);
        }
        [Obsolete]
        public void SecondaryColorPointer(int size, uint type, int stride, IntPtr pointer)
        {
            getDelegateFor<glSecondaryColorPointer>(ref glSecondaryColorPointerDelegate)(size, type, stride, pointer);
        }
        [Obsolete]
        public void WindowPos2(double x, double y)
        {
            getDelegateFor<glWindowPos2d>(ref glWindowPos2dDelegate)(x, y);
        }
        [Obsolete]
        public void WindowPos2(double[] v)
        {
            getDelegateFor<glWindowPos2dv>(ref glWindowPos2dvDelegate)(v);
        }
        [Obsolete]
        public void WindowPos2(float x, float y)
        {
            getDelegateFor<glWindowPos2f>(ref glWindowPos2fDelegate)(x, y);
        }
        [Obsolete]
        public void WindowPos2(float[] v)
        {
            getDelegateFor<glWindowPos2fv>(ref glWindowPos2fvDelegate)(v);
        }
        [Obsolete]
        public void WindowPos2(int x, int y)
        {
            getDelegateFor<glWindowPos2i>(ref glWindowPos2iDelegate)(x, y);
        }
        [Obsolete]
        public void WindowPos2(int[] v)
        {
            getDelegateFor<glWindowPos2iv>(ref glWindowPos2ivDelegate)(v);
        }
        [Obsolete]
        public void WindowPos2(short x, short y)
        {
            getDelegateFor<glWindowPos2s>(ref glWindowPos2sDelegate)(x, y);
        }
        [Obsolete]
        public void WindowPos2(short[] v)
        {
            getDelegateFor<glWindowPos2sv>(ref glWindowPos2svDelegate)(v);
        }
        [Obsolete]
        public void WindowPos3(double x, double y, double z)
        {
            getDelegateFor<glWindowPos3d>(ref glWindowPos3dDelegate)(x, y, z);
        }
        [Obsolete]
        public void WindowPos3(double[] v)
        {
            getDelegateFor<glWindowPos3dv>(ref glWindowPos3dvDelegate)(v);
        }
        [Obsolete]
        public void WindowPos3(float x, float y, float z)
        {
            getDelegateFor<glWindowPos3f>(ref glWindowPos3fDelegate)(x, y, z);
        }
        [Obsolete]
        public void WindowPos3(float[] v)
        {
            getDelegateFor<glWindowPos3fv>(ref glWindowPos3fvDelegate)(v);
        }
        [Obsolete]
        public void WindowPos3(int x, int y, int z)
        {
            getDelegateFor<glWindowPos3i>(ref glWindowPos3iDelegate)(x, y, z);
        }
        [Obsolete]
        public void WindowPos3(int[] v)
        {
            getDelegateFor<glWindowPos3iv>(ref glWindowPos3ivDelegate)(v);
        }
        [Obsolete]
        public void WindowPos3(short x, short y, short z)
        {
            getDelegateFor<glWindowPos3s>(ref glWindowPos3sDelegate)(x, y, z);
        }
        [Obsolete]
        public void WindowPos3(short[] v)
        {
            getDelegateFor<glWindowPos3sv>(ref glWindowPos3svDelegate)(v);
        }

        //  Delegates
        private delegate void glBlendFuncSeparate(uint sfactorRGB, uint dfactorRGB, uint sfactorAlpha, uint dfactorAlpha);
		private Delegate glBlendFuncSeparateDelegate;
        private delegate void glMultiDrawArrays(uint mode, int[] first, int[] count, int primcount);
		private Delegate glMultiDrawArraysDelegate;
        private delegate void glMultiDrawElements(uint mode, int[] count, uint type, IntPtr indices, int primcount);
		private Delegate glMultiDrawElementsDelegate;
        private delegate void glPointParameterf(uint pname, float parameter);
		private Delegate glPointParameterfDelegate;
        private delegate void glPointParameterfv(uint pname, float[] parameters);
		private Delegate glPointParameterfvDelegate;
        private delegate void glPointParameteri(uint pname, int parameter);
		private Delegate glPointParameteriDelegate;
        private delegate void glPointParameteriv(uint pname, int[] parameters);
		private Delegate glPointParameterivDelegate;
        private delegate void glFogCoordf(float coord);
		private Delegate glFogCoordfDelegate;
        private delegate void glFogCoordfv(float[] coord);
		private Delegate glFogCoordfvDelegate;
        private delegate void glFogCoordd(double coord);
		private Delegate glFogCoorddDelegate;
        private delegate void glFogCoorddv(double[] coord);
		private Delegate glFogCoorddvDelegate;
        private delegate void glFogCoordPointer(uint type, int stride, IntPtr pointer);
		private Delegate glFogCoordPointerDelegate;
        private delegate void glSecondaryColor3b(sbyte red, sbyte green, sbyte blue);
		private Delegate glSecondaryColor3bDelegate;
        private delegate void glSecondaryColor3bv(sbyte[] v);
		private Delegate glSecondaryColor3bvDelegate;
        private delegate void glSecondaryColor3d(double red, double green, double blue);
		private Delegate glSecondaryColor3dDelegate;
        private delegate void glSecondaryColor3dv(double[] v);
		private Delegate glSecondaryColor3dvDelegate;
        private delegate void glSecondaryColor3f(float red, float green, float blue);
		private Delegate glSecondaryColor3fDelegate;
        private delegate void glSecondaryColor3fv(float[] v);
		private Delegate glSecondaryColor3fvDelegate;
        private delegate void glSecondaryColor3i(int red, int green, int blue);
		private Delegate glSecondaryColor3iDelegate;
        private delegate void glSecondaryColor3iv(int[] v);
		private Delegate glSecondaryColor3ivDelegate;
        private delegate void glSecondaryColor3s(short red, short green, short blue);
		private Delegate glSecondaryColor3sDelegate;
        private delegate void glSecondaryColor3sv(short[] v);
		private Delegate glSecondaryColor3svDelegate;
        private delegate void glSecondaryColor3ub(byte red, byte green, byte blue);
		private Delegate glSecondaryColor3ubDelegate;
        private delegate void glSecondaryColor3ubv(byte[] v);
		private Delegate glSecondaryColor3ubvDelegate;
        private delegate void glSecondaryColor3ui(uint red, uint green, uint blue);
		private Delegate glSecondaryColor3uiDelegate;
        private delegate void glSecondaryColor3uiv(uint[] v);
		private Delegate glSecondaryColor3uivDelegate;
        private delegate void glSecondaryColor3us(ushort red, ushort green, ushort blue);
		private Delegate glSecondaryColor3usDelegate;
        private delegate void glSecondaryColor3usv(ushort[] v);
		private Delegate glSecondaryColor3usvDelegate;
        private delegate void glSecondaryColorPointer(int size, uint type, int stride, IntPtr pointer);
		private Delegate glSecondaryColorPointerDelegate;
        private delegate void glWindowPos2d(double x, double y);
		private Delegate glWindowPos2dDelegate;
        private delegate void glWindowPos2dv(double[] v);
		private Delegate glWindowPos2dvDelegate;
        private delegate void glWindowPos2f(float x, float y);
		private Delegate glWindowPos2fDelegate;
        private delegate void glWindowPos2fv(float[] v);
		private Delegate glWindowPos2fvDelegate;
        private delegate void glWindowPos2i(int x, int y);
		private Delegate glWindowPos2iDelegate;
        private delegate void glWindowPos2iv(int[] v);
		private Delegate glWindowPos2ivDelegate;
        private delegate void glWindowPos2s(short x, short y);
		private Delegate glWindowPos2sDelegate;
        private delegate void glWindowPos2sv(short[] v);
		private Delegate glWindowPos2svDelegate;
        private delegate void glWindowPos3d(double x, double y, double z);
		private Delegate glWindowPos3dDelegate;
        private delegate void glWindowPos3dv(double[] v);
		private Delegate glWindowPos3dvDelegate;
        private delegate void glWindowPos3f(float x, float y, float z);
		private Delegate glWindowPos3fDelegate;
        private delegate void glWindowPos3fv(float[] v);
		private Delegate glWindowPos3fvDelegate;
        private delegate void glWindowPos3i(int x, int y, int z);
		private Delegate glWindowPos3iDelegate;
        private delegate void glWindowPos3iv(int[] v);
		private Delegate glWindowPos3ivDelegate;
        private delegate void glWindowPos3s(short x, short y, short z);
		private Delegate glWindowPos3sDelegate;
        private delegate void glWindowPos3sv(short[] v);
		private Delegate glWindowPos3svDelegate;

        //  Constants
        public const uint GL_BLEND_DST_RGB                   = 0x80C8;
        public const uint GL_BLEND_SRC_RGB                   = 0x80C9;
        public const uint GL_BLEND_DST_ALPHA                 = 0x80CA;
        public const uint GL_BLEND_SRC_ALPHA                 = 0x80CB;
        public const uint GL_POINT_FADE_THRESHOLD_SIZE       = 0x8128;
        public const uint GL_DEPTH_COMPONENT16               = 0x81A5;
        public const uint GL_DEPTH_COMPONENT24               = 0x81A6;
        public const uint GL_DEPTH_COMPONENT32               = 0x81A7;
        public const uint GL_MIRRORED_REPEAT                 = 0x8370;
        public const uint GL_MAX_TEXTURE_LOD_BIAS            = 0x84FD;
        public const uint GL_TEXTURE_LOD_BIAS                = 0x8501;
        public const uint GL_INCR_WRAP                       = 0x8507;
        public const uint GL_DECR_WRAP                       = 0x8508;
        public const uint GL_TEXTURE_DEPTH_SIZE              = 0x884A;
        public const uint GL_TEXTURE_COMPARE_MODE            = 0x884C;
        public const uint GL_TEXTURE_COMPARE_FUNC            = 0x884D;

        #endregion
        
        #region OpenGL 1.5

        //  Methods
        public void GenQueries(int n, uint[] ids)
        {
            getDelegateFor<glGenQueries>(ref glGenQueriesDelegate)(n, ids);
        }
        public void DeleteQueries(int n, uint[] ids)
        {
            getDelegateFor<glDeleteQueries>(ref glDeleteQueriesDelegate)(n, ids);
        }
        public bool IsQuery(uint id)
        {
            return(bool)getDelegateFor<glIsQuery>(ref glIsQueryDelegate)(id);
        }
        public void BeginQuery(uint target, uint id)
        {
            getDelegateFor<glBeginQuery>(ref glBeginQueryDelegate)(target, id);
        }
        public void EndQuery(uint target)
        {
            getDelegateFor<glEndQuery>(ref glEndQueryDelegate)(target);
        }
        public void GetQuery(uint target, uint pname, int[] parameters)
        {
            getDelegateFor<glGetQueryiv>(ref glGetQueryivDelegate)(target, pname, parameters);
        }
        public void GetQueryObject(uint id, uint pname, int[] parameters)
        {
            getDelegateFor<glGetQueryObjectiv>(ref glGetQueryObjectivDelegate)(id, pname, parameters);
        }
        public void GetQueryObject(uint id, uint pname, uint[] parameters)
        {
            getDelegateFor<glGetQueryObjectuiv>(ref glGetQueryObjectuivDelegate)(id, pname, parameters);
        }
        public void BindBuffer(uint target, uint buffer)
        {
            getDelegateFor<glBindBuffer>(ref glBindBufferDelegate)(target, buffer);
        }
        public void DeleteBuffers(int n, uint[] buffers)
        {
            getDelegateFor<glDeleteBuffers>(ref glDeleteBuffersDelegate)(n, buffers);
        }
        public void GenBuffers(int n, uint[] buffers)
        {
            getDelegateFor<glGenBuffers>(ref glGenBuffersDelegate)(n, buffers);
        }
        public bool IsBuffer(uint buffer)
        {
            return(bool)getDelegateFor<glIsBuffer>(ref glIsBufferDelegate)(buffer);
        }
        public void BufferData(uint target, int size, IntPtr data, uint usage)
        {
            getDelegateFor<glBufferData>(ref glBufferDataDelegate)(target, size, data, usage);
        }
        public void BufferData(uint target, float[] data, uint usage)
        {
            IntPtr p = Marshal.AllocHGlobal(data.Length * sizeof(float));
            Marshal.Copy(data, 0, p, data.Length);
            getDelegateFor<glBufferData>(ref glBufferDataDelegate)(target, data.Length * sizeof(float), p, usage);
            Marshal.FreeHGlobal(p);
        }
        public void BufferData(uint target, ushort[] data, uint usage)
        {
            var dataSize = data.Length * sizeof(ushort);
            IntPtr p = Marshal.AllocHGlobal(dataSize);
            var shortData = new short[data.Length];
            Buffer.BlockCopy(data, 0, shortData, 0, dataSize);
            Marshal.Copy(shortData, 0, p, data.Length);
            getDelegateFor<glBufferData>(ref glBufferDataDelegate)(target, dataSize, p, usage);
            Marshal.FreeHGlobal(p);
        }
        public void BufferSubData(uint target, int offset, int size, IntPtr data)
        {
            getDelegateFor<glBufferSubData>(ref glBufferSubDataDelegate)(target, offset, size, data);
        }
        public void GetBufferSubData(uint target, int offset, int size, IntPtr data)
        {
            getDelegateFor<glGetBufferSubData>(ref glGetBufferSubDataDelegate)(target, offset, size, data);
        }
        public IntPtr MapBuffer(uint target, uint access)
        {
            return(IntPtr)getDelegateFor<glMapBuffer>(ref glMapBufferDelegate)(target, access);
        }
        public bool UnmapBuffer(uint target)
        {
            return(bool)getDelegateFor<glUnmapBuffer>(ref glUnmapBufferDelegate)(target);
        }
        public void GetBufferParameter(uint target, uint pname, int[] parameters)
        {
            getDelegateFor<glGetBufferParameteriv>(ref glGetBufferParameterivDelegate)(target, pname, parameters);
        }
        public void GetBufferPointer(uint target, uint pname, IntPtr[] parameters)
        {
            getDelegateFor<glGetBufferPointerv>(ref glGetBufferPointervDelegate)(target, pname, parameters);
        }
        
        //  Delegates
        private delegate void glGenQueries(int n, uint[] ids);
		private Delegate glGenQueriesDelegate;
        private delegate void glDeleteQueries(int n, uint[] ids);
		private Delegate glDeleteQueriesDelegate;
        private delegate bool glIsQuery(uint id);
		private Delegate glIsQueryDelegate;
        private delegate void glBeginQuery(uint target, uint id);
		private Delegate glBeginQueryDelegate;
        private delegate void glEndQuery(uint target);
		private Delegate glEndQueryDelegate;
        private delegate void glGetQueryiv(uint target, uint pname, int[] parameters);
		private Delegate glGetQueryivDelegate;
        private delegate void glGetQueryObjectiv(uint id, uint pname, int[] parameters);
		private Delegate glGetQueryObjectivDelegate;
        private delegate void glGetQueryObjectuiv(uint id, uint pname, uint[] parameters);
		private Delegate glGetQueryObjectuivDelegate;
        private delegate void glBindBuffer(uint target, uint buffer);
		private Delegate glBindBufferDelegate;
        private delegate void glDeleteBuffers(int n, uint[] buffers);
		private Delegate glDeleteBuffersDelegate;
        private delegate void glGenBuffers(int n, uint[] buffers);
		private Delegate glGenBuffersDelegate;
        private delegate bool glIsBuffer(uint buffer);
		private Delegate glIsBufferDelegate;
        private delegate void glBufferData(uint target, int size, IntPtr data, uint usage);
		private Delegate glBufferDataDelegate;
        private delegate void glBufferSubData(uint target, int offset, int size, IntPtr data);
		private Delegate glBufferSubDataDelegate;
        private delegate void glGetBufferSubData(uint target, int offset, int size, IntPtr data);
		private Delegate glGetBufferSubDataDelegate;
        private delegate IntPtr glMapBuffer(uint target, uint access);
		private Delegate glMapBufferDelegate;
        private delegate bool glUnmapBuffer(uint target);
		private Delegate glUnmapBufferDelegate;
        private delegate void glGetBufferParameteriv(uint target, uint pname, int[] parameters);
		private Delegate glGetBufferParameterivDelegate;
        private delegate void glGetBufferPointerv(uint target, uint pname, IntPtr[] parameters);
		private Delegate glGetBufferPointervDelegate;

        //  Constants
        public const uint GL_BUFFER_SIZE                             = 0x8764;
        public const uint GL_BUFFER_USAGE                            = 0x8765;
        public const uint GL_QUERY_COUNTER_BITS                      = 0x8864;
        public const uint GL_CURRENT_QUERY                           = 0x8865;
        public const uint GL_QUERY_RESULT                            = 0x8866;
        public const uint GL_QUERY_RESULT_AVAILABLE                  = 0x8867;
        public const uint GL_ARRAY_BUFFER                            = 0x8892;
        public const uint GL_ELEMENT_ARRAY_BUFFER                    = 0x8893;
        public const uint GL_ARRAY_BUFFER_BINDING                    = 0x8894;
        public const uint GL_ELEMENT_ARRAY_BUFFER_BINDING            = 0x8895;
        public const uint GL_VERTEX_ATTRIB_ARRAY_BUFFER_BINDING      = 0x889F;
        public const uint GL_READ_ONLY                               = 0x88B8;
        public const uint GL_WRITE_ONLY                              = 0x88B9;
        public const uint GL_READ_WRITE                              = 0x88BA;
        public const uint GL_BUFFER_ACCESS                           = 0x88BB;
        public const uint GL_BUFFER_MAPPED                           = 0x88BC;
        public const uint GL_BUFFER_MAP_POINTER                      = 0x88BD;
        public const uint GL_STREAM_DRAW                             = 0x88E0;
        public const uint GL_STREAM_READ                             = 0x88E1;
        public const uint GL_STREAM_COPY                             = 0x88E2;
        public const uint GL_STATIC_DRAW                             = 0x88E4;
        public const uint GL_STATIC_READ                             = 0x88E5;
        public const uint GL_STATIC_COPY                             = 0x88E6;
        public const uint GL_DYNAMIC_DRAW                            = 0x88E8;
        public const uint GL_DYNAMIC_READ                            = 0x88E9;
        public const uint GL_DYNAMIC_COPY                            = 0x88EA;
        public const uint GL_SAMPLES_PASSED                          = 0x8914;

        #endregion
        
        #region OpenGL 2.0

        //  Methods
        public void BlendEquationSeparate(uint modeRGB, uint modeAlpha)
        {
            getDelegateFor<glBlendEquationSeparate>(ref glBlendEquationSeparateDelegate)(modeRGB, modeAlpha);
        }
        public void DrawBuffers(int n, uint[] bufs)
        {
            getDelegateFor<glDrawBuffers>(ref glDrawBuffersDelegate)(n, bufs);
        }
        public void StencilOpSeparate(uint face, uint sfail, uint dpfail, uint dppass)
        {
            getDelegateFor<glStencilOpSeparate>(ref glStencilOpSeparateDelegate)(face, sfail, dpfail, dppass);
        }
        public void StencilFuncSeparate(uint face, uint func, int reference, uint mask)
        {
            getDelegateFor<glStencilFuncSeparate>(ref glStencilFuncSeparateDelegate)(face, func, reference, mask);
        }
        public void StencilMaskSeparate(uint face, uint mask)
        {
            getDelegateFor<glStencilMaskSeparate>(ref glStencilMaskSeparateDelegate)(face, mask);
        }
        public void AttachShader(uint program, uint shader)
        {
            getDelegateFor<glAttachShader>(ref glAttachShaderDelegate)(program, shader);
        }
        public void BindAttribLocation(uint program, uint index, string name)
        {
            getDelegateFor<glBindAttribLocation>(ref glBindAttribLocationDelegate)(program, index, name);
        }
        /// <summary>
        /// Compile a shader object
        /// </summary>
        /// <param name="shader">Specifies the shader object to be compiled.</param>
        public void CompileShader(uint shader)
        {
            getDelegateFor<glCompileShader>(ref glCompileShaderDelegate)(shader);
        }
        public uint CreateProgram()
        {
            return(uint)getDelegateFor<glCreateProgram>(ref glCreateProgramDelegate)();
        }
        /// <summary>
        /// Create a shader object
        /// </summary>
        /// <param name="type">Specifies the type of shader to be created. Must be either GL_VERTEX_SHADER or GL_FRAGMENT_SHADER.</param>
        /// <returns>This function returns 0 if an error occurs creating the shader object. Otherwise the shader id is returned.</returns>
        public uint CreateShader(uint type)
        {
            return(uint)getDelegateFor<glCreateShader>(ref glCreateShaderDelegate)(type);
        }
        public void DeleteProgram(uint program)
        {
            getDelegateFor<glDeleteProgram>(ref glDeleteProgramDelegate)(program);
        }
        public void DeleteShader(uint shader)
        {
            getDelegateFor<glDeleteShader>(ref glDeleteShaderDelegate)(shader);
        }
        public void DetachShader(uint program, uint shader)
        {
            getDelegateFor<glDetachShader>(ref glDetachShaderDelegate)(program, shader);
        }
        public void DisableVertexAttribArray(uint index)
        {
            getDelegateFor<glDisableVertexAttribArray>(ref glDisableVertexAttribArrayDelegate)(index);
        }
        public void EnableVertexAttribArray(uint index)
        {
            getDelegateFor<glEnableVertexAttribArray>(ref glEnableVertexAttribArrayDelegate)(index);
        }


        /// <summary>
        /// Return information about an active attribute variable
        /// </summary>
        /// <param name="program">Specifies the program object to be queried.</param>
        /// <param name="index">Specifies the index of the attribute variable to be queried.</param>
        /// <param name="bufSize">Specifies the maximum number of characters OpenGL is allowed to write in the character buffer indicated by <paramref name="name"/>.</param>
        /// <param name="length">Returns the number of characters actually written by OpenGL in the string indicated by name(excluding the null terminator) if a value other than NULL is passed.</param>
        /// <param name="size">Returns the size of the attribute variable.</param>
        /// <param name="type">Returns the data type of the attribute variable.</param>
        /// <param name="name">Returns a null terminated string containing the name of the attribute variable.</param>
        public void GetActiveAttrib(uint program, uint index, int bufSize, out int length, out int size, out uint type, out string name)
        {
            var builder = new StringBuilder(bufSize);
            getDelegateFor<glGetActiveAttrib>(ref glGetActiveAttribDelegate)(program, index, bufSize, out length, out size, out type, builder);
            name = builder.ToString();
        }

        /// <summary>
        /// Return information about an active uniform variable
        /// </summary>
        /// <param name="program">Specifies the program object to be queried.</param>
        /// <param name="index">Specifies the index of the uniform variable to be queried.</param>
        /// <param name="bufSize">Specifies the maximum number of characters OpenGL is allowed 
        /// to write in the character buffer indicated by <paramref name="name"/>.</param>
        /// <param name="length">Returns the number of characters actually written by OpenGL in the string indicated by name 
        ///(excluding the null terminator) if a value other than NULL is passed.</param>
        /// <param name="size">Returns the size of the uniform variable.</param>
        /// <param name="type">Returns the data type of the uniform variable.</param>
        /// <param name="name">Returns a null terminated string containing the name of the uniform variable.</param>
        public void GetActiveUniform(uint program, uint index, int bufSize, out int length, out int size, out uint type, out string name)
        {
            var builder = new StringBuilder(bufSize);
            getDelegateFor<glGetActiveUniform>(ref glGetActiveUniformDelegate)(program, index, bufSize, out length, out size, out type, builder);
            name = builder.ToString();
        }

        public void GetAttachedShaders(uint program, int maxCount, int[] count, uint[] obj)
        {
            getDelegateFor<glGetAttachedShaders>(ref glGetAttachedShadersDelegate)(program, maxCount, count, obj);
        }
        public int GetAttribLocation(uint program, string name)
        {
            return(int)getDelegateFor<glGetAttribLocation>(ref glGetAttribLocationDelegate)(program, name);
        }
        public void GetProgram(uint program, uint pname, int[] parameters)
        {
            getDelegateFor<glGetProgramiv>(ref glGetProgramivDelegate)(program, pname, parameters);
        }
        public void GetProgramInfoLog(uint program, int bufSize, IntPtr length, StringBuilder infoLog)
        {
            getDelegateFor<glGetProgramInfoLog>(ref glGetProgramInfoLogDelegate)(program, bufSize, length, infoLog);
        }
        public void GetShader(uint shader, uint pname, int[] parameters)
        {
            getDelegateFor<glGetShaderiv>(ref glGetShaderivDelegate)(shader, pname, parameters);
        }
        public void GetShaderInfoLog(uint shader, int bufSize, IntPtr length, StringBuilder infoLog)
        {
            getDelegateFor<glGetShaderInfoLog>(ref glGetShaderInfoLogDelegate)(shader, bufSize, length, infoLog);

        }
        public void GetShaderSource(uint shader, int bufSize, IntPtr length, StringBuilder source)
        {
            getDelegateFor<glGetShaderSource>(ref glGetShaderSourceDelegate)(shader, bufSize, length, source);
        }
        /// <summary>
        /// Returns an integer that represents the location of a specific uniform variable within a program object. name must be a null terminated string that contains no white space. name must be an active uniform variable name in program that is not a structure, an array of structures, or a subcomponent of a vector or a matrix. This function returns -1 if name does not correspond to an active uniform variable in program, if name starts with the reserved prefix "gl_", or if name is associated with an atomic counter or a named uniform block.
        /// </summary>
        /// <param name="program">Specifies the program object to be queried.</param>
        /// <param name="name">Points to a null terminated string containing the name of the uniform variable whose location is to be queried.</param>
        /// <returns></returns>
        public int GetUniformLocation(uint program, string name)
        {
            return(int)getDelegateFor<glGetUniformLocation>(ref glGetUniformLocationDelegate)(program, name);
        }
        public void GetUniform(uint program, int location, float[] parameters)
        {
            getDelegateFor<glGetUniformfv>(ref glGetUniformfvDelegate)(program, location, parameters);
        }
        public void GetUniform(uint program, int location, int[] parameters)
        {
            getDelegateFor<glGetUniformiv>(ref glGetUniformivDelegate)(program, location, parameters);
        }
        public void GetVertexAttrib(uint index, uint pname, double[] parameters)
        {
            getDelegateFor<glGetVertexAttribdv>(ref glGetVertexAttribdvDelegate)(index, pname, parameters);
        }
        public void GetVertexAttrib(uint index, uint pname, float[] parameters)
        {
            getDelegateFor<glGetVertexAttribfv>(ref glGetVertexAttribfvDelegate)(index, pname, parameters);
        }
        public void GetVertexAttrib(uint index, uint pname, int[] parameters)
        {
            getDelegateFor<glGetVertexAttribiv>(ref glGetVertexAttribivDelegate)(index, pname, parameters);
        }
        public void GetVertexAttribPointer(uint index, uint pname, IntPtr pointer)
        {
            getDelegateFor<glGetVertexAttribPointerv>(ref glGetVertexAttribPointervDelegate)(index, pname, pointer);
        }
        public bool IsProgram(uint program)
        {
            return(bool)getDelegateFor<glIsProgram>(ref glIsProgramDelegate)(program);
        }
        public bool IsShader(uint shader)
        {
            return(bool)getDelegateFor<glIsShader>(ref glIsShaderDelegate)(shader);
        }
        public void LinkProgram(uint program)
        {
            getDelegateFor<glLinkProgram>(ref glLinkProgramDelegate)(program);
        }

        /// <summary>
        /// Replace the source code in a shader object
        /// </summary>
        /// <param name="shader">Specifies the handle of the shader object whose source code is to be replaced.</param>
        /// <param name="source">The source.</param>
        public void ShaderSource(uint shader, string source)
        {
            //  Remember, the function takes an array of strings but concatenates them, so we should NOT split into lines!
            getDelegateFor<glShaderSource>(ref glShaderSourceDelegate)(shader, 1, new[] { source }, new[] { source.Length });
        }

        public void UseProgram(uint program)
        {
            getDelegateFor<glUseProgram>(ref glUseProgramDelegate)(program);
        }
        public void Uniform1(int location, float v0)
        {
            getDelegateFor<glUniform1f>(ref glUniform1fDelegate)(location, v0);
        }
        public void Uniform2(int location, float v0, float v1)
        {
            getDelegateFor<glUniform2f>(ref glUniform2fDelegate)(location, v0, v1);
        }
        public void Uniform3(int location, float v0, float v1, float v2)
        {
            getDelegateFor<glUniform3f>(ref glUniform3fDelegate)(location, v0, v1, v2);
        }
        public void Uniform4(int location, float v0, float v1, float v2, float v3)
        {
            getDelegateFor<glUniform4f>(ref glUniform4fDelegate)(location, v0, v1, v2, v3);
        }
        public void Uniform1(int location, int v0)
        {
            getDelegateFor<glUniform1i>(ref glUniform1iDelegate)(location, v0);
        }
        public void Uniform2(int location, int v0, int v1)
        {
            getDelegateFor<glUniform2i>(ref glUniform2iDelegate)(location, v0, v1);
        }
        public void Uniform3(int location, int v0, int v1, int v2)
        {
            getDelegateFor<glUniform3i>(ref glUniform3iDelegate)(location, v0, v1, v2);
        }
        public void Uniform(int location, int v0, int v1, int v2, int v3)
        {
            getDelegateFor<glUniform4i>(ref glUniform4iDelegate)(location, v0, v1, v2, v3);
        }
        public void Uniform1(int location, int count, float[] value)
        {
            getDelegateFor<glUniform1fv>(ref glUniform1fvDelegate)(location, count, value);
        }
        public void Uniform2(int location, int count, float[] value)
        {
            getDelegateFor<glUniform2fv>(ref glUniform2fvDelegate)(location, count, value);
        }
        public void Uniform3(int location, int count, float[] value)
        {
            getDelegateFor<glUniform3fv>(ref glUniform3fvDelegate)(location, count, value);
        }
        public void Uniform4(int location, int count, float[] value)
        {
            getDelegateFor<glUniform4fv>(ref glUniform4fvDelegate)(location, count, value);
        }
        public void Uniform1(int location, int count, int[] value)
        {
            getDelegateFor<glUniform1iv>(ref glUniform1ivDelegate)(location, count, value);
        }
        public void Uniform2(int location, int count, int[] value)
        {
            getDelegateFor<glUniform2iv>(ref glUniform2ivDelegate)(location, count, value);
        }
        public void Uniform3(int location, int count, int[] value)
        {
            getDelegateFor<glUniform3iv>(ref glUniform3ivDelegate)(location, count, value);
        }
        public void Uniform4(int location, int count, int[] value)
        {
            getDelegateFor<glUniform4iv>(ref glUniform4ivDelegate)(location, count, value);
        }
        public void UniformMatrix2(int location, int count, bool transpose, float[] value)
        {
            getDelegateFor<glUniformMatrix2fv>(ref glUniformMatrix2fvDelegate)(location, count, transpose, value);
        }
        public void UniformMatrix3(int location, int count, bool transpose, float[] value)
        {
            getDelegateFor<glUniformMatrix3fv>(ref glUniformMatrix3fvDelegate)(location, count, transpose, value);
        }
        public void UniformMatrix4(int location, int count, bool transpose, float[] value)
        {
            getDelegateFor<glUniformMatrix4fv>(ref glUniformMatrix4fvDelegate)(location, count, transpose, value);
        }
        public void ValidateProgram(uint program)
        {
            getDelegateFor<glValidateProgram>(ref glValidateProgramDelegate)(program);
        }
        public void VertexAttrib1(uint index, double x)
        {
            getDelegateFor<glVertexAttrib1d>(ref glVertexAttrib1dDelegate)(index, x);
        }
        public void VertexAttrib1(uint index, double[] v)
        {
            getDelegateFor<glVertexAttrib1dv>(ref glVertexAttrib1dvDelegate)(index, v);
        }
        public void VertexAttrib(uint index, float x)
        {
            getDelegateFor<glVertexAttrib1f>(ref glVertexAttrib1fDelegate)(index, x);
        }
        public void VertexAttrib1(uint index, float[] v)
        {
            getDelegateFor<glVertexAttrib1fv>(ref glVertexAttrib1fvDelegate)(index, v);
        }
        public void VertexAttrib(uint index, short x)
        {
            getDelegateFor<glVertexAttrib1s>(ref glVertexAttrib1sDelegate)(index, x);
        }
        public void VertexAttrib1(uint index, short[] v)
        {
            getDelegateFor<glVertexAttrib1sv>(ref glVertexAttrib1svDelegate)(index, v);
        }
        public void VertexAttrib2(uint index, double x, double y)
        {
            getDelegateFor<glVertexAttrib2d>(ref glVertexAttrib2dDelegate)(index, x, y);
        }
        public void VertexAttrib2(uint index, double[] v)
        {
            getDelegateFor<glVertexAttrib2dv>(ref glVertexAttrib2dvDelegate)(index, v);
        }
        public void VertexAttrib2(uint index, float x, float y)
        {
            getDelegateFor<glVertexAttrib2f>(ref glVertexAttrib2fDelegate)(index, x, y);
        }
        public void VertexAttrib2(uint index, float[] v)
        {
            getDelegateFor<glVertexAttrib2fv>(ref glVertexAttrib2fvDelegate)(index, v);
        }
        public void VertexAttrib2(uint index, short x, short y)
        {
            getDelegateFor<glVertexAttrib2s>(ref glVertexAttrib2sDelegate)(index, x, y);
        }
        public void VertexAttrib2(uint index, short[] v)
        {
            getDelegateFor<glVertexAttrib2sv>(ref glVertexAttrib2svDelegate)(index, v);
        }
        public void VertexAttrib3(uint index, double x, double y, double z)
        {
            getDelegateFor<glVertexAttrib3d>(ref glVertexAttrib3dDelegate)(index, x, y, z);
        }
        public void VertexAttrib3(uint index, double[] v)
        {
            getDelegateFor<glVertexAttrib3dv>(ref glVertexAttrib3dvDelegate)(index, v);
        }
        public void VertexAttrib3(uint index, float x, float y, float z)
        {
            getDelegateFor<glVertexAttrib3f>(ref glVertexAttrib3fDelegate)(index, x, y, z);
        }
        public void VertexAttrib3(uint index, float[] v)
        {
            getDelegateFor<glVertexAttrib3fv>(ref glVertexAttrib3fvDelegate)(index, v);
        }
        public void VertexAttrib3(uint index, short x, short y, short z)
        {
            getDelegateFor<glVertexAttrib3s>(ref glVertexAttrib3sDelegate)(index, x, y, z);
        }
        public void VertexAttrib3(uint index, short[] v)
        {
            getDelegateFor<glVertexAttrib3sv>(ref glVertexAttrib3svDelegate)(index, v);
        }
        public void VertexAttrib4N(uint index, sbyte[] v)
        {
            getDelegateFor<glVertexAttrib4Nbv>(ref glVertexAttrib4NbvDelegate)(index, v);
        }
        public void VertexAttrib4N(uint index, int[] v)
        {
            getDelegateFor<glVertexAttrib4Niv>(ref glVertexAttrib4NivDelegate)(index, v);
        }
        public void VertexAttrib4N(uint index, short[] v)
        {
            getDelegateFor<glVertexAttrib4Nsv>(ref glVertexAttrib4NsvDelegate)(index, v);
        }
        public void VertexAttrib4N(uint index, byte x, byte y, byte z, byte w)
        {
            getDelegateFor<glVertexAttrib4Nub>(ref glVertexAttrib4NubDelegate)(index, x, y, z, w);
        }
        public void VertexAttrib4N(uint index, byte[] v)
        {
            getDelegateFor<glVertexAttrib4Nubv>(ref glVertexAttrib4NubvDelegate)(index, v);
        }
        public void VertexAttrib4N(uint index, uint[] v)
        {
            getDelegateFor<glVertexAttrib4Nuiv>(ref glVertexAttrib4NuivDelegate)(index, v);
        }
        public void VertexAttrib4N(uint index, ushort[] v)
        {
            getDelegateFor<glVertexAttrib4Nusv>(ref glVertexAttrib4NusvDelegate)(index, v);
        }
        public void VertexAttrib4(uint index, sbyte[] v)
        {
            getDelegateFor<glVertexAttrib4bv>(ref glVertexAttrib4bvDelegate)(index, v);
        }
        public void VertexAttrib4(uint index, double x, double y, double z, double w)
        {
            getDelegateFor<glVertexAttrib4d>(ref glVertexAttrib4dDelegate)(index, x, y, z, w);
        }
        public void VertexAttrib4(uint index, double[] v)
        {
            getDelegateFor<glVertexAttrib4dv>(ref glVertexAttrib4dvDelegate)(index, v);
        }
        public void VertexAttrib4(uint index, float x, float y, float z, float w)
        {
            getDelegateFor<glVertexAttrib4f>(ref glVertexAttrib4fDelegate)(index, x, y, z, w);
        }
        public void VertexAttrib4(uint index, float[] v)
        {
            getDelegateFor<glVertexAttrib4fv>(ref glVertexAttrib4fvDelegate)(index, v);
        }
        public void VertexAttrib4(uint index, int[] v)
        {
            getDelegateFor<glVertexAttrib4iv>(ref glVertexAttrib4ivDelegate)(index, v);
        }
        public void VertexAttrib4(uint index, short x, short y, short z, short w)
        {
            getDelegateFor<glVertexAttrib4s>(ref glVertexAttrib4sDelegate)(index, x, y, z, w);
        }
        public void VertexAttrib4(uint index, short[] v)
        {
            getDelegateFor<glVertexAttrib4sv>(ref glVertexAttrib4svDelegate)(index, v);
        }
        public void VertexAttrib4(uint index, byte[] v)
        {
            getDelegateFor<glVertexAttrib4ubv>(ref glVertexAttrib4ubvDelegate)(index, v);
        }
        public void VertexAttrib4(uint index, uint[] v)
        {
            getDelegateFor<glVertexAttrib4uiv>(ref glVertexAttrib4uivDelegate)(index, v);
        }
        public void VertexAttrib4(uint index, ushort[] v)
        {
            getDelegateFor<glVertexAttrib4usv>(ref glVertexAttrib4usvDelegate)(index, v);
        }
        public void VertexAttribPointer(uint index, int size, uint type, bool normalized, int stride, IntPtr pointer)
        {
            getDelegateFor<glVertexAttribPointer>(ref glVertexAttribPointerDelegate)(index, size, type, normalized, stride, pointer);
        }

        //  Delegates
        private delegate void glBlendEquationSeparate(uint modeRGB, uint modeAlpha);
		private Delegate glBlendEquationSeparateDelegate;
        private delegate void glDrawBuffers(int n, uint[] bufs);
		private Delegate glDrawBuffersDelegate;
        private delegate void glStencilOpSeparate(uint face, uint sfail, uint dpfail, uint dppass);
		private Delegate glStencilOpSeparateDelegate;
        private delegate void glStencilFuncSeparate(uint face, uint func, int reference, uint mask);
		private Delegate glStencilFuncSeparateDelegate;
        private delegate void glStencilMaskSeparate(uint face, uint mask);
		private Delegate glStencilMaskSeparateDelegate;
        private delegate void glAttachShader(uint program, uint shader);
		private Delegate glAttachShaderDelegate;
        private delegate void glBindAttribLocation(uint program, uint index, string name);
		private Delegate glBindAttribLocationDelegate;
        private delegate void glCompileShader(uint shader);
		private Delegate glCompileShaderDelegate;
        private delegate uint glCreateProgram();
		private Delegate glCreateProgramDelegate;
        private delegate uint glCreateShader(uint type);
		private Delegate glCreateShaderDelegate;
        private delegate void glDeleteProgram(uint program);
		private Delegate glDeleteProgramDelegate;
        private delegate void glDeleteShader(uint shader);
		private Delegate glDeleteShaderDelegate;
        private delegate void glDetachShader(uint program, uint shader);
		private Delegate glDetachShaderDelegate;
        private delegate void glDisableVertexAttribArray(uint index);
		private Delegate glDisableVertexAttribArrayDelegate;
        private delegate void glEnableVertexAttribArray(uint index);
		private Delegate glEnableVertexAttribArrayDelegate;
        private delegate void glGetActiveAttrib(uint program, uint index, int bufSize, out int length, out int size, out uint type, StringBuilder name);
		private Delegate glGetActiveAttribDelegate;
        private delegate void glGetActiveUniform(uint program, uint index, int bufSize, out int length, out int size, out uint type, StringBuilder name);
		private Delegate glGetActiveUniformDelegate;
        private delegate void glGetAttachedShaders(uint program, int maxCount, int[] count, uint[] obj);
		private Delegate glGetAttachedShadersDelegate;
        private delegate int glGetAttribLocation(uint program, string name);
		private Delegate glGetAttribLocationDelegate;
        private delegate void glGetProgramiv(uint program, uint pname, int[] parameters);
		private Delegate glGetProgramivDelegate;
        private delegate void glGetProgramInfoLog(uint program, int bufSize, IntPtr length, StringBuilder infoLog);
		private Delegate glGetProgramInfoLogDelegate;
        private delegate void glGetShaderiv(uint shader, uint pname, int[] parameters);
		private Delegate glGetShaderivDelegate;
        private delegate void glGetShaderInfoLog(uint shader, int bufSize, IntPtr length, StringBuilder infoLog);
		private Delegate glGetShaderInfoLogDelegate;
        private delegate void glGetShaderSource(uint shader, int bufSize, IntPtr length, StringBuilder source);
		private Delegate glGetShaderSourceDelegate;
        private delegate int glGetUniformLocation(uint program, string name);
		private Delegate glGetUniformLocationDelegate;
        private delegate void glGetUniformfv(uint program, int location, float[] parameters);
		private Delegate glGetUniformfvDelegate;
        private delegate void glGetUniformiv(uint program, int location, int[] parameters);
		private Delegate glGetUniformivDelegate;
        private delegate void glGetVertexAttribdv(uint index, uint pname, double[] parameters);
		private Delegate glGetVertexAttribdvDelegate;
        private delegate void glGetVertexAttribfv(uint index, uint pname, float[] parameters);
		private Delegate glGetVertexAttribfvDelegate;
        private delegate void glGetVertexAttribiv(uint index, uint pname, int[] parameters);
		private Delegate glGetVertexAttribivDelegate;
        private delegate void glGetVertexAttribPointerv(uint index, uint pname, IntPtr pointer);
		private Delegate glGetVertexAttribPointervDelegate;
        private delegate bool glIsProgram(uint program);
		private Delegate glIsProgramDelegate;
        private delegate bool glIsShader(uint shader);
		private Delegate glIsShaderDelegate;
        private delegate void glLinkProgram(uint program);
		private Delegate glLinkProgramDelegate;
        private delegate void glShaderSource(uint shader, int count, string[] source, int[] length);
		private Delegate glShaderSourceDelegate;
        private delegate void glUseProgram(uint program);
		private Delegate glUseProgramDelegate;
        private delegate void glUniform1f(int location, float v0);
		private Delegate glUniform1fDelegate;
        private delegate void glUniform2f(int location, float v0, float v1);
		private Delegate glUniform2fDelegate;
        private delegate void glUniform3f(int location, float v0, float v1, float v2);
		private Delegate glUniform3fDelegate;
        private delegate void glUniform4f(int location, float v0, float v1, float v2, float v3);
		private Delegate glUniform4fDelegate;
        private delegate void glUniform1i(int location, int v0);
		private Delegate glUniform1iDelegate;
        private delegate void glUniform2i(int location, int v0, int v1);
		private Delegate glUniform2iDelegate;
        private delegate void glUniform3i(int location, int v0, int v1, int v2);
		private Delegate glUniform3iDelegate;
        private delegate void glUniform4i(int location, int v0, int v1, int v2, int v3);
		private Delegate glUniform4iDelegate;
        private delegate void glUniform1fv(int location, int count, float[] value);
		private Delegate glUniform1fvDelegate;
        private delegate void glUniform2fv(int location, int count, float[] value);
		private Delegate glUniform2fvDelegate;
        private delegate void glUniform3fv(int location, int count, float[] value);
		private Delegate glUniform3fvDelegate;
        private delegate void glUniform4fv(int location, int count, float[] value);
		private Delegate glUniform4fvDelegate;
        private delegate void glUniform1iv(int location, int count, int[] value);
		private Delegate glUniform1ivDelegate;
        private delegate void glUniform2iv(int location, int count, int[] value);
		private Delegate glUniform2ivDelegate;
        private delegate void glUniform3iv(int location, int count, int[] value);
		private Delegate glUniform3ivDelegate;
        private delegate void glUniform4iv(int location, int count, int[] value);
		private Delegate glUniform4ivDelegate;
        private delegate void glUniformMatrix2fv(int location, int count, bool transpose, float[] value);
		private Delegate glUniformMatrix2fvDelegate;
        private delegate void glUniformMatrix3fv(int location, int count, bool transpose, float[] value);
		private Delegate glUniformMatrix3fvDelegate;
        private delegate void glUniformMatrix4fv(int location, int count, bool transpose, float[] value);
		private Delegate glUniformMatrix4fvDelegate;
        private delegate void glValidateProgram(uint program);
		private Delegate glValidateProgramDelegate;
        private delegate void glVertexAttrib1d(uint index, double x);
		private Delegate glVertexAttrib1dDelegate;
        private delegate void glVertexAttrib1dv(uint index, double[] v);
		private Delegate glVertexAttrib1dvDelegate;
        private delegate void glVertexAttrib1f(uint index, float x);
		private Delegate glVertexAttrib1fDelegate;
        private delegate void glVertexAttrib1fv(uint index, float[] v);
		private Delegate glVertexAttrib1fvDelegate;
        private delegate void glVertexAttrib1s(uint index, short x);
		private Delegate glVertexAttrib1sDelegate;
        private delegate void glVertexAttrib1sv(uint index, short[] v);
		private Delegate glVertexAttrib1svDelegate;
        private delegate void glVertexAttrib2d(uint index, double x, double y);
		private Delegate glVertexAttrib2dDelegate;
        private delegate void glVertexAttrib2dv(uint index, double[] v);
		private Delegate glVertexAttrib2dvDelegate;
        private delegate void glVertexAttrib2f(uint index, float x, float y);
		private Delegate glVertexAttrib2fDelegate;
        private delegate void glVertexAttrib2fv(uint index, float[] v);
		private Delegate glVertexAttrib2fvDelegate;
        private delegate void glVertexAttrib2s(uint index, short x, short y);
		private Delegate glVertexAttrib2sDelegate;
        private delegate void glVertexAttrib2sv(uint index, short[] v);
		private Delegate glVertexAttrib2svDelegate;
        private delegate void glVertexAttrib3d(uint index, double x, double y, double z);
		private Delegate glVertexAttrib3dDelegate;
        private delegate void glVertexAttrib3dv(uint index, double[] v);
		private Delegate glVertexAttrib3dvDelegate;
        private delegate void glVertexAttrib3f(uint index, float x, float y, float z);
		private Delegate glVertexAttrib3fDelegate;
        private delegate void glVertexAttrib3fv(uint index, float[] v);
		private Delegate glVertexAttrib3fvDelegate;
        private delegate void glVertexAttrib3s(uint index, short x, short y, short z);
		private Delegate glVertexAttrib3sDelegate;
        private delegate void glVertexAttrib3sv(uint index, short[] v);
		private Delegate glVertexAttrib3svDelegate;
        private delegate void glVertexAttrib4Nbv(uint index, sbyte[] v);
		private Delegate glVertexAttrib4NbvDelegate;
        private delegate void glVertexAttrib4Niv(uint index, int[] v);
		private Delegate glVertexAttrib4NivDelegate;
        private delegate void glVertexAttrib4Nsv(uint index, short[] v);
		private Delegate glVertexAttrib4NsvDelegate;
        private delegate void glVertexAttrib4Nub(uint index, byte x, byte y, byte z, byte w);
		private Delegate glVertexAttrib4NubDelegate;
        private delegate void glVertexAttrib4Nubv(uint index, byte[] v);
		private Delegate glVertexAttrib4NubvDelegate;
        private delegate void glVertexAttrib4Nuiv(uint index, uint[] v);
		private Delegate glVertexAttrib4NuivDelegate;
        private delegate void glVertexAttrib4Nusv(uint index, ushort[] v);
		private Delegate glVertexAttrib4NusvDelegate;
        private delegate void glVertexAttrib4bv(uint index, sbyte[] v);
		private Delegate glVertexAttrib4bvDelegate;
        private delegate void glVertexAttrib4d(uint index, double x, double y, double z, double w);
		private Delegate glVertexAttrib4dDelegate;
        private delegate void glVertexAttrib4dv(uint index, double[] v);
		private Delegate glVertexAttrib4dvDelegate;
        private delegate void glVertexAttrib4f(uint index, float x, float y, float z, float w);
		private Delegate glVertexAttrib4fDelegate;
        private delegate void glVertexAttrib4fv(uint index, float[] v);
		private Delegate glVertexAttrib4fvDelegate;
        private delegate void glVertexAttrib4iv(uint index, int[] v);
		private Delegate glVertexAttrib4ivDelegate;
        private delegate void glVertexAttrib4s(uint index, short x, short y, short z, short w);
		private Delegate glVertexAttrib4sDelegate;
        private delegate void glVertexAttrib4sv(uint index, short[] v);
		private Delegate glVertexAttrib4svDelegate;
        private delegate void glVertexAttrib4ubv(uint index, byte[] v);
		private Delegate glVertexAttrib4ubvDelegate;
        private delegate void glVertexAttrib4uiv(uint index, uint[] v);
		private Delegate glVertexAttrib4uivDelegate;
        private delegate void glVertexAttrib4usv(uint index, ushort[] v);
		private Delegate glVertexAttrib4usvDelegate;
        private delegate void glVertexAttribPointer(uint index, int size, uint type, bool normalized, int stride, IntPtr pointer);
		private Delegate glVertexAttribPointerDelegate;

        //  Constants
        public const uint GL_BLEND_EQUATION_RGB                  = 0x8009;
        public const uint GL_VERTEX_ATTRIB_ARRAY_ENABLED         = 0x8622;
        public const uint GL_VERTEX_ATTRIB_ARRAY_SIZE            = 0x8623;
        public const uint GL_VERTEX_ATTRIB_ARRAY_STRIDE          = 0x8624;
        public const uint GL_VERTEX_ATTRIB_ARRAY_TYPE            = 0x8625;
        public const uint GL_CURRENT_VERTEX_ATTRIB               = 0x8626;
        public const uint GL_VERTEX_PROGRAM_POINT_SIZE           = 0x8642;
        public const uint GL_VERTEX_ATTRIB_ARRAY_POINTER         = 0x8645;
        public const uint GL_STENCIL_BACK_FUNC                   = 0x8800;
        public const uint GL_STENCIL_BACK_FAIL                   = 0x8801;
        public const uint GL_STENCIL_BACK_PASS_DEPTH_FAIL        = 0x8802;
        public const uint GL_STENCIL_BACK_PASS_DEPTH_PASS        = 0x8803;
        public const uint GL_MAX_DRAW_BUFFERS                    = 0x8824;
        public const uint GL_DRAW_BUFFER0                        = 0x8825;
        public const uint GL_DRAW_BUFFER1                        = 0x8826;
        public const uint GL_DRAW_BUFFER2                        = 0x8827;
        public const uint GL_DRAW_BUFFER3                        = 0x8828;
        public const uint GL_DRAW_BUFFER4                        = 0x8829;
        public const uint GL_DRAW_BUFFER5                        = 0x882A;
        public const uint GL_DRAW_BUFFER6                        = 0x882B;
        public const uint GL_DRAW_BUFFER7                        = 0x882C;
        public const uint GL_DRAW_BUFFER8                        = 0x882D;
        public const uint GL_DRAW_BUFFER9                        = 0x882E;
        public const uint GL_DRAW_BUFFER10                       = 0x882F;
        public const uint GL_DRAW_BUFFER11                       = 0x8830;
        public const uint GL_DRAW_BUFFER12                       = 0x8831;
        public const uint GL_DRAW_BUFFER13                       = 0x8832;
        public const uint GL_DRAW_BUFFER14                       = 0x8833;
        public const uint GL_DRAW_BUFFER15                       = 0x8834;
        public const uint GL_BLEND_EQUATION_ALPHA                = 0x883D;
        public const uint GL_MAX_VERTEX_ATTRIBS                  = 0x8869;
        public const uint GL_VERTEX_ATTRIB_ARRAY_NORMALIZED      = 0x886A;
        public const uint GL_MAX_TEXTURE_IMAGE_UNITS             = 0x8872;
        public const uint GL_FRAGMENT_SHADER                     = 0x8B30;
        public const uint GL_VERTEX_SHADER                       = 0x8B31;
        public const uint GL_MAX_FRAGMENT_UNIFORM_COMPONENTS     = 0x8B49;
        public const uint GL_MAX_VERTEX_UNIFORM_COMPONENTS       = 0x8B4A;
        public const uint GL_MAX_VARYING_FLOATS                  = 0x8B4B;
        public const uint GL_MAX_VERTEX_TEXTURE_IMAGE_UNITS      = 0x8B4C;
        public const uint GL_MAX_COMBINED_TEXTURE_IMAGE_UNITS    = 0x8B4D;
        public const uint GL_SHADER_TYPE                         = 0x8B4F;
        public const uint GL_FLOAT_VEC2                          = 0x8B50;
        public const uint GL_FLOAT_VEC3                          = 0x8B51;
        public const uint GL_FLOAT_VEC4                          = 0x8B52;
        public const uint GL_INT_VEC2                            = 0x8B53;
        public const uint GL_INT_VEC3                            = 0x8B54;
        public const uint GL_INT_VEC4                            = 0x8B55;
        public const uint GL_BOOL                                = 0x8B56;
        public const uint GL_BOOL_VEC2                           = 0x8B57;
        public const uint GL_BOOL_VEC3                           = 0x8B58;
        public const uint GL_BOOL_VEC4                           = 0x8B59;
        public const uint GL_FLOAT_MAT2                          = 0x8B5A;
        public const uint GL_FLOAT_MAT3                          = 0x8B5B;
        public const uint GL_FLOAT_MAT4                          = 0x8B5C;
        public const uint GL_SAMPLER_1D                          = 0x8B5D;
        public const uint GL_SAMPLER_2D                          = 0x8B5E;
        public const uint GL_SAMPLER_3D                          = 0x8B5F;
        public const uint GL_SAMPLER_CUBE                        = 0x8B60;
        public const uint GL_SAMPLER_1D_SHADOW                   = 0x8B61;
        public const uint GL_SAMPLER_2D_SHADOW                   = 0x8B62;
        public const uint GL_DELETE_STATUS                       = 0x8B80;
        public const uint GL_COMPILE_STATUS                      = 0x8B81;
        public const uint GL_LINK_STATUS                         = 0x8B82;
        public const uint GL_VALIDATE_STATUS                     = 0x8B83;
        public const uint GL_INFO_LOG_LENGTH                     = 0x8B84;
        public const uint GL_ATTACHED_SHADERS                    = 0x8B85;
        public const uint GL_ACTIVE_UNIFORMS                     = 0x8B86;
        public const uint GL_ACTIVE_UNIFORM_MAX_LENGTH           = 0x8B87;
        public const uint GL_SHADER_SOURCE_LENGTH                = 0x8B88;
        public const uint GL_ACTIVE_ATTRIBUTES                   = 0x8B89;
        public const uint GL_ACTIVE_ATTRIBUTE_MAX_LENGTH         = 0x8B8A;
        public const uint GL_FRAGMENT_SHADER_DERIVATIVE_HINT     = 0x8B8B;
        public const uint GL_SHADING_LANGUAGE_VERSION            = 0x8B8C;
        public const uint GL_CURRENT_PROGRAM                     = 0x8B8D;
        public const uint GL_POINT_SPRITE_COORD_ORIGIN           = 0x8CA0;
        public const uint GL_LOWER_LEFT                          = 0x8CA1;
        public const uint GL_UPPER_LEFT                          = 0x8CA2;
        public const uint GL_STENCIL_BACK_REF                    = 0x8CA3;
        public const uint GL_STENCIL_BACK_VALUE_MASK             = 0x8CA4;
        public const uint GL_STENCIL_BACK_WRITEMASK              = 0x8CA5;
        
        #endregion

        #region OpenGL 2.1

        //  Methods
        public void UniformMatrix2x3(int location, int count, bool transpose, float[] value)
        {
            getDelegateFor<glUniformMatrix2x3fv>(ref glUniformMatrix2x3fvDelegate)(location, count, transpose, value);
        }
        public void UniformMatrix3x2(int location, int count, bool transpose, float[] value)
        {
            getDelegateFor<glUniformMatrix3x2fv>(ref glUniformMatrix3x2fvDelegate)(location, count, transpose, value);
        }
        public void UniformMatrix2x4(int location, int count, bool transpose, float[] value)
        {
            getDelegateFor<glUniformMatrix2x4fv>(ref glUniformMatrix2x4fvDelegate)(location, count, transpose, value);
        }
        public void UniformMatrix4x2(int location, int count, bool transpose, float[] value)
        {
            getDelegateFor<glUniformMatrix4x2fv>(ref glUniformMatrix4x2fvDelegate)(location, count, transpose, value);
        }
        public void UniformMatrix3x4(int location, int count, bool transpose, float[] value)
        {
            getDelegateFor<glUniformMatrix3x4fv>(ref glUniformMatrix3x4fvDelegate)(location, count, transpose, value);
        }
        public void UniformMatrix4x3(int location, int count, bool transpose, float[] value)
        {
            getDelegateFor<glUniformMatrix4x3fv>(ref glUniformMatrix4x3fvDelegate)(location, count, transpose, value);
        }

        //  Delegates
        private delegate void glUniformMatrix2x3fv(int location, int count, bool transpose, float[] value);
		private Delegate glUniformMatrix2x3fvDelegate;
        private delegate void glUniformMatrix3x2fv(int location, int count, bool transpose, float[] value);
		private Delegate glUniformMatrix3x2fvDelegate;
        private delegate void glUniformMatrix2x4fv(int location, int count, bool transpose, float[] value);
		private Delegate glUniformMatrix2x4fvDelegate;
        private delegate void glUniformMatrix4x2fv(int location, int count, bool transpose, float[] value);
		private Delegate glUniformMatrix4x2fvDelegate;
        private delegate void glUniformMatrix3x4fv(int location, int count, bool transpose, float[] value);
		private Delegate glUniformMatrix3x4fvDelegate;
        private delegate void glUniformMatrix4x3fv(int location, int count, bool transpose, float[] value);
		private Delegate glUniformMatrix4x3fvDelegate;

        //  Constants
        public const uint GL_PIXEL_PACK_BUFFER                   = 0x88EB;
        public const uint GL_PIXEL_UNPACK_BUFFER                 = 0x88EC;
        public const uint GL_PIXEL_PACK_BUFFER_BINDING           = 0x88ED;
        public const uint GL_PIXEL_UNPACK_BUFFER_BINDING         = 0x88EF;
        public const uint GL_FLOAT_MAT2x3                        = 0x8B65;
        public const uint GL_FLOAT_MAT2x4                        = 0x8B66;
        public const uint GL_FLOAT_MAT3x2                        = 0x8B67;
        public const uint GL_FLOAT_MAT3x4                        = 0x8B68;
        public const uint GL_FLOAT_MAT4x2                        = 0x8B69;
        public const uint GL_FLOAT_MAT4x3                        = 0x8B6A;
        public const uint GL_SRGB                                = 0x8C40;
        public const uint GL_SRGB8                               = 0x8C41;
        public const uint GL_SRGB_ALPHA                          = 0x8C42;
        public const uint GL_SRGB8_ALPHA8                        = 0x8C43;
        public const uint GL_COMPRESSED_SRGB                     = 0x8C48;
        public const uint GL_COMPRESSED_SRGB_ALPHA               = 0x8C49;
       
        #endregion

        #region OpenGL 3.0
        
        //  Methods
        public void ColorMask(uint index, bool r, bool g, bool b, bool a)
        {
            getDelegateFor<glColorMaski>(ref glColorMaskiDelegate)(index, r, g, b, a);
        }
        public void GetBoolean(uint target, uint index, bool[] data)
        {
            getDelegateFor<glGetBooleani_v>(ref glGetBooleani_vDelegate)(target, index, data);
        }
        public void GetInteger(uint target, uint index, int[] data)
        {
            getDelegateFor<glGetIntegeri_v>(ref glGetIntegeri_vDelegate)(target, index, data);
        }
        public void Enable(uint target, uint index)
        {
            getDelegateFor<glEnablei>(ref glEnableiDelegate)(target, index);
        }
        public void Disable(uint target, uint index)
        {
            getDelegateFor<glDisablei>(ref glDisableiDelegate)(target, index);
        }
        public bool IsEnabled(uint target, uint index)
        {
            return(bool)getDelegateFor<glIsEnabledi>(ref glIsEnablediDelegate)(target, index);
        }
        public void BeginTransformFeedback(uint primitiveMode)
        {
            getDelegateFor<glBeginTransformFeedback>(ref glBeginTransformFeedbackDelegate)(primitiveMode);
        }
        public void EndTransformFeedback()
        {
            getDelegateFor<glEndTransformFeedback>(ref glEndTransformFeedbackDelegate)();
        }
        public void BindBufferRange(uint target, uint index, uint buffer, int offset, int size)
        {
            getDelegateFor<glBindBufferRange>(ref glBindBufferRangeDelegate)(target, index, buffer, offset, size);
        }
        public void BindBufferBase(uint target, uint index, uint buffer)
        {
            getDelegateFor<glBindBufferBase>(ref glBindBufferBaseDelegate)(target, index, buffer);
        }
        public void TransformFeedbackVaryings(uint program, int count, string[] varyings, uint bufferMode)
        {
            getDelegateFor<glTransformFeedbackVaryings>(ref glTransformFeedbackVaryingsDelegate)(program, count, varyings, bufferMode);
        }
        public void GetTransformFeedbackVarying(uint program, uint index, int bufSize, int[] length, int[] size, uint[] type, string name)
        {
            getDelegateFor<glGetTransformFeedbackVarying>(ref glGetTransformFeedbackVaryingDelegate)(program, index, bufSize, length, size, type, name);
        }
        public void ClampColor(uint target, uint clamp)
        {
            getDelegateFor<glClampColor>(ref glClampColorDelegate)(target, clamp);
        }
        public void BeginConditionalRender(uint id, uint mode)
        {
            getDelegateFor<glBeginConditionalRender>(ref glBeginConditionalRenderDelegate)(id, mode);
        }
        public void EndConditionalRender()
        {
            getDelegateFor<glEndConditionalRender>(ref glEndConditionalRenderDelegate)();
        }
        public void VertexAttribIPointer(uint index, int size, uint type, int stride, IntPtr pointer)
        {
            getDelegateFor<glVertexAttribIPointer>(ref glVertexAttribIPointerDelegate)(index, size, type, stride, pointer);
        }
        public void GetVertexAttribI(uint index, uint pname, int[] parameters)
        {
            getDelegateFor<glGetVertexAttribIiv>(ref glGetVertexAttribIivDelegate)(index, pname, parameters);
        }
        public void GetVertexAttribI(uint index, uint pname, uint[] parameters)
        {
            getDelegateFor<glGetVertexAttribIuiv>(ref glGetVertexAttribIuivDelegate)(index, pname, parameters);
        }
        public void VertexAttribI1(uint index, int x)
        {
            getDelegateFor<glVertexAttribI1i>(ref glVertexAttribI1iDelegate)(index, x);
        }
        public void VertexAttribI2(uint index, int x, int y)
        {
            getDelegateFor<glVertexAttribI2i>(ref glVertexAttribI2iDelegate)(index, x, y);
        }
        public void VertexAttribI3(uint index, int x, int y, int z)
        {
            getDelegateFor<glVertexAttribI3i>(ref glVertexAttribI3iDelegate)(index, x, y, z);
        }
        public void VertexAttribI4(uint index, int x, int y, int z, int w)
        {
            getDelegateFor<glVertexAttribI4i>(ref glVertexAttribI4iDelegate)(index, x, y, z, w);
        }
        public void VertexAttribI1(uint index, uint x)
        {
            getDelegateFor<glVertexAttribI1ui>(ref glVertexAttribI1uiDelegate)(index, x);
        }
        public void VertexAttribI2(uint index, uint x, uint y)
        {
            getDelegateFor<glVertexAttribI2ui>(ref glVertexAttribI2uiDelegate)(index, x, y);
        }
        public void VertexAttribI3(uint index, uint x, uint y, uint z)
        {
            getDelegateFor<glVertexAttribI3ui>(ref glVertexAttribI3uiDelegate)(index, x, y, z);
        }
        public void VertexAttribI4(uint index, uint x, uint y, uint z, uint w)
        {
            getDelegateFor<glVertexAttribI4ui>(ref glVertexAttribI4uiDelegate)(index, x, y, z, w);
        }
        public void VertexAttribI1(uint index, int[] v)
        {
            getDelegateFor<glVertexAttribI1iv>(ref glVertexAttribI1ivDelegate)(index, v);
        }
        public void VertexAttribI2(uint index, int[] v)
        {
            getDelegateFor<glVertexAttribI2iv>(ref glVertexAttribI2ivDelegate)(index, v);
        }
        public void VertexAttribI3(uint index, int[] v)
        {
            getDelegateFor<glVertexAttribI3iv>(ref glVertexAttribI3ivDelegate)(index, v);
        }
        public void VertexAttribI4(uint index, int[] v)
        {
            getDelegateFor<glVertexAttribI4iv>(ref glVertexAttribI4ivDelegate)(index, v);
        }
        public void VertexAttribI1(uint index, uint[] v)
        {
            getDelegateFor<glVertexAttribI1uiv>(ref glVertexAttribI1uivDelegate)(index, v);
        }
        public void VertexAttribI2(uint index, uint[] v)
        {
            getDelegateFor<glVertexAttribI2uiv>(ref glVertexAttribI2uivDelegate)(index, v);
        }
        public void VertexAttribI3(uint index, uint[] v)
        {
            getDelegateFor<glVertexAttribI3uiv>(ref glVertexAttribI3uivDelegate)(index, v);
        }
        public void VertexAttribI4(uint index, uint[] v)
        {
            getDelegateFor<glVertexAttribI4uiv>(ref glVertexAttribI4uivDelegate)(index, v);
        }
        public void VertexAttribI4(uint index, sbyte[] v)
        {
            getDelegateFor<glVertexAttribI4bv>(ref glVertexAttribI4bvDelegate)(index, v);
        }
        public void VertexAttribI4(uint index, short[] v)
        {
            getDelegateFor<glVertexAttribI4sv>(ref glVertexAttribI4svDelegate)(index, v);
        }
        public void VertexAttribI4(uint index, byte[] v)
        {
            getDelegateFor<glVertexAttribI4ubv>(ref glVertexAttribI4ubvDelegate)(index, v);
        }
        public void VertexAttribI4(uint index, ushort[] v)
        {
            getDelegateFor<glVertexAttribI4usv>(ref glVertexAttribI4usvDelegate)(index, v);
        }
        public void GetUniform(uint program, int location, uint[] parameters)
        {
            getDelegateFor<glGetUniformuiv>(ref glGetUniformuivDelegate)(program, location, parameters);
        }
        public void BindFragDataLocation(uint program, uint color, string name)
        {
            getDelegateFor<glBindFragDataLocation>(ref glBindFragDataLocationDelegate)(program, color, name);
        }
        public int GetFragDataLocation(uint program, string name)
        {
            return(int)getDelegateFor<glGetFragDataLocation>(ref glGetFragDataLocationDelegate)(program, name);
        }
        public void Uniform1(int location, uint v0)
        {
            getDelegateFor<glUniform1ui>(ref glUniform1uiDelegate)(location, v0);
        }
        public void Uniform2(int location, uint v0, uint v1)
        {
            getDelegateFor<glUniform2ui>(ref glUniform2uiDelegate)(location, v0, v1);
        }
        public void Uniform3(int location, uint v0, uint v1, uint v2)
        {
            getDelegateFor<glUniform3ui>(ref glUniform3uiDelegate)(location, v0, v1, v2);
        }
        public void Uniform4(int location, uint v0, uint v1, uint v2, uint v3)
        {
            getDelegateFor<glUniform4ui>(ref glUniform4uiDelegate)(location, v0, v1, v2, v3);
        }
        public void Uniform1(int location, int count, uint[] value)
        {
            getDelegateFor<glUniform1uiv>(ref glUniform1uivDelegate)(location, count, value);
        }
        public void Uniform2(int location, int count, uint[] value)
        {
            getDelegateFor<glUniform2uiv>(ref glUniform2uivDelegate)(location, count, value);
        }
        public void Uniform3(int location, int count, uint[] value)
        {
            getDelegateFor<glUniform3uiv>(ref glUniform3uivDelegate)(location, count, value);
        }
        public void Uniform4(int location, int count, uint[] value)
        {
            getDelegateFor<glUniform4uiv>(ref glUniform4uivDelegate)(location, count, value);
        }
        public void TexParameterI(uint target, uint pname, int[] parameters)
        {
            getDelegateFor<glTexParameterIiv>(ref glTexParameterIivDelegate)(target, pname, parameters);
        }
        public void TexParameterI(uint target, uint pname, uint[] parameters)
        {
            getDelegateFor<glTexParameterIuiv>(ref glTexParameterIuivDelegate)(target, pname, parameters);
        }
        public void GetTexParameterI(uint target, uint pname, int[] parameters)
        {
            getDelegateFor<glGetTexParameterIiv>(ref glGetTexParameterIivDelegate)(target, pname, parameters);
        }
        public void GetTexParameterI(uint target, uint pname, uint[] parameters)
        {
            getDelegateFor<glGetTexParameterIuiv>(ref glGetTexParameterIuivDelegate)(target, pname, parameters);
        }
        public void ClearBuffer(uint buffer, int drawbuffer, int[] value)
        {
            getDelegateFor<glClearBufferiv>(ref glClearBufferivDelegate)(buffer, drawbuffer, value);
        }
        public void ClearBuffer(uint buffer, int drawbuffer, uint[] value)
        {
            getDelegateFor<glClearBufferuiv>(ref glClearBufferuivDelegate)(buffer, drawbuffer, value);
        }
        public void ClearBuffer(uint buffer, int drawbuffer, float[] value)
        {
            getDelegateFor<glClearBufferfv>(ref glClearBufferfvDelegate)(buffer, drawbuffer, value);
        }
        public void ClearBuffer(uint buffer, int drawbuffer, float depth, int stencil)
        {
            getDelegateFor<glClearBufferfi>(ref glClearBufferfiDelegate)(buffer, drawbuffer, depth, stencil);
        }
        public unsafe string GetString(uint name, uint index)
        {
            var chars = getDelegateFor<glGetStringi>(ref glGetStringiDelegate)(name, index);
			return chars == IntPtr.Zero ? null : Marshal.PtrToStringAnsi(chars);
        }

        //  Delegates
        private delegate void glColorMaski(uint index, bool r, bool g, bool b, bool a);
		private Delegate glColorMaskiDelegate;
        private delegate void glGetBooleani_v(uint target, uint index, bool[] data);
		private Delegate glGetBooleani_vDelegate;
        private delegate void glGetIntegeri_v(uint target, uint index, int[] data);
		private Delegate glGetIntegeri_vDelegate;
        private delegate void glEnablei(uint target, uint index);
		private Delegate glEnableiDelegate;
        private delegate void glDisablei(uint target, uint index);
		private Delegate glDisableiDelegate;
        private delegate bool glIsEnabledi(uint target, uint index);
		private Delegate glIsEnablediDelegate;
        private delegate void glBeginTransformFeedback(uint primitiveMode);
		private Delegate glBeginTransformFeedbackDelegate;
        private delegate void glEndTransformFeedback();
		private Delegate glEndTransformFeedbackDelegate;
        private delegate void glBindBufferRange(uint target, uint index, uint buffer, int offset, int size);
		private Delegate glBindBufferRangeDelegate;
        private delegate void glBindBufferBase(uint target, uint index, uint buffer);
		private Delegate glBindBufferBaseDelegate;
        private delegate void glTransformFeedbackVaryings(uint program, int count, string[] varyings, uint bufferMode);
		private Delegate glTransformFeedbackVaryingsDelegate;
        private delegate void glGetTransformFeedbackVarying(uint program, uint index, int bufSize, int[] length, int[] size, uint[] type, string name);
		private Delegate glGetTransformFeedbackVaryingDelegate;
        private delegate void glClampColor(uint target, uint clamp);
		private Delegate glClampColorDelegate;
        private delegate void glBeginConditionalRender(uint id, uint mode);
		private Delegate glBeginConditionalRenderDelegate;
        private delegate void glEndConditionalRender();
		private Delegate glEndConditionalRenderDelegate;
        private delegate void glVertexAttribIPointer(uint index, int size, uint type, int stride, IntPtr pointer);
		private Delegate glVertexAttribIPointerDelegate;
        private delegate void glGetVertexAttribIiv(uint index, uint pname, int[] parameters);
		private Delegate glGetVertexAttribIivDelegate;
        private delegate void glGetVertexAttribIuiv(uint index, uint pname, uint[] parameters);
		private Delegate glGetVertexAttribIuivDelegate;
        private delegate void glVertexAttribI1i(uint index, int x);
		private Delegate glVertexAttribI1iDelegate;
        private delegate void glVertexAttribI2i(uint index, int x, int y);
		private Delegate glVertexAttribI2iDelegate;
        private delegate void glVertexAttribI3i(uint index, int x, int y, int z);
		private Delegate glVertexAttribI3iDelegate;
        private delegate void glVertexAttribI4i(uint index, int x, int y, int z, int w);
		private Delegate glVertexAttribI4iDelegate;
        private delegate void glVertexAttribI1ui(uint index, uint x);
		private Delegate glVertexAttribI1uiDelegate;
        private delegate void glVertexAttribI2ui(uint index, uint x, uint y);
		private Delegate glVertexAttribI2uiDelegate;
        private delegate void glVertexAttribI3ui(uint index, uint x, uint y, uint z);
		private Delegate glVertexAttribI3uiDelegate;
        private delegate void glVertexAttribI4ui(uint index, uint x, uint y, uint z, uint w);
		private Delegate glVertexAttribI4uiDelegate;
        private delegate void glVertexAttribI1iv(uint index, int[] v);
		private Delegate glVertexAttribI1ivDelegate;
        private delegate void glVertexAttribI2iv(uint index, int[] v);
		private Delegate glVertexAttribI2ivDelegate;
        private delegate void glVertexAttribI3iv(uint index, int[] v);
		private Delegate glVertexAttribI3ivDelegate;
        private delegate void glVertexAttribI4iv(uint index, int[] v);
		private Delegate glVertexAttribI4ivDelegate;
        private delegate void glVertexAttribI1uiv(uint index, uint[] v);
		private Delegate glVertexAttribI1uivDelegate;
        private delegate void glVertexAttribI2uiv(uint index, uint[] v);
		private Delegate glVertexAttribI2uivDelegate;
        private delegate void glVertexAttribI3uiv(uint index, uint[] v);
		private Delegate glVertexAttribI3uivDelegate;
        private delegate void glVertexAttribI4uiv(uint index, uint[] v);
		private Delegate glVertexAttribI4uivDelegate;
        private delegate void glVertexAttribI4bv(uint index, sbyte[] v);
		private Delegate glVertexAttribI4bvDelegate;
        private delegate void glVertexAttribI4sv(uint index, short[] v);
		private Delegate glVertexAttribI4svDelegate;
        private delegate void glVertexAttribI4ubv(uint index, byte[] v);
		private Delegate glVertexAttribI4ubvDelegate;
        private delegate void glVertexAttribI4usv(uint index, ushort[] v);
		private Delegate glVertexAttribI4usvDelegate;
        private delegate void glGetUniformuiv(uint program, int location, uint[] parameters);
		private Delegate glGetUniformuivDelegate;
        private delegate void glBindFragDataLocation(uint program, uint color, string name);
		private Delegate glBindFragDataLocationDelegate;
        private delegate int glGetFragDataLocation(uint program, string name);
		private Delegate glGetFragDataLocationDelegate;
        private delegate void glUniform1ui(int location, uint v0);
		private Delegate glUniform1uiDelegate;
        private delegate void glUniform2ui(int location, uint v0, uint v1);
		private Delegate glUniform2uiDelegate;
        private delegate void glUniform3ui(int location, uint v0, uint v1, uint v2);
		private Delegate glUniform3uiDelegate;
        private delegate void glUniform4ui(int location, uint v0, uint v1, uint v2, uint v3);
		private Delegate glUniform4uiDelegate;
        private delegate void glUniform1uiv(int location, int count, uint[] value);
		private Delegate glUniform1uivDelegate;
        private delegate void glUniform2uiv(int location, int count, uint[] value);
		private Delegate glUniform2uivDelegate;
        private delegate void glUniform3uiv(int location, int count, uint[] value);
		private Delegate glUniform3uivDelegate;
        private delegate void glUniform4uiv(int location, int count, uint[] value);
		private Delegate glUniform4uivDelegate;
        private delegate void glTexParameterIiv(uint target, uint pname, int[] parameters);
		private Delegate glTexParameterIivDelegate;
        private delegate void glTexParameterIuiv(uint target, uint pname, uint[] parameters);
		private Delegate glTexParameterIuivDelegate;
        private delegate void glGetTexParameterIiv(uint target, uint pname, int[] parameters);
		private Delegate glGetTexParameterIivDelegate;
        private delegate void glGetTexParameterIuiv(uint target, uint pname, uint[] parameters);
		private Delegate glGetTexParameterIuivDelegate;
        private delegate void glClearBufferiv(uint buffer, int drawbuffer, int[] value);
		private Delegate glClearBufferivDelegate;
        private delegate void glClearBufferuiv(uint buffer, int drawbuffer, uint[] value);
		private Delegate glClearBufferuivDelegate;
        private delegate void glClearBufferfv(uint buffer, int drawbuffer, float[] value);
		private Delegate glClearBufferfvDelegate;
        private delegate void glClearBufferfi(uint buffer, int drawbuffer, float depth, int stencil);
		private Delegate glClearBufferfiDelegate;
        private delegate IntPtr glGetStringi(uint name, uint index);
		private Delegate glGetStringiDelegate;

        //  Constants
        public const uint GL_COMPARE_REF_TO_TEXTURE                        = 0x884E;
        public const uint GL_CLIP_DISTANCE0                                = 0x3000;
        public const uint GL_CLIP_DISTANCE1                                = 0x3001;
        public const uint GL_CLIP_DISTANCE2                                = 0x3002;
        public const uint GL_CLIP_DISTANCE3                                = 0x3003;
        public const uint GL_CLIP_DISTANCE4                                = 0x3004;
        public const uint GL_CLIP_DISTANCE5                                = 0x3005;
        public const uint GL_CLIP_DISTANCE6                                = 0x3006;
        public const uint GL_CLIP_DISTANCE7                                = 0x3007;
        public const uint GL_MAX_CLIP_DISTANCES                            = 0x0D32;
        public const uint GL_MAJOR_VERSION                                 = 0x821B;
        public const uint GL_MINOR_VERSION                                 = 0x821C;
        public const uint GL_NUM_EXTENSIONS                                = 0x821D;
        public const uint GL_CONTEXT_FLAGS                                 = 0x821E;
        public const uint GL_DEPTH_BUFFER                                  = 0x8223;
        public const uint GL_STENCIL_BUFFER                                = 0x8224;
        public const uint GL_COMPRESSED_RED                                = 0x8225;
        public const uint GL_COMPRESSED_RG                                 = 0x8226;
        public const uint GL_CONTEXT_FLAG_FORWARD_COMPATIBLE_BIT           = 0x0001;
        public const uint GL_RGBA32F                                       = 0x8814;
        public const uint GL_RGB32F                                        = 0x8815;
        public const uint GL_RGBA16F                                       = 0x881A;
        public const uint GL_RGB16F                                        = 0x881B;
        public const uint GL_VERTEX_ATTRIB_ARRAY_INTEGER                   = 0x88FD;
        public const uint GL_MAX_ARRAY_TEXTURE_LAYERS                      = 0x88FF;
        public const uint GL_MIN_PROGRAM_TEXEL_OFFSET                      = 0x8904;
        public const uint GL_MAX_PROGRAM_TEXEL_OFFSET                      = 0x8905;
        public const uint GL_CLAMP_READ_COLOR                              = 0x891C;
        public const uint GL_FIXED_ONLY                                    = 0x891D;
        public const uint GL_MAX_VARYING_COMPONENTS                        = 0x8B4B;
        public const uint GL_TEXTURE_1D_ARRAY                              = 0x8C18;
        public const uint GL_PROXY_TEXTURE_1D_ARRAY                        = 0x8C19;
        public const uint GL_TEXTURE_2D_ARRAY                              = 0x8C1A;
        public const uint GL_PROXY_TEXTURE_2D_ARRAY                        = 0x8C1B;
        public const uint GL_TEXTURE_BINDING_1D_ARRAY                      = 0x8C1C;
        public const uint GL_TEXTURE_BINDING_2D_ARRAY                      = 0x8C1D;
        public const uint GL_R11F_G11F_B10F                                = 0x8C3A;
        public const uint GL_UNSIGNED_INT_10F_11F_11F_REV                  = 0x8C3B;
        public const uint GL_RGB9_E5                                       = 0x8C3D;
        public const uint GL_UNSIGNED_INT_5_9_9_9_REV                      = 0x8C3E;
        public const uint GL_TEXTURE_SHARED_SIZE                           = 0x8C3F;
        public const uint GL_TRANSFORM_FEEDBACK_VARYING_MAX_LENGTH         = 0x8C76;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_MODE                = 0x8C7F;
        public const uint GL_MAX_TRANSFORM_FEEDBACK_SEPARATE_COMPONENTS    = 0x8C80;
        public const uint GL_TRANSFORM_FEEDBACK_VARYINGS                   = 0x8C83;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_START               = 0x8C84;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_SIZE                = 0x8C85;
        public const uint GL_PRIMITIVES_GENERATED                          = 0x8C87;
        public const uint GL_TRANSFORM_FEEDBACK_PRIMITIVES_WRITTEN         = 0x8C88;
        public const uint GL_RASTERIZER_DISCARD                            = 0x8C89;
        public const uint GL_MAX_TRANSFORM_FEEDBACK_INTERLEAVED_COMPONENTS = 0x8C8A;
        public const uint GL_MAX_TRANSFORM_FEEDBACK_SEPARATE_ATTRIBS       = 0x8C8B;
        public const uint GL_INTERLEAVED_ATTRIBS                           = 0x8C8C;
        public const uint GL_SEPARATE_ATTRIBS                              = 0x8C8D;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER                     = 0x8C8E;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_BINDING             = 0x8C8F;
        public const uint GL_RGBA32UI                                      = 0x8D70;
        public const uint GL_RGB32UI                                       = 0x8D71;
        public const uint GL_RGBA16UI                                      = 0x8D76;
        public const uint GL_RGB16UI                                       = 0x8D77;
        public const uint GL_RGBA8UI                                       = 0x8D7C;
        public const uint GL_RGB8UI                                        = 0x8D7D;
        public const uint GL_RGBA32I                                       = 0x8D82;
        public const uint GL_RGB32I                                        = 0x8D83;
        public const uint GL_RGBA16I                                       = 0x8D88;
        public const uint GL_RGB16I                                        = 0x8D89;
        public const uint GL_RGBA8I                                        = 0x8D8E;
        public const uint GL_RGB8I                                         = 0x8D8F;
        public const uint GL_RED_INTEGER                                   = 0x8D94;
        public const uint GL_GREEN_INTEGER                                 = 0x8D95;
        public const uint GL_BLUE_INTEGER                                  = 0x8D96;
        public const uint GL_RGB_INTEGER                                   = 0x8D98;
        public const uint GL_RGBA_INTEGER                                  = 0x8D99;
        public const uint GL_BGR_INTEGER                                   = 0x8D9A;
        public const uint GL_BGRA_INTEGER                                  = 0x8D9B;
        public const uint GL_SAMPLER_1D_ARRAY                              = 0x8DC0;
        public const uint GL_SAMPLER_2D_ARRAY                              = 0x8DC1;
        public const uint GL_SAMPLER_1D_ARRAY_SHADOW                       = 0x8DC3;
        public const uint GL_SAMPLER_2D_ARRAY_SHADOW                       = 0x8DC4;
        public const uint GL_SAMPLER_CUBE_SHADOW                           = 0x8DC5;
        public const uint GL_UNSIGNED_INT_VEC2                             = 0x8DC6;
        public const uint GL_UNSIGNED_INT_VEC3                             = 0x8DC7;
        public const uint GL_UNSIGNED_INT_VEC4                             = 0x8DC8;
        public const uint GL_INT_SAMPLER_1D                                = 0x8DC9;
        public const uint GL_INT_SAMPLER_2D                                = 0x8DCA;
        public const uint GL_INT_SAMPLER_3D                                = 0x8DCB;
        public const uint GL_INT_SAMPLER_CUBE                              = 0x8DCC;
        public const uint GL_INT_SAMPLER_1D_ARRAY                          = 0x8DCE;
        public const uint GL_INT_SAMPLER_2D_ARRAY                          = 0x8DCF;
        public const uint GL_UNSIGNED_INT_SAMPLER_1D                       = 0x8DD1;
        public const uint GL_UNSIGNED_INT_SAMPLER_2D                       = 0x8DD2;
        public const uint GL_UNSIGNED_INT_SAMPLER_3D                       = 0x8DD3;
        public const uint GL_UNSIGNED_INT_SAMPLER_CUBE                     = 0x8DD4;
        public const uint GL_UNSIGNED_INT_SAMPLER_1D_ARRAY                 = 0x8DD6;
        public const uint GL_UNSIGNED_INT_SAMPLER_2D_ARRAY                 = 0x8DD7;
        public const uint GL_QUERY_WAIT                                    = 0x8E13;
        public const uint GL_QUERY_NO_WAIT                                 = 0x8E14;
        public const uint GL_QUERY_BY_REGION_WAIT                          = 0x8E15;
        public const uint GL_QUERY_BY_REGION_NO_WAIT                       = 0x8E16;
        public const uint GL_BUFFER_ACCESS_FLAGS                           = 0x911F;
        public const uint GL_BUFFER_MAP_LENGTH                             = 0x9120;
        public const uint GL_BUFFER_MAP_OFFSET                             = 0x9121;
        public const uint GL_R8 = 0x8229;
        public const uint GL_R16 = 0x822A;
        public const uint GL_RG8 = 0x822B;
        public const uint GL_RG16 = 0x822C;
        public const uint GL_R16F = 0x822D;
        public const uint GL_R32F = 0x822E;
        public const uint GL_RG16F = 0x822F;
        public const uint GL_RG32F = 0x8230;
        public const uint GL_R8I = 0x8231;
        public const uint GL_R8UI = 0x8232;
        public const uint GL_R16I = 0x8233;
        public const uint GL_R16UI = 0x8234;
        public const uint GL_R32I = 0x8235;
        public const uint GL_R32UI = 0x8236;
        public const uint GL_RG8I = 0x8237;
        public const uint GL_RG8UI = 0x8238;
        public const uint GL_RG16I = 0x8239;
        public const uint GL_RG16UI = 0x823A;
        public const uint GL_RG32I = 0x823B;
        public const uint GL_RG32UI = 0x823C;
        public const uint GL_RG = 0x8227;
        public const uint GL_RG_INTEGER = 0x8228;
     
        #endregion

        #region OpenGL 3.1

        //  Methods
        public void DrawArraysInstanced(uint mode, int first, int count, int primcount)
        {
            getDelegateFor<glDrawArraysInstanced>(ref glDrawArraysInstancedDelegate)(mode, first, count, primcount);
        }
        public void DrawElementsInstanced(uint mode, int count, uint type, IntPtr indices, int primcount)
        {
            getDelegateFor<glDrawElementsInstanced>(ref glDrawElementsInstancedDelegate)(mode, count, type, indices, primcount);
        }
        public void TexBuffer(uint target, uint internalformat, uint buffer)
        {
            getDelegateFor<glTexBuffer>(ref glTexBufferDelegate)(target, internalformat, buffer);
        }
        public void PrimitiveRestartIndex(uint index)
        {
            getDelegateFor<glPrimitiveRestartIndex>(ref glPrimitiveRestartIndexDelegate)(index);
        }

        //  Delegates
        private delegate void glDrawArraysInstanced(uint mode, int first, int count, int primcount);
		private Delegate glDrawArraysInstancedDelegate;
        private delegate void glDrawElementsInstanced(uint mode, int count, uint type, IntPtr indices, int primcount);
		private Delegate glDrawElementsInstancedDelegate;
        private delegate void glTexBuffer(uint target, uint internalformat, uint buffer);
		private Delegate glTexBufferDelegate;
        private delegate void glPrimitiveRestartIndex(uint index);
		private Delegate glPrimitiveRestartIndexDelegate;

        //  Constants
        public const uint GL_SAMPLER_2D_RECT                       = 0x8B63;
        public const uint GL_SAMPLER_2D_RECT_SHADOW                = 0x8B64;
        public const uint GL_SAMPLER_BUFFER                        = 0x8DC2;
        public const uint GL_INT_SAMPLER_2D_RECT                   = 0x8DCD;
        public const uint GL_INT_SAMPLER_BUFFER                    = 0x8DD0;
        public const uint GL_UNSIGNED_INT_SAMPLER_2D_RECT          = 0x8DD5;
        public const uint GL_UNSIGNED_INT_SAMPLER_BUFFER           = 0x8DD8;
        public const uint GL_TEXTURE_BUFFER                        = 0x8C2A;
        public const uint GL_MAX_TEXTURE_BUFFER_SIZE               = 0x8C2B;
        public const uint GL_TEXTURE_BINDING_BUFFER                = 0x8C2C;
        public const uint GL_TEXTURE_BUFFER_DATA_STORE_BINDING     = 0x8C2D;
        public const uint GL_TEXTURE_BUFFER_FORMAT                 = 0x8C2E;
        public const uint GL_TEXTURE_RECTANGLE                     = 0x84F5;
        public const uint GL_TEXTURE_BINDING_RECTANGLE             = 0x84F6;
        public const uint GL_PROXY_TEXTURE_RECTANGLE               = 0x84F7;
        public const uint GL_MAX_RECTANGLE_TEXTURE_SIZE            = 0x84F8;
        public const uint GL_RED_SNORM                             = 0x8F90;
        public const uint GL_RG_SNORM                              = 0x8F91;
        public const uint GL_RGB_SNORM                             = 0x8F92;
        public const uint GL_RGBA_SNORM                            = 0x8F93;
        public const uint GL_R8_SNORM                              = 0x8F94;
        public const uint GL_RG8_SNORM                             = 0x8F95;
        public const uint GL_RGB8_SNORM                            = 0x8F96;
        public const uint GL_RGBA8_SNORM                           = 0x8F97;
        public const uint GL_R16_SNORM                             = 0x8F98;
        public const uint GL_RG16_SNORM                            = 0x8F99;
        public const uint GL_RGB16_SNORM                           = 0x8F9A;
        public const uint GL_RGBA16_SNORM                          = 0x8F9B;
        public const uint GL_SIGNED_NORMALIZED                     = 0x8F9C;
        public const uint GL_PRIMITIVE_RESTART                     = 0x8F9D;
        public const uint GL_PRIMITIVE_RESTART_INDEX               = 0x8F9E;
        
        #endregion

        #region OpenGL 3.2

        //  Methods
        public void GetInteger64(uint target, uint index, Int64[] data)
        {
            getDelegateFor<glGetInteger64i_v>(ref glGetInteger64i_vDelegate)(target, index, data);
        }
        public void GetBufferParameteri64(uint target, uint pname, Int64[] parameters)
        {
            getDelegateFor<glGetBufferParameteri64v>(ref glGetBufferParameteri64vDelegate)(target, pname, parameters);
        }
        public void FramebufferTexture(uint target, uint attachment, uint texture, int level)
        {
            getDelegateFor<glFramebufferTexture>(ref glFramebufferTextureDelegate)(target, attachment, texture, level);
        }

        /// <summary>
        /// Render primitives from array data with a per-element offset.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render. Symbolic constants OpenGL.GL_POINTS, OpenGL.GL_LINE_STRIP, OpenGL.GL_LINE_LOOP, OpenGL.GL_LINES, OpenGL.GL_TRIANGLE_STRIP, OpenGL.GL_TRIANGLE_FAN, OpenGL.GL_TRIANGLES, OpenGL.GL_LINES_ADJACENCY, OpenGL.GL_LINE_STRIP_ADJACENCY, OpenGL.GL_TRIANGLES_ADJACENCY, OpenGL.GL_TRIANGLE_STRIP_ADJACENCY and OpenGL.GL_PATCHES are accepted.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="type">Specifies the type of the values in indices. Must be one of GL_UNSIGNED_BYTE, GL_UNSIGNED_SHORT, or GL_UNSIGNED_INT.</param>
        /// <param name="indices">Specifies a pointer to the location where the indices are stored.</param>
        /// <param name="basevertex">Specifies a constant that should be added to each element of indices when chosing elements from the enabled vertex arrays.</param>
        public void DrawElementsBaseVertex(uint mode, int count, uint type, IntPtr indices, int basevertex)
        {
            getDelegateFor<glDrawElementsBaseVertex>(ref glDrawElementsBaseVertexDelegate)(mode, count, type, indices, basevertex);
        }

        /// <summary>
        /// Render primitives from array data with a per-element offset. Uses OpenGL.GL_UNSIGNED_INT as the data type.
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render. Symbolic constants OpenGL.GL_POINTS, OpenGL.GL_LINE_STRIP, OpenGL.GL_LINE_LOOP, OpenGL.GL_LINES, OpenGL.GL_TRIANGLE_STRIP, OpenGL.GL_TRIANGLE_FAN, OpenGL.GL_TRIANGLES, OpenGL.GL_LINES_ADJACENCY, OpenGL.GL_LINE_STRIP_ADJACENCY, OpenGL.GL_TRIANGLES_ADJACENCY, OpenGL.GL_TRIANGLE_STRIP_ADJACENCY and OpenGL.GL_PATCHES are accepted.</param>
        /// <param name="count">Specifies the number of elements to be rendered.</param>
        /// <param name="indices">Specifies a pointer to the location where the indices are stored.</param>
        /// <param name="basevertex">Specifies a constant that should be added to each element of indices when chosing elements from the enabled vertex arrays.</param>
        public void DrawElementsBaseVertex(uint mode, int count, uint[] indices, int basevertex)
        {
            var pinned = GCHandle.Alloc(indices, GCHandleType.Pinned);
            getDelegateFor<glDrawElementsBaseVertex>(ref glDrawElementsBaseVertexDelegate)(mode, count, OpenGL.GL_UNSIGNED_INT, pinned.AddrOfPinnedObject(), basevertex);
            pinned.Free();
        }

        //  Delegates
        private delegate void glGetInteger64i_v(uint target, uint index, Int64[] data);
		private Delegate glGetInteger64i_vDelegate;
        private delegate void glGetBufferParameteri64v(uint target, uint pname, Int64[] parameters);
		private Delegate glGetBufferParameteri64vDelegate;
        private delegate void glFramebufferTexture(uint target, uint attachment, uint texture, int level);
		private Delegate glFramebufferTextureDelegate;
        private delegate void glDrawElementsBaseVertex(uint mode, int count, uint type, IntPtr indices, int basevertex);
		private Delegate glDrawElementsBaseVertexDelegate;

        //  Constants
        public const uint GL_CONTEXT_CORE_PROFILE_BIT                  = 0x00000001;
        public const uint GL_CONTEXT_COMPATIBILITY_PROFILE_BIT         = 0x00000002;
        public const uint GL_LINES_ADJACENCY                           = 0x000A;
        public const uint GL_LINE_STRIP_ADJACENCY                      = 0x000B;
        public const uint GL_TRIANGLES_ADJACENCY                       = 0x000C;
        public const uint GL_TRIANGLE_STRIP_ADJACENCY                  = 0x000D;
        public const uint GL_PATCHES                                   = 0x000E;
        public const uint GL_PROGRAM_POINT_SIZE                        = 0x8642;
        public const uint GL_MAX_GEOMETRY_TEXTURE_IMAGE_UNITS          = 0x8C29;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_LAYERED            = 0x8DA7;
        public const uint GL_FRAMEBUFFER_INCOMPLETE_LAYER_TARGETS      = 0x8DA8;
        public const uint GL_GEOMETRY_SHADER                           = 0x8DD9;
        public const uint GL_GEOMETRY_VERTICES_OUT                     = 0x8916;
        public const uint GL_GEOMETRY_INPUT_TYPE                       = 0x8917;
        public const uint GL_GEOMETRY_OUTPUT_TYPE                      = 0x8918;
        public const uint GL_MAX_GEOMETRY_UNIFORM_COMPONENTS           = 0x8DDF;
        public const uint GL_MAX_GEOMETRY_OUTPUT_VERTICES              = 0x8DE0;
        public const uint GL_MAX_GEOMETRY_TOTAL_OUTPUT_COMPONENTS      = 0x8DE1;
        public const uint GL_MAX_VERTEX_OUTPUT_COMPONENTS              = 0x9122;
        public const uint GL_MAX_GEOMETRY_INPUT_COMPONENTS             = 0x9123;
        public const uint GL_MAX_GEOMETRY_OUTPUT_COMPONENTS            = 0x9124;
        public const uint GL_MAX_FRAGMENT_INPUT_COMPONENTS             = 0x9125;
        public const uint GL_CONTEXT_PROFILE_MASK                      = 0x9126;

        #endregion

        #region OpenGL 3.3

        //  Methods
        public void VertexAttribDivisor(uint index, uint divisor)
        {
            getDelegateFor<glVertexAttribDivisor>(ref glVertexAttribDivisorDelegate)(index, divisor);
        }
        
        //  Delegates
        private delegate void glVertexAttribDivisor(uint index, uint divisor);
		private Delegate glVertexAttribDivisorDelegate;

        //  Constants
        public const uint GL_VERTEX_ATTRIB_ARRAY_DIVISOR             = 0x88FE;
        
        #endregion

        #region OpenGL 4.0

        //  Methods        
        public void MinSampleShading(float value)
        {
            getDelegateFor<glMinSampleShading>(ref glMinSampleShadingDelegate)(value);
        }
        public void BlendEquation(uint buf, uint mode)
        {
            getDelegateFor<glBlendEquationi>(ref glBlendEquationiDelegate)(buf, mode);
        }
        public void BlendEquationSeparate(uint buf, uint modeRGB, uint modeAlpha)
        {
            getDelegateFor<glBlendEquationSeparatei>(ref glBlendEquationSeparateiDelegate)(buf, modeRGB, modeAlpha);
        }
        public void BlendFunc(uint buf, uint src, uint dst)
        {
            getDelegateFor<glBlendFunci>(ref glBlendFunciDelegate)(buf, src, dst);
        }
        public void BlendFuncSeparate(uint buf, uint srcRGB, uint dstRGB, uint srcAlpha, uint dstAlpha)
        {
            getDelegateFor<glBlendFuncSeparatei>(ref glBlendFuncSeparateiDelegate)(buf, srcRGB, dstRGB, srcAlpha, dstAlpha);
        }

        //  Delegates        
        private delegate void glMinSampleShading(float value);
		private Delegate glMinSampleShadingDelegate;
        private delegate void glBlendEquationi(uint buf, uint mode);
		private Delegate glBlendEquationiDelegate;
        private delegate void glBlendEquationSeparatei(uint buf, uint modeRGB, uint modeAlpha);
		private Delegate glBlendEquationSeparateiDelegate;
        private delegate void glBlendFunci(uint buf, uint src, uint dst);
		private Delegate glBlendFunciDelegate;
        private delegate void glBlendFuncSeparatei(uint buf, uint srcRGB, uint dstRGB, uint srcAlpha, uint dstAlpha);
		private Delegate glBlendFuncSeparateiDelegate;

        //  Constants
        public const uint GL_SAMPLE_SHADING                        = 0x8C36;
        public const uint GL_MIN_SAMPLE_SHADING_VALUE              = 0x8C37;
        public const uint GL_MIN_PROGRAM_TEXTURE_GATHER_OFFSET     = 0x8E5E;
        public const uint GL_MAX_PROGRAM_TEXTURE_GATHER_OFFSET     = 0x8E5F;
        public const uint GL_TEXTURE_CUBE_MAP_ARRAY                = 0x9009;
        public const uint GL_TEXTURE_BINDING_CUBE_MAP_ARRAY        = 0x900A;
        public const uint GL_PROXY_TEXTURE_CUBE_MAP_ARRAY          = 0x900B;
        public const uint GL_SAMPLER_CUBE_MAP_ARRAY                = 0x900C;
        public const uint GL_SAMPLER_CUBE_MAP_ARRAY_SHADOW         = 0x900D;
        public const uint GL_INT_SAMPLER_CUBE_MAP_ARRAY            = 0x900E;
        public const uint GL_UNSIGNED_INT_SAMPLER_CUBE_MAP_ARRAY   = 0x900F;

        #endregion

        #region GL_EXT_texture3D

        /// <summary>
        /// Specify a three-dimensional texture subimage.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="level">The level.</param>
        /// <param name="internalformat">The internalformat.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="depth">The depth.</param>
        /// <param name="border">The border.</param>
        /// <param name="format">The format.</param>
        /// <param name="type">The type.</param>
        /// <param name="pixels">The pixels.</param>
        public void TexImage3DEXT(uint target, int level, uint internalformat, uint width, 
            uint height, uint depth, int border, uint format, uint type, IntPtr pixels)
        {
            getDelegateFor<glTexImage3DEXT>(ref glTexImage3DEXTDelegate)(target, level, internalformat, width, height, depth, border, format, type, pixels);
        }

        /// <summary>
        /// Texes the sub image3 DEXT.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="level">The level.</param>
        /// <param name="xoffset">The xoffset.</param>
        /// <param name="yoffset">The yoffset.</param>
        /// <param name="zoffset">The zoffset.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="depth">The depth.</param>
        /// <param name="format">The format.</param>
        /// <param name="type">The type.</param>
        /// <param name="pixels">The pixels.</param>
        public void TexSubImage3DEXT(uint target, int level, int xoffset, int yoffset, int zoffset,
            uint width, uint height, uint depth, uint format, uint type, IntPtr pixels)
        {
            getDelegateFor<glTexSubImage3DEXT>(ref glTexSubImage3DEXTDelegate)(target, level, xoffset, yoffset, zoffset, width, height, depth, format, type, pixels);
        }

        private delegate void glTexImage3DEXT(uint target, int level, uint internalformat, uint width, uint height, uint depth, int border, uint format, uint type, IntPtr pixels);
		private Delegate glTexImage3DEXTDelegate;
        private delegate void glTexSubImage3DEXT(uint target, int level, int xoffset, int yoffset, int zoffset, uint width, uint height, uint depth, uint format, uint type, IntPtr pixels);
		private Delegate glTexSubImage3DEXTDelegate;

        #endregion

        #region GL_EXT_bgra

        public const uint GL_BGR_EXT = 0x80E0;
        public const uint GL_BGRA_EXT = 0x80E1;

        #endregion

        #region GL_EXT_packed_pixels

        public const uint GL_UNSIGNED_BYTE_3_3_2_EXT = 0x8032;
        public const uint GL_UNSIGNED_SHORT_4_4_4_4_EXT = 0x8033;
        public const uint GL_UNSIGNED_SHORT_5_5_5_1_EXT = 0x8034;
        public const uint GL_UNSIGNED_INT_8_8_8_8_EXT = 0x8035;
        public const uint GL_UNSIGNED_INT_10_10_10_2_EXT = 0x8036;

        #endregion

        #region GL_EXT_rescale_normal

        public const uint GL_RESCALE_NORMAL_EXT = 0x803A;

        #endregion

        #region GL_EXT_separate_specular_color

        public const uint GL_LIGHT_MODEL_COLOR_CONTROL_EXT = 0x81F8;
        public const uint GL_SINGLE_COLOR_EXT = 0x81F9;
        public const uint GL_SEPARATE_SPECULAR_COLOR_EXT = 0x81FA;

        #endregion

        #region GL_SGIS_texture_edge_clamp

        public const uint GL_CLAMP_TO_EDGE_SGIS = 0x812F;

        #endregion

        #region GL_SGIS_texture_lod

        public const uint GL_TEXTURE_MIN_LOD_SGIS = 0x813A;
        public const uint GL_TEXTURE_MAX_LOD_SGIS = 0x813B;
        public const uint GL_TEXTURE_BASE_LEVEL_SGIS = 0x813C;
        public const uint GL_TEXTURE_MAX_LEVEL_SGIS = 0x813D;

        #endregion

        #region GL_EXT_draw_range_elements

        /// <summary>
        /// Render primitives from array data.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <param name="count">The count.</param>
        /// <param name="type">The type.</param>
        /// <param name="indices">The indices.</param>
        public void DrawRangeElementsEXT(uint mode, uint start, uint end, uint count, uint type, IntPtr indices)
        {
            getDelegateFor<glDrawRangeElementsEXT>(ref glDrawRangeElementsEXTDelegate)(mode, start, end, count, type, indices);
        }

        private delegate void glDrawRangeElementsEXT(uint mode, uint start, uint end, uint count, uint type, IntPtr indices);
		private Delegate glDrawRangeElementsEXTDelegate;

        public const uint GL_MAX_ELEMENTS_VERTICES_EXT = 0x80E8;
        public const uint GL_MAX_ELEMENTS_INDICES_EXT = 0x80E9;

        #endregion

        #region GL_SGI_color_table

        //  Delegates
        public void ColorTableSGI(uint target, uint internalformat, uint width, uint format, uint type, IntPtr table)
        {
            getDelegateFor<glColorTableSGI>(ref glColorTableSGIDelegate)(target, internalformat, width, format, type, table);
        }

        public void ColorTableParameterSGI(uint target, uint pname, float[] parameters)
        {
            getDelegateFor<glColorTableParameterfvSGI>(ref glColorTableParameterfvSGIDelegate)(target, pname, parameters);
        }

        public void ColorTableParameterSGI(uint target, uint pname, int[] parameters)
        {
            getDelegateFor<glColorTableParameterivSGI>(ref glColorTableParameterivSGIDelegate)(target, pname, parameters);
        }

        public void CopyColorTableSGI(uint target, uint internalformat, int x, int y, uint width)
        {
            getDelegateFor<glCopyColorTableSGI>(ref glCopyColorTableSGIDelegate)(target, internalformat, x, y, width);
        }

        public void GetColorTableSGI(uint target, uint format, uint type, IntPtr table)
        {
            getDelegateFor<glGetColorTableSGI>(ref glGetColorTableSGIDelegate)(target, format, type, table);
        }

        public void GetColorTableParameterSGI(uint target, uint pname, float[] parameters)
        {
            getDelegateFor<glGetColorTableParameterfvSGI>(ref glGetColorTableParameterfvSGIDelegate)(target, pname, parameters);
        }

        public void GetColorTableParameterSGI(uint target, uint pname, int[] parameters)
        {
            getDelegateFor<glGetColorTableParameterivSGI>(ref glGetColorTableParameterivSGIDelegate)(target, pname, parameters);
        }

        //  Delegates
        private delegate void glColorTableSGI(uint target, uint internalformat, uint width, uint format, uint type, IntPtr table);
		private Delegate glColorTableSGIDelegate;
        private delegate void glColorTableParameterfvSGI(uint target, uint pname, float[] parameters);
		private Delegate glColorTableParameterfvSGIDelegate;
        private delegate void glColorTableParameterivSGI(uint target, uint pname, int[] parameters);
		private Delegate glColorTableParameterivSGIDelegate;
        private delegate void glCopyColorTableSGI(uint target, uint internalformat, int x, int y, uint width);
		private Delegate glCopyColorTableSGIDelegate;
        private delegate void glGetColorTableSGI(uint target, uint format, uint type, IntPtr table);
		private Delegate glGetColorTableSGIDelegate;
        private delegate void glGetColorTableParameterfvSGI(uint target, uint pname, float[] parameters);
		private Delegate glGetColorTableParameterfvSGIDelegate;
        private delegate void glGetColorTableParameterivSGI(uint target, uint pname, int[] parameters);
		private Delegate glGetColorTableParameterivSGIDelegate;

        //  Constants
        public const uint GL_COLOR_TABLE_SGI = 0x80D0;
        public const uint GL_POST_CONVOLUTION_COLOR_TABLE_SGI = 0x80D1;
        public const uint GL_POST_COLOR_MATRIX_COLOR_TABLE_SGI = 0x80D2;
        public const uint GL_PROXY_COLOR_TABLE_SGI = 0x80D3;
        public const uint GL_PROXY_POST_CONVOLUTION_COLOR_TABLE_SGI = 0x80D4;
        public const uint GL_PROXY_POST_COLOR_MATRIX_COLOR_TABLE_SGI = 0x80D5;
        public const uint GL_COLOR_TABLE_SCALE_SGI = 0x80D6;
        public const uint GL_COLOR_TABLE_BIAS_SGI = 0x80D7;
        public const uint GL_COLOR_TABLE_FORMAT_SGI = 0x80D8;
        public const uint GL_COLOR_TABLE_WIDTH_SGI = 0x80D9;
        public const uint GL_COLOR_TABLE_RED_SIZE_SGI = 0x80DA;
        public const uint GL_COLOR_TABLE_GREEN_SIZE_SGI = 0x80DB;
        public const uint GL_COLOR_TABLE_BLUE_SIZE_SGI = 0x80DC;
        public const uint GL_COLOR_TABLE_ALPHA_SIZE_SGI = 0x80DD;
        public const uint GL_COLOR_TABLE_LUMINANCE_SIZE_SGI = 0x80DE;
        public const uint GL_COLOR_TABLE_INTENSITY_SIZE_SGI = 0x80DF;

        #endregion

        #region GL_EXT_convolution

        //  Methods.
        public void ConvolutionFilter1DEXT(uint target, uint internalformat, int width, uint format, uint type, IntPtr image)
        {
            getDelegateFor<glConvolutionFilter1DEXT>(ref glConvolutionFilter1DEXTDelegate)(target, internalformat, width, format, type, image);
        }

        public void ConvolutionFilter2DEXT(uint target, uint internalformat, int width, int height, uint format, uint type, IntPtr image)
        {
            getDelegateFor<glConvolutionFilter2DEXT>(ref glConvolutionFilter2DEXTDelegate)(target, internalformat, width, height, format, type, image);
        }

        public void ConvolutionParameterEXT(uint target, uint pname, float parameters)
        {
            getDelegateFor<glConvolutionParameterfEXT>(ref glConvolutionParameterfEXTDelegate)(target, pname, parameters);
        }

        public void ConvolutionParameterEXT(uint target, uint pname, float[] parameters)
        {
            getDelegateFor<glConvolutionParameterfvEXT>(ref glConvolutionParameterfvEXTDelegate)(target, pname, parameters);
        }

        public void ConvolutionParameterEXT(uint target, uint pname, int parameter)
        {
            getDelegateFor<glConvolutionParameteriEXT>(ref glConvolutionParameteriEXTDelegate)(target, pname, parameter);
        }

        public void ConvolutionParameterEXT(uint target, uint pname, int[] parameters)
        {
            getDelegateFor<glConvolutionParameterivEXT>(ref glConvolutionParameterivEXTDelegate)(target, pname, parameters);
        }

        public void CopyConvolutionFilter1DEXT(uint target, uint internalformat, int x, int y, int width)
        {
            getDelegateFor<glCopyConvolutionFilter1DEXT>(ref glCopyConvolutionFilter1DEXTDelegate)(target, internalformat, x, y, width);
        }

        public void CopyConvolutionFilter2DEXT(uint target, uint internalformat, int x, int y, int width, int height)
        {
            getDelegateFor<glCopyConvolutionFilter2DEXT>(ref glCopyConvolutionFilter2DEXTDelegate)(target, internalformat, x, y, width, height);
        }

        public void GetConvolutionFilterEXT(uint target, uint format, uint type, IntPtr image)
        {
            getDelegateFor<glGetConvolutionFilterEXT>(ref glGetConvolutionFilterEXTDelegate)(target, format, type, image);
        }

        public void GetConvolutionParameterfvEXT(uint target, uint pname, float[] parameters)
        {
            getDelegateFor<glGetConvolutionParameterfvEXT>(ref glGetConvolutionParameterfvEXTDelegate)(target, pname, parameters);
        }

        public void GetConvolutionParameterivEXT(uint target, uint pname, int[] parameters)
        {
            getDelegateFor<glGetConvolutionParameterivEXT>(ref glGetConvolutionParameterivEXTDelegate)(target, pname, parameters);
        }

        public void GetSeparableFilterEXT(uint target, uint format, uint type, IntPtr row, IntPtr column, IntPtr span)
        {
            getDelegateFor<glGetSeparableFilterEXT>(ref glGetSeparableFilterEXTDelegate)(target, format, type, row, column, span);
        }

        public void SeparableFilter2DEXT(uint target, uint internalformat, int width, int height, uint format, uint type, IntPtr row, IntPtr column)
        {
            getDelegateFor<glSeparableFilter2DEXT>(ref glSeparableFilter2DEXTDelegate)(target, internalformat, width, height, format, type, row, column);
        }

        //  Delegates
        private delegate void glConvolutionFilter1DEXT(uint target, uint internalformat, int width, uint format, uint type, IntPtr image);
		private Delegate glConvolutionFilter1DEXTDelegate;
        private delegate void glConvolutionFilter2DEXT(uint target, uint internalformat, int width, int height, uint format, uint type, IntPtr image);
		private Delegate glConvolutionFilter2DEXTDelegate;
        private delegate void glConvolutionParameterfEXT(uint target, uint pname, float parameters);
		private Delegate glConvolutionParameterfEXTDelegate;
        private delegate void glConvolutionParameterfvEXT(uint target, uint pname, float[] parameters);
		private Delegate glConvolutionParameterfvEXTDelegate;
        private delegate void glConvolutionParameteriEXT(uint target, uint pname, int parameter);
		private Delegate glConvolutionParameteriEXTDelegate;
        private delegate void glConvolutionParameterivEXT(uint target, uint pname, int[] parameters);
		private Delegate glConvolutionParameterivEXTDelegate;
        private delegate void glCopyConvolutionFilter1DEXT(uint target, uint internalformat, int x, int y, int width);
		private Delegate glCopyConvolutionFilter1DEXTDelegate;
        private delegate void glCopyConvolutionFilter2DEXT(uint target, uint internalformat, int x, int y, int width, int height);
		private Delegate glCopyConvolutionFilter2DEXTDelegate;
        private delegate void glGetConvolutionFilterEXT(uint target, uint format, uint type, IntPtr image);
		private Delegate glGetConvolutionFilterEXTDelegate;
        private delegate void glGetConvolutionParameterfvEXT(uint target, uint pname, float[] parameters);
		private Delegate glGetConvolutionParameterfvEXTDelegate;
        private delegate void glGetConvolutionParameterivEXT(uint target, uint pname, int[] parameters);
		private Delegate glGetConvolutionParameterivEXTDelegate;
        private delegate void glGetSeparableFilterEXT(uint target, uint format, uint type, IntPtr row, IntPtr column, IntPtr span);
		private Delegate glGetSeparableFilterEXTDelegate;
        private delegate void glSeparableFilter2DEXT(uint target, uint internalformat, int width, int height, uint format, uint type, IntPtr row, IntPtr column);
		private Delegate glSeparableFilter2DEXTDelegate;

        //  Constants        
        public const uint GL_CONVOLUTION_1D_EXT = 0x8010;
        public const uint GL_CONVOLUTION_2D_EXT = 0x8011;
        public const uint GL_SEPARABLE_2D_EXT = 0x8012;
        public const uint GL_CONVOLUTION_BORDER_MODE_EXT = 0x8013;
        public const uint GL_CONVOLUTION_FILTER_SCALE_EXT = 0x8014;
        public const uint GL_CONVOLUTION_FILTER_BIAS_EXT = 0x8015;
        public const uint GL_REDUCE_EXT = 0x8016;
        public const uint GL_CONVOLUTION_FORMAT_EXT = 0x8017;
        public const uint GL_CONVOLUTION_WIDTH_EXT = 0x8018;
        public const uint GL_CONVOLUTION_HEIGHT_EXT = 0x8019;
        public const uint GL_MAX_CONVOLUTION_WIDTH_EXT = 0x801A;
        public const uint GL_MAX_CONVOLUTION_HEIGHT_EXT = 0x801B;
        public const uint GL_POST_CONVOLUTION_RED_SCALE_EXT = 0x801C;
        public const uint GL_POST_CONVOLUTION_GREEN_SCALE_EXT = 0x801D;
        public const uint GL_POST_CONVOLUTION_BLUE_SCALE_EXT = 0x801E;
        public const uint GL_POST_CONVOLUTION_ALPHA_SCALE_EXT = 0x801F;
        public const uint GL_POST_CONVOLUTION_RED_BIAS_EXT = 0x8020;
        public const uint GL_POST_CONVOLUTION_GREEN_BIAS_EXT = 0x8021;
        public const uint GL_POST_CONVOLUTION_BLUE_BIAS_EXT = 0x8022;
        public const uint GL_POST_CONVOLUTION_ALPHA_BIAS_EXT = 0x8023;

        #endregion

        #region GL_SGI_color_matrix

        public const uint GL_COLOR_MATRIX_SGI = 0x80B1;
        public const uint GL_COLOR_MATRIX_STACK_DEPTH_SGI = 0x80B2;
        public const uint GL_MAX_COLOR_MATRIX_STACK_DEPTH_SGI = 0x80B3;
        public const uint GL_POST_COLOR_MATRIX_RED_SCALE_SGI = 0x80B4;
        public const uint GL_POST_COLOR_MATRIX_GREEN_SCALE_SGI = 0x80B5;
        public const uint GL_POST_COLOR_MATRIX_BLUE_SCALE_SGI = 0x80B6;
        public const uint GL_POST_COLOR_MATRIX_ALPHA_SCALE_SGI = 0x80B7;
        public const uint GL_POST_COLOR_MATRIX_RED_BIAS_SGI = 0x80B8;
        public const uint GL_POST_COLOR_MATRIX_GREEN_BIAS_SGI = 0x80B9;
        public const uint GL_POST_COLOR_MATRIX_BLUE_BIAS_SGI = 0x80BA;
        public const uint GL_POST_COLOR_MATRIX_ALPHA_BIAS_SGI = 0x80BB;

        #endregion

        #region GL_EXT_histogram

        //  Methods
        public void GetHistogramEXT(uint target, bool reset, uint format, uint type, IntPtr values)
        {
            getDelegateFor<glGetHistogramEXT>(ref glGetHistogramEXTDelegate)(target, reset, format, type, values);
        }

        public void GetHistogramParameterEXT(uint target, uint pname, float[] parameters)
        {
            getDelegateFor<glGetHistogramParameterfvEXT>(ref glGetHistogramParameterfvEXTDelegate)(target, pname, parameters);
        }

        public void GetHistogramParameterEXT(uint target, uint pname, int[] parameters)
        {
            getDelegateFor<glGetHistogramParameterivEXT>(ref glGetHistogramParameterivEXTDelegate)(target, pname, parameters);
        }

        public void GetMinmaxEXT(uint target, bool reset, uint format, uint type, IntPtr values)
        {
            getDelegateFor<glGetMinmaxEXT>(ref glGetMinmaxEXTDelegate)(target, reset, format, type, values);
        }

        public void GetMinmaxParameterfvEXT(uint target, uint pname, float[] parameters)
        {
            getDelegateFor<glGetMinmaxParameterfvEXT>(ref glGetMinmaxParameterfvEXTDelegate)(target, pname, parameters);
        }

        public void GetMinmaxParameterivEXT(uint target, uint pname, int[] parameters)
        {
            getDelegateFor<glGetMinmaxParameterivEXT>(ref glGetMinmaxParameterivEXTDelegate)(target, pname, parameters);
        }

        public void HistogramEXT(uint target, int width, uint internalformat, bool sink)
        {
            getDelegateFor<glHistogramEXT>(ref glHistogramEXTDelegate)(target, width, internalformat, sink);
        }

        public void MinmaxEXT(uint target, uint internalformat, bool sink)
        {
            getDelegateFor<glMinmaxEXT>(ref glMinmaxEXTDelegate)(target, internalformat, sink);
        }

        public void ResetHistogramEXT(uint target)
        {
            getDelegateFor<glResetHistogramEXT>(ref glResetHistogramEXTDelegate)(target);
        }

        public void ResetMinmaxEXT(uint target)
        {
            getDelegateFor<glResetMinmaxEXT>(ref glResetMinmaxEXTDelegate)(target);
        }

        //  Delegates
        private delegate void glGetHistogramEXT(uint target, bool reset, uint format, uint type, IntPtr values);
		private Delegate glGetHistogramEXTDelegate;
        private delegate void glGetHistogramParameterfvEXT(uint target, uint pname, float[] parameters);
		private Delegate glGetHistogramParameterfvEXTDelegate;
        private delegate void glGetHistogramParameterivEXT(uint target, uint pname, int[] parameters);
		private Delegate glGetHistogramParameterivEXTDelegate;
        private delegate void glGetMinmaxEXT(uint target, bool reset, uint format, uint type, IntPtr values);
		private Delegate glGetMinmaxEXTDelegate;
        private delegate void glGetMinmaxParameterfvEXT(uint target, uint pname, float[] parameters);
		private Delegate glGetMinmaxParameterfvEXTDelegate;
        private delegate void glGetMinmaxParameterivEXT(uint target, uint pname, int[] parameters);
		private Delegate glGetMinmaxParameterivEXTDelegate;
        private delegate void glHistogramEXT(uint target, int width, uint internalformat, bool sink);
		private Delegate glHistogramEXTDelegate;
        private delegate void glMinmaxEXT(uint target, uint internalformat, bool sink);
		private Delegate glMinmaxEXTDelegate;
        private delegate void glResetHistogramEXT(uint target);
		private Delegate glResetHistogramEXTDelegate;
        private delegate void glResetMinmaxEXT(uint target);
		private Delegate glResetMinmaxEXTDelegate;

        //  Constants
        public const uint GL_HISTOGRAM_EXT = 0x8024;
        public const uint GL_PROXY_HISTOGRAM_EXT = 0x8025;
        public const uint GL_HISTOGRAM_WIDTH_EXT = 0x8026;
        public const uint GL_HISTOGRAM_FORMAT_EXT = 0x8027;
        public const uint GL_HISTOGRAM_RED_SIZE_EXT = 0x8028;
        public const uint GL_HISTOGRAM_GREEN_SIZE_EXT = 0x8029;
        public const uint GL_HISTOGRAM_BLUE_SIZE_EXT = 0x802A;
        public const uint GL_HISTOGRAM_ALPHA_SIZE_EXT = 0x802B;
        public const uint GL_HISTOGRAM_LUMINANCE_SIZE_EXT = 0x802C;
        public const uint GL_HISTOGRAM_SINK_EXT = 0x802D;
        public const uint GL_MINMAX_EXT = 0x802E;
        public const uint GL_MINMAX_FORMAT_EXT = 0x802F;
        public const uint GL_MINMAX_SINK_EXT = 0x8030;
        public const uint GL_TABLE_TOO_LARGE_EXT = 0x8031;

        #endregion

        #region GL_EXT_blend_color

        //  Methods
        public void BlendColorEXT(float red, float green, float blue, float alpha)
        {
            getDelegateFor<glBlendColorEXT>(ref glBlendColorEXTDelegate)(red, green, blue, alpha);
        }

        //  Delegates
        private delegate void glBlendColorEXT(float red, float green, float blue, float alpha);
		private Delegate glBlendColorEXTDelegate;

        //  Constants        
        public const uint GL_CONSTANT_COLOR_EXT = 0x8001;
        public const uint GL_ONE_MINUS_CONSTANT_COLOR_EXT = 0x8002;
        public const uint GL_CONSTANT_ALPHA_EXT = 0x8003;
        public const uint GL_ONE_MINUS_CONSTANT_ALPHA_EXT = 0x8004;
        public const uint GL_BLEND_COLOR_EXT = 0x8005;

        #endregion

        #region GL_EXT_blend_minmax

        //  Methods
        public void BlendEquationEXT(uint mode)
        {
            getDelegateFor<glBlendEquationEXT>(ref glBlendEquationEXTDelegate)(mode);
        }

        //  Delegates
        private delegate void glBlendEquationEXT(uint mode);
		private Delegate glBlendEquationEXTDelegate;

        //  Constants        
        public const uint GL_FUNC_ADD_EXT = 0x8006;
        public const uint GL_MIN_EXT = 0x8007;
        public const uint GL_MAX_EXT = 0x8008;
        public const uint GL_FUNC_SUBTRACT_EXT = 0x800A;
        public const uint GL_FUNC_REVERSE_SUBTRACT_EXT = 0x800B;
        public const uint GL_BLEND_EQUATION_EXT = 0x8009;

        #endregion

        #region GL_ARB_multitexture

        //  Methods
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void ActiveTextureARB(uint texture)
        {
            getDelegateFor<glActiveTextureARB>(ref glActiveTextureARBDelegate)(texture);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void ClientActiveTextureARB(uint texture)
        {
            getDelegateFor<glClientActiveTextureARB>(ref glClientActiveTextureARBDelegate)(texture);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord1ARB(uint target, double s)
        {
            getDelegateFor<glMultiTexCoord1dARB>(ref glMultiTexCoord1dARBDelegate)(target, s);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord1ARB(uint target, double[] v)
        {
            getDelegateFor<glMultiTexCoord1dvARB>(ref glMultiTexCoord1dvARBDelegate)(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord1ARB(uint target, float s)
        {
            getDelegateFor<glMultiTexCoord1fARB>(ref glMultiTexCoord1fARBDelegate)(target, s);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord1ARB(uint target, float[] v)
        {
            getDelegateFor<glMultiTexCoord1fvARB>(ref glMultiTexCoord1fvARBDelegate)(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord1ARB(uint target, int s)
        {
            getDelegateFor<glMultiTexCoord1iARB>(ref glMultiTexCoord1iARBDelegate)(target, s);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord1ARB(uint target, int[] v)
        {
            getDelegateFor<glMultiTexCoord1ivARB>(ref glMultiTexCoord1ivARBDelegate)(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord1ARB(uint target, short s)
        {
            getDelegateFor<glMultiTexCoord1sARB>(ref glMultiTexCoord1sARBDelegate)(target, s);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord1ARB(uint target, short[] v)
        {
            getDelegateFor<glMultiTexCoord1svARB>(ref glMultiTexCoord1svARBDelegate)(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord2ARB(uint target, double s, double t)
        {
            getDelegateFor<glMultiTexCoord2dARB>(ref glMultiTexCoord2dARBDelegate)(target, s, t);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord2ARB(uint target, double[] v)
        {
            getDelegateFor<glMultiTexCoord2dvARB>(ref glMultiTexCoord2dvARBDelegate)(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord2ARB(uint target, float s, float t)
        {
            getDelegateFor<glMultiTexCoord2fARB>(ref glMultiTexCoord2fARBDelegate)(target, s, t);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord2ARB(uint target, float[] v)
        {
            getDelegateFor<glMultiTexCoord2fvARB>(ref glMultiTexCoord2fvARBDelegate)(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord2ARB(uint target, int s, int t)
        {
            getDelegateFor<glMultiTexCoord2iARB>(ref glMultiTexCoord2iARBDelegate)(target, s, t);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord2ARB(uint target, int[] v)
        {
            getDelegateFor<glMultiTexCoord2ivARB>(ref glMultiTexCoord2ivARBDelegate)(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord2ARB(uint target, short s, short t)
        {
            getDelegateFor<glMultiTexCoord2sARB>(ref glMultiTexCoord2sARBDelegate)(target, s, t);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord2ARB(uint target, short[] v)
        {
            getDelegateFor<glMultiTexCoord2svARB>(ref glMultiTexCoord2svARBDelegate)(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord3ARB(uint target, double s, double t, double r)
        {
            getDelegateFor<glMultiTexCoord3dARB>(ref glMultiTexCoord3dARBDelegate)(target, s, t, r);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord3ARB(uint target, double[] v)
        {
            getDelegateFor<glMultiTexCoord3dvARB>(ref glMultiTexCoord3dvARBDelegate)(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord3ARB(uint target, float s, float t, float r)
        {
            getDelegateFor<glMultiTexCoord3fARB>(ref glMultiTexCoord3fARBDelegate)(target, s, t, r);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord3ARB(uint target, float[] v)
        {
            getDelegateFor<glMultiTexCoord3fvARB>(ref glMultiTexCoord3fvARBDelegate)(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord3ARB(uint target, int s, int t, int r)
        {
            getDelegateFor<glMultiTexCoord3iARB>(ref glMultiTexCoord3iARBDelegate)(target, s, t, r);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord3ARB(uint target, int[] v)
        {
            getDelegateFor<glMultiTexCoord3ivARB>(ref glMultiTexCoord3ivARBDelegate)(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord3ARB(uint target, short s, short t, short r)
        {
            getDelegateFor<glMultiTexCoord3sARB>(ref glMultiTexCoord3sARBDelegate)(target, s, t, r);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord3ARB(uint target, short[] v)
        {
            getDelegateFor<glMultiTexCoord3svARB>(ref glMultiTexCoord3svARBDelegate)(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord4ARB(uint target, double s, double t, double r, double q)
        {
            getDelegateFor<glMultiTexCoord4dARB>(ref glMultiTexCoord4dARBDelegate)(target, s, t, r, q);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord4ARB(uint target, double[] v)
        {
            getDelegateFor<glMultiTexCoord4dvARB>(ref glMultiTexCoord4dvARBDelegate)(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord4ARB(uint target, float s, float t, float r, float q)
        {
            getDelegateFor<glMultiTexCoord4fARB>(ref glMultiTexCoord4fARBDelegate)(target, s, t, r, q);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord4ARB(uint target, float[] v)
        {
            getDelegateFor<glMultiTexCoord4fvARB>(ref glMultiTexCoord4fvARBDelegate)(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord4ARB(uint target, int s, int t, int r, int q)
        {
            getDelegateFor<glMultiTexCoord4iARB>(ref glMultiTexCoord4iARBDelegate)(target, s, t, r, q);
        }
        public void MultiTexCoord4ARB(uint target, int[] v)
        {
            getDelegateFor<glMultiTexCoord4ivARB>(ref glMultiTexCoord4ivARBDelegate)(target, v);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord4ARB(uint target, short s, short t, short r, short q)
        {
            getDelegateFor<glMultiTexCoord4sARB>(ref glMultiTexCoord4sARBDelegate)(target, s, t, r, q);
        }
        [Obsolete("Deprecated from OpenGL version 3.0")]
        public void MultiTexCoord4ARB(uint target, short[] v)
        {
            getDelegateFor<glMultiTexCoord4svARB>(ref glMultiTexCoord4svARBDelegate)(target, v);
        }

        //  Delegates 
        private delegate void glActiveTextureARB(uint texture);
		private Delegate glActiveTextureARBDelegate;
        private delegate void glClientActiveTextureARB(uint texture);
		private Delegate glClientActiveTextureARBDelegate;
        private delegate void glMultiTexCoord1dARB(uint target, double s);
		private Delegate glMultiTexCoord1dARBDelegate;
        private delegate void glMultiTexCoord1dvARB(uint target, double[] v);
		private Delegate glMultiTexCoord1dvARBDelegate;
        private delegate void glMultiTexCoord1fARB(uint target, float s);
		private Delegate glMultiTexCoord1fARBDelegate;
        private delegate void glMultiTexCoord1fvARB(uint target, float[] v);
		private Delegate glMultiTexCoord1fvARBDelegate;
        private delegate void glMultiTexCoord1iARB(uint target, int s);
		private Delegate glMultiTexCoord1iARBDelegate;
        private delegate void glMultiTexCoord1ivARB(uint target, int[] v);
		private Delegate glMultiTexCoord1ivARBDelegate;
        private delegate void glMultiTexCoord1sARB(uint target, short s);
		private Delegate glMultiTexCoord1sARBDelegate;
        private delegate void glMultiTexCoord1svARB(uint target, short[] v);
		private Delegate glMultiTexCoord1svARBDelegate;
        private delegate void glMultiTexCoord2dARB(uint target, double s, double t);
		private Delegate glMultiTexCoord2dARBDelegate;
        private delegate void glMultiTexCoord2dvARB(uint target, double[] v);
		private Delegate glMultiTexCoord2dvARBDelegate;
        private delegate void glMultiTexCoord2fARB(uint target, float s, float t);
		private Delegate glMultiTexCoord2fARBDelegate;
        private delegate void glMultiTexCoord2fvARB(uint target, float[] v);
		private Delegate glMultiTexCoord2fvARBDelegate;
        private delegate void glMultiTexCoord2iARB(uint target, int s, int t);
		private Delegate glMultiTexCoord2iARBDelegate;
        private delegate void glMultiTexCoord2ivARB(uint target, int[] v);
		private Delegate glMultiTexCoord2ivARBDelegate;
        private delegate void glMultiTexCoord2sARB(uint target, short s, short t);
		private Delegate glMultiTexCoord2sARBDelegate;
        private delegate void glMultiTexCoord2svARB(uint target, short[] v);
		private Delegate glMultiTexCoord2svARBDelegate;
        private delegate void glMultiTexCoord3dARB(uint target, double s, double t, double r);
		private Delegate glMultiTexCoord3dARBDelegate;
        private delegate void glMultiTexCoord3dvARB(uint target, double[] v);
		private Delegate glMultiTexCoord3dvARBDelegate;
        private delegate void glMultiTexCoord3fARB(uint target, float s, float t, float r);
		private Delegate glMultiTexCoord3fARBDelegate;
        private delegate void glMultiTexCoord3fvARB(uint target, float[] v);
		private Delegate glMultiTexCoord3fvARBDelegate;
        private delegate void glMultiTexCoord3iARB(uint target, int s, int t, int r);
		private Delegate glMultiTexCoord3iARBDelegate;
        private delegate void glMultiTexCoord3ivARB(uint target, int[] v);
		private Delegate glMultiTexCoord3ivARBDelegate;
        private delegate void glMultiTexCoord3sARB(uint target, short s, short t, short r);
		private Delegate glMultiTexCoord3sARBDelegate;
        private delegate void glMultiTexCoord3svARB(uint target, short[] v);
		private Delegate glMultiTexCoord3svARBDelegate;
        private delegate void glMultiTexCoord4dARB(uint target, double s, double t, double r, double q);
		private Delegate glMultiTexCoord4dARBDelegate;
        private delegate void glMultiTexCoord4dvARB(uint target, double[] v);
		private Delegate glMultiTexCoord4dvARBDelegate;
        private delegate void glMultiTexCoord4fARB(uint target, float s, float t, float r, float q);
		private Delegate glMultiTexCoord4fARBDelegate;
        private delegate void glMultiTexCoord4fvARB(uint target, float[] v);
		private Delegate glMultiTexCoord4fvARBDelegate;
        private delegate void glMultiTexCoord4iARB(uint target, int s, int t, int r, int q);
		private Delegate glMultiTexCoord4iARBDelegate;
        private delegate void glMultiTexCoord4ivARB(uint target, int[] v);
		private Delegate glMultiTexCoord4ivARBDelegate;
        private delegate void glMultiTexCoord4sARB(uint target, short s, short t, short r, short q);
		private Delegate glMultiTexCoord4sARBDelegate;
        private delegate void glMultiTexCoord4svARB(uint target, short[] v);
		private Delegate glMultiTexCoord4svARBDelegate;

        //  Constants
        public const uint GL_TEXTURE0_ARB = 0x84C0;
        public const uint GL_TEXTURE1_ARB = 0x84C1;
        public const uint GL_TEXTURE2_ARB = 0x84C2;
        public const uint GL_TEXTURE3_ARB = 0x84C3;
        public const uint GL_TEXTURE4_ARB = 0x84C4;
        public const uint GL_TEXTURE5_ARB = 0x84C5;
        public const uint GL_TEXTURE6_ARB = 0x84C6;
        public const uint GL_TEXTURE7_ARB = 0x84C7;
        public const uint GL_TEXTURE8_ARB = 0x84C8;
        public const uint GL_TEXTURE9_ARB = 0x84C9;
        public const uint GL_TEXTURE10_ARB = 0x84CA;
        public const uint GL_TEXTURE11_ARB = 0x84CB;
        public const uint GL_TEXTURE12_ARB = 0x84CC;
        public const uint GL_TEXTURE13_ARB = 0x84CD;
        public const uint GL_TEXTURE14_ARB = 0x84CE;
        public const uint GL_TEXTURE15_ARB = 0x84CF;
        public const uint GL_TEXTURE16_ARB = 0x84D0;
        public const uint GL_TEXTURE17_ARB = 0x84D1;
        public const uint GL_TEXTURE18_ARB = 0x84D2;
        public const uint GL_TEXTURE19_ARB = 0x84D3;
        public const uint GL_TEXTURE20_ARB = 0x84D4;
        public const uint GL_TEXTURE21_ARB = 0x84D5;
        public const uint GL_TEXTURE22_ARB = 0x84D6;
        public const uint GL_TEXTURE23_ARB = 0x84D7;
        public const uint GL_TEXTURE24_ARB = 0x84D8;
        public const uint GL_TEXTURE25_ARB = 0x84D9;
        public const uint GL_TEXTURE26_ARB = 0x84DA;
        public const uint GL_TEXTURE27_ARB = 0x84DB;
        public const uint GL_TEXTURE28_ARB = 0x84DC;
        public const uint GL_TEXTURE29_ARB = 0x84DD;
        public const uint GL_TEXTURE30_ARB = 0x84DE;
        public const uint GL_TEXTURE31_ARB = 0x84DF;
        public const uint GL_ACTIVE_TEXTURE_ARB = 0x84E0;
        public const uint GL_CLIENT_ACTIVE_TEXTURE_ARB = 0x84E1;
        public const uint GL_MAX_TEXTURE_UNITS_ARB = 0x84E2;

        #endregion

        #region GL_ARB_texture_compression

        //  Methods
        public void CompressedTexImage3DARB(uint target, int level, uint internalformat, int width, int height, int depth, int border, int imageSize, IntPtr data)
        {
            getDelegateFor<glCompressedTexImage3DARB>(ref glCompressedTexImage3DARBDelegate)(target, level, internalformat, width, height, depth, border, imageSize, data);
        }
        public void CompressedTexImage2DARB(uint target, int level, uint internalformat, int width, int height, int border, int imageSize, IntPtr data)
        {
            getDelegateFor<glCompressedTexImage2DARB>(ref glCompressedTexImage2DARBDelegate)(target, level, internalformat, width, height, border, imageSize, data);
        }
        public void CompressedTexImage1DARB(uint target, int level, uint internalformat, int width, int border, int imageSize, IntPtr data)
        {
            getDelegateFor<glCompressedTexImage1DARB>(ref glCompressedTexImage1DARBDelegate)(target, level, internalformat, width, border, imageSize, data);
        }
        public void CompressedTexSubImage3DARB(uint target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, int imageSize, IntPtr data)
        {
            getDelegateFor<glCompressedTexSubImage3DARB>(ref glCompressedTexSubImage3DARBDelegate)(target, level, xoffset, yoffset, zoffset, width, height, depth, format, imageSize, data);
        }
        public void CompressedTexSubImage2DARB(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, int imageSize, IntPtr data)
        {
            getDelegateFor<glCompressedTexSubImage2DARB>(ref glCompressedTexSubImage2DARBDelegate)(target, level, xoffset, yoffset, width, height, format, imageSize, data);
        }
        public void CompressedTexSubImage1DARB(uint target, int level, int xoffset, int width, uint format, int imageSize, IntPtr data)
        {
            getDelegateFor<glCompressedTexSubImage1DARB>(ref glCompressedTexSubImage1DARBDelegate)(target, level, xoffset, width, format, imageSize, data);
        }

        //  Delegates
        private delegate void glCompressedTexImage3DARB(uint target, int level, uint internalformat, int width, int height, int depth, int border, int imageSize, IntPtr data);
		private Delegate glCompressedTexImage3DARBDelegate;
        private delegate void glCompressedTexImage2DARB(uint target, int level, uint internalformat, int width, int height, int border, int imageSize, IntPtr data);
		private Delegate glCompressedTexImage2DARBDelegate;
        private delegate void glCompressedTexImage1DARB(uint target, int level, uint internalformat, int width, int border, int imageSize, IntPtr data);
		private Delegate glCompressedTexImage1DARBDelegate;
        private delegate void glCompressedTexSubImage3DARB(uint target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, uint format, int imageSize, IntPtr data);
		private Delegate glCompressedTexSubImage3DARBDelegate;
        private delegate void glCompressedTexSubImage2DARB(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, int imageSize, IntPtr data);
		private Delegate glCompressedTexSubImage2DARBDelegate;
        private delegate void glCompressedTexSubImage1DARB(uint target, int level, int xoffset, int width, uint format, int imageSize, IntPtr data);
		private Delegate glCompressedTexSubImage1DARBDelegate;

        //  Constants
        public const uint GL_COMPRESSED_ALPHA_ARB = 0x84E9;
        public const uint GL_COMPRESSED_LUMINANCE_ARB = 0x84EA;
        public const uint GL_COMPRESSED_LUMINANCE_ALPHA_ARB = 0x84EB;
        public const uint GL_COMPRESSED_INTENSITY_ARB = 0x84EC;
        public const uint GL_COMPRESSED_RGB_ARB = 0x84ED;
        public const uint GL_COMPRESSED_RGBA_ARB = 0x84EE;
        public const uint GL_TEXTURE_COMPRESSION_HINT_ARB = 0x84EF;
        public const uint GL_TEXTURE_COMPRESSED_IMAGE_SIZE_ARB = 0x86A0;
        public const uint GL_TEXTURE_COMPRESSED_ARB = 0x86A1;
        public const uint GL_NUM_COMPRESSED_TEXTURE_FORMATS_ARB = 0x86A2;
        public const uint GL_COMPRESSED_TEXTURE_FORMATS_ARB = 0x86A3;

        #endregion

        #region GL_EXT_texture_cube_map

        //  Constants
        public const uint GL_NORMAL_MAP_EXT = 0x8511;
        public const uint GL_REFLECTION_MAP_EXT = 0x8512;
        public const uint GL_TEXTURE_CUBE_MAP_EXT = 0x8513;
        public const uint GL_TEXTURE_BINDING_CUBE_MAP_EXT = 0x8514;
        public const uint GL_TEXTURE_CUBE_MAP_POSITIVE_X_EXT = 0x8515;
        public const uint GL_TEXTURE_CUBE_MAP_NEGATIVE_X_EXT = 0x8516;
        public const uint GL_TEXTURE_CUBE_MAP_POSITIVE_Y_EXT = 0x8517;
        public const uint GL_TEXTURE_CUBE_MAP_NEGATIVE_Y_EXT = 0x8518;
        public const uint GL_TEXTURE_CUBE_MAP_POSITIVE_Z_EXT = 0x8519;
        public const uint GL_TEXTURE_CUBE_MAP_NEGATIVE_Z_EXT = 0x851A;
        public const uint GL_PROXY_TEXTURE_CUBE_MAP_EXT = 0x851B;
        public const uint GL_MAX_CUBE_MAP_TEXTURE_SIZE_EXT = 0x851C;

        #endregion

        #region GL_ARB_multisample

        //  Methods
        public void SampleCoverageARB(float value, bool invert)
        {
            getDelegateFor<glSampleCoverageARB>(ref glSampleCoverageARBDelegate)(value, invert);
        }

        //  Delegates
        private delegate void glSampleCoverageARB(float value, bool invert);
		private Delegate glSampleCoverageARBDelegate;

        //  Constants
        public const uint GL_MULTISAMPLE_ARB = 0x809D;
        public const uint GL_SAMPLE_ALPHA_TO_COVERAGE_ARB = 0x809E;
        public const uint GL_SAMPLE_ALPHA_TO_ONE_ARB = 0x809F;
        public const uint GL_SAMPLE_COVERAGE_ARB = 0x80A0;
        public const uint GL_SAMPLE_BUFFERS_ARB = 0x80A8;
        public const uint GL_SAMPLES_ARB = 0x80A9;
        public const uint GL_SAMPLE_COVERAGE_VALUE_ARB = 0x80AA;
        public const uint GL_SAMPLE_COVERAGE_INVERT_ARB = 0x80AB;
        public const uint GL_MULTISAMPLE_BIT_ARB = 0x20000000;

        #endregion

        #region GL_ARB_texture_env_add

        //  Appears to not have any functionality

        #endregion

        #region GL_ARB_texture_env_combine

        //  Constants
        public const uint GL_COMBINE_ARB = 0x8570;
        public const uint GL_COMBINE_RGB_ARB = 0x8571;
        public const uint GL_COMBINE_ALPHA_ARB = 0x8572;
        public const uint GL_SOURCE0_RGB_ARB = 0x8580;
        public const uint GL_SOURCE1_RGB_ARB = 0x8581;
        public const uint GL_SOURCE2_RGB_ARB = 0x8582;
        public const uint GL_SOURCE0_ALPHA_ARB = 0x8588;
        public const uint GL_SOURCE1_ALPHA_ARB = 0x8589;
        public const uint GL_SOURCE2_ALPHA_ARB = 0x858A;
        public const uint GL_OPERAND0_RGB_ARB = 0x8590;
        public const uint GL_OPERAND1_RGB_ARB = 0x8591;
        public const uint GL_OPERAND2_RGB_ARB = 0x8592;
        public const uint GL_OPERAND0_ALPHA_ARB = 0x8598;
        public const uint GL_OPERAND1_ALPHA_ARB = 0x8599;
        public const uint GL_OPERAND2_ALPHA_ARB = 0x859A;
        public const uint GL_RGB_SCALE_ARB = 0x8573;
        public const uint GL_ADD_SIGNED_ARB = 0x8574;
        public const uint GL_INTERPOLATE_ARB = 0x8575;
        public const uint GL_SUBTRACT_ARB = 0x84E7;
        public const uint GL_CONSTANT_ARB = 0x8576;
        public const uint GL_PRIMARY_COLOR_ARB = 0x8577;
        public const uint GL_PREVIOUS_ARB = 0x8578;

        #endregion

        #region GL_ARB_texture_env_dot3

        //  Constants
        public const uint GL_DOT3_RGB_ARB = 0x86AE;
        public const uint GL_DOT3_RGBA_ARB = 0x86AF;

        #endregion

        #region GL_ARB_texture_border_clamp

        //  Constants
        public const uint GL_CLAMP_TO_BORDER_ARB = 0x812D;

        #endregion

        #region GL_ARB_transpose_matrix

        //  Methods
        public void LoadTransposeMatrixARB(float[] m)
        {
            getDelegateFor<glLoadTransposeMatrixfARB>(ref glLoadTransposeMatrixfARBDelegate)(m);
        }
        public void LoadTransposeMatrixARB(double[] m)
        {
            getDelegateFor<glLoadTransposeMatrixdARB>(ref glLoadTransposeMatrixdARBDelegate)(m);
        }
        public void MultTransposeMatrixARB(float[] m)
        {
            getDelegateFor<glMultTransposeMatrixfARB>(ref glMultTransposeMatrixfARBDelegate)(m);
        }
        public void MultTransposeMatrixARB(double[] m)
        {
            getDelegateFor<glMultTransposeMatrixdARB>(ref glMultTransposeMatrixdARBDelegate)(m);
        }

        //  Delegates
        private delegate void glLoadTransposeMatrixfARB(float[] m);
		private Delegate glLoadTransposeMatrixfARBDelegate;
        private delegate void glLoadTransposeMatrixdARB(double[] m);
		private Delegate glLoadTransposeMatrixdARBDelegate;
        private delegate void glMultTransposeMatrixfARB(float[] m);
		private Delegate glMultTransposeMatrixfARBDelegate;
        private delegate void glMultTransposeMatrixdARB(double[] m);
		private Delegate glMultTransposeMatrixdARBDelegate;

        //  Constants
        public const uint GL_TRANSPOSE_MODELVIEW_MATRIX_ARB = 0x84E3;
        public const uint GL_TRANSPOSE_PROJECTION_MATRIX_ARB = 0x84E4;
        public const uint GL_TRANSPOSE_TEXTURE_MATRIX_ARB = 0x84E5;
        public const uint GL_TRANSPOSE_COLOR_MATRIX_ARB = 0x84E6;

        #endregion

        #region GL_SGIS_generate_mipmap

        //  Constants
        public const uint GL_GENERATE_MIPMAP_SGIS = 0x8191;
        public const uint GL_GENERATE_MIPMAP_HINT_SGIS = 0x8192;

        #endregion

        #region GL_NV_blend_square

        //  Appears to be empty.

        #endregion

        #region GL_ARB_depth_texture

        //  Constants
        public const uint GL_DEPTH_COMPONENT16_ARB = 0x81A5;
        public const uint GL_DEPTH_COMPONENT24_ARB = 0x81A6;
        public const uint GL_DEPTH_COMPONENT32_ARB = 0x81A7;
        public const uint GL_TEXTURE_DEPTH_SIZE_ARB = 0x884A;
        public const uint GL_DEPTH_TEXTURE_MODE_ARB = 0x884B;

        #endregion

        #region GL_ARB_shadow

        //  Constants
        public const uint GL_TEXTURE_COMPARE_MODE_ARB = 0x884C;
        public const uint GL_TEXTURE_COMPARE_FUNC_ARB = 0x884D;
        public const uint GL_COMPARE_R_TO_TEXTURE_ARB = 0x884E;

        #endregion

        #region GL_EXT_fog_coord

        //  Methods
        public void FogCoordEXT(float coord)
        {
            getDelegateFor<glFogCoordfEXT>(ref glFogCoordfEXTDelegate)(coord);
        }
        public void FogCoordEXT(float[] coord)
        {
            getDelegateFor<glFogCoordfvEXT>(ref glFogCoordfvEXTDelegate)(coord);
        }
        public void FogCoordEXT(double coord)
        {
            getDelegateFor<glFogCoorddEXT>(ref glFogCoorddEXTDelegate)(coord);
        }
        public void FogCoordEXT(double[] coord)
        {
            getDelegateFor<glFogCoorddvEXT>(ref glFogCoorddvEXTDelegate)(coord);
        }
        public void FogCoordPointerEXT(uint type, int stride, IntPtr pointer)
        {
            getDelegateFor<glFogCoordPointerEXT>(ref glFogCoordPointerEXTDelegate)(type, stride, pointer);
        }

        //  Delegates
        private delegate void glFogCoordfEXT(float coord);
		private Delegate glFogCoordfEXTDelegate;
        private delegate void glFogCoordfvEXT(float[] coord);
		private Delegate glFogCoordfvEXTDelegate;
        private delegate void glFogCoorddEXT(double coord);
		private Delegate glFogCoorddEXTDelegate;
        private delegate void glFogCoorddvEXT(double[] coord);
		private Delegate glFogCoorddvEXTDelegate;
        private delegate void glFogCoordPointerEXT(uint type, int stride, IntPtr pointer);
		private Delegate glFogCoordPointerEXTDelegate;

        //  Constants
        public const uint GL_FOG_COORDINATE_SOURCE_EXT = 0x8450;
        public const uint GL_FOG_COORDINATE_EXT = 0x8451;
        public const uint GL_FRAGMENT_DEPTH_EXT = 0x8452;
        public const uint GL_CURRENT_FOG_COORDINATE_EXT = 0x8453;
        public const uint GL_FOG_COORDINATE_ARRAY_TYPE_EXT = 0x8454;
        public const uint GL_FOG_COORDINATE_ARRAY_STRIDE_EXT = 0x8455;
        public const uint GL_FOG_COORDINATE_ARRAY_POINTER_EXT = 0x8456;
        public const uint GL_FOG_COORDINATE_ARRAY_EXT = 0x8457;

        #endregion

        #region GL_EXT_multi_draw_arrays

        //  Methods
        public void MultiDrawArraysEXT(uint mode, int[] first, int[] count, int primcount)
        {
            getDelegateFor<glMultiDrawArraysEXT>(ref glMultiDrawArraysEXTDelegate)(mode, first, count, primcount);
        }
        public void MultiDrawElementsEXT(uint mode, int[] count, uint type, IntPtr indices, int primcount)
        {
            getDelegateFor<glMultiDrawElementsEXT>(ref glMultiDrawElementsEXTDelegate)(mode, count, type, indices, primcount);
        }

        //  Delegates
        private delegate void glMultiDrawArraysEXT(uint mode, int[] first, int[] count, int primcount);
		private Delegate glMultiDrawArraysEXTDelegate;
        private delegate void glMultiDrawElementsEXT(uint mode, int[] count, uint type, IntPtr indices, int primcount);
		private Delegate glMultiDrawElementsEXTDelegate;

        #endregion

        #region GL_ARB_point_parameters

        //  Methods
        public void PointParameterARB(uint pname, float parameter)
        {
            getDelegateFor<glPointParameterfARB>(ref glPointParameterfARBDelegate)(pname, parameter);
        }
        public void PointParameterARB(uint pname, float[] parameters)
        {
            getDelegateFor<glPointParameterfvARB>(ref glPointParameterfvARBDelegate)(pname, parameters);
        }

        //  Delegates
        private delegate void glPointParameterfARB(uint pname, float param);
		private Delegate glPointParameterfARBDelegate;
        private delegate void glPointParameterfvARB(uint pname, float[] parameters);
		private Delegate glPointParameterfvARBDelegate;

        //  Constants
        public const uint GL_POINT_SIZE_MIN_ARB = 0x8126;
        public const uint GL_POINT_SIZE_MAX_ARB = 0x8127;
        public const uint GL_POINT_FADE_THRESHOLD_SIZE_ARB = 0x8128;
        public const uint GL_POINT_DISTANCE_ATTENUATION_ARB = 0x8129;

        #endregion

        #region GL_EXT_secondary_color

        //  Methods
        public void SecondaryColor3EXT(sbyte red, sbyte green, sbyte blue)
        {
            getDelegateFor<glSecondaryColor3bEXT>(ref glSecondaryColor3bEXTDelegate)(red, green, blue);
        }
        public void SecondaryColor3EXT(sbyte[] v)
        {
            getDelegateFor<glSecondaryColor3bvEXT>(ref glSecondaryColor3bvEXTDelegate)(v);
        }
        public void SecondaryColor3EXT(double red, double green, double blue)
        {
            getDelegateFor<glSecondaryColor3dEXT>(ref glSecondaryColor3dEXTDelegate)(red, green, blue);
        }
        public void SecondaryColor3EXT(double[] v)
        {
            getDelegateFor<glSecondaryColor3dvEXT>(ref glSecondaryColor3dvEXTDelegate)(v);
        }
        public void SecondaryColor3EXT(float red, float green, float blue)
        {
            getDelegateFor<glSecondaryColor3fEXT>(ref glSecondaryColor3fEXTDelegate)(red, green, blue);
        }
        public void SecondaryColor3EXT(float[] v)
        {
            getDelegateFor<glSecondaryColor3fvEXT>(ref glSecondaryColor3fvEXTDelegate)(v);
        }
        public void SecondaryColor3EXT(int red, int green, int blue)
        {
            getDelegateFor<glSecondaryColor3iEXT>(ref glSecondaryColor3iEXTDelegate)(red, green, blue);
        }
        public void SecondaryColor3EXT(int[] v)
        {
            getDelegateFor<glSecondaryColor3ivEXT>(ref glSecondaryColor3ivEXTDelegate)(v);
        }
        public void SecondaryColor3EXT(short red, short green, short blue)
        {
            getDelegateFor<glSecondaryColor3sEXT>(ref glSecondaryColor3sEXTDelegate)(red, green, blue);
        }
        public void SecondaryColor3EXT(short[] v)
        {
            getDelegateFor<glSecondaryColor3svEXT>(ref glSecondaryColor3svEXTDelegate)(v);
        }
        public void SecondaryColor3EXT(byte red, byte green, byte blue)
        {
            getDelegateFor<glSecondaryColor3ubEXT>(ref glSecondaryColor3ubEXTDelegate)(red, green, blue);
        }
        public void SecondaryColor3EXT(byte[] v)
        {
            getDelegateFor<glSecondaryColor3ubvEXT>(ref glSecondaryColor3ubvEXTDelegate)(v);
        }
        public void SecondaryColor3EXT(uint red, uint green, uint blue)
        {
            getDelegateFor<glSecondaryColor3uiEXT>(ref glSecondaryColor3uiEXTDelegate)(red, green, blue);
        }
        public void SecondaryColor3EXT(uint[] v)
        {
            getDelegateFor<glSecondaryColor3uivEXT>(ref glSecondaryColor3uivEXTDelegate)(v);
        }
        public void SecondaryColor3EXT(ushort red, ushort green, ushort blue)
        {
            getDelegateFor<glSecondaryColor3usEXT>(ref glSecondaryColor3usEXTDelegate)(red, green, blue);
        }
        public void SecondaryColor3EXT(ushort[] v)
        {
            getDelegateFor<glSecondaryColor3usvEXT>(ref glSecondaryColor3usvEXTDelegate)(v);
        }
        public void SecondaryColorPointerEXT(int size, uint type, int stride, IntPtr pointer)
        {
            getDelegateFor<glSecondaryColorPointerEXT>(ref glSecondaryColorPointerEXTDelegate)(size, type, stride, pointer);
        }

        //  Delegates
        private delegate void glSecondaryColor3bEXT(sbyte red, sbyte green, sbyte blue);
		private Delegate glSecondaryColor3bEXTDelegate;
        private delegate void glSecondaryColor3bvEXT(sbyte[] v);
		private Delegate glSecondaryColor3bvEXTDelegate;
        private delegate void glSecondaryColor3dEXT(double red, double green, double blue);
		private Delegate glSecondaryColor3dEXTDelegate;
        private delegate void glSecondaryColor3dvEXT(double[] v);
		private Delegate glSecondaryColor3dvEXTDelegate;
        private delegate void glSecondaryColor3fEXT(float red, float green, float blue);
		private Delegate glSecondaryColor3fEXTDelegate;
        private delegate void glSecondaryColor3fvEXT(float[] v);
		private Delegate glSecondaryColor3fvEXTDelegate;
        private delegate void glSecondaryColor3iEXT(int red, int green, int blue);
		private Delegate glSecondaryColor3iEXTDelegate;
        private delegate void glSecondaryColor3ivEXT(int[] v);
		private Delegate glSecondaryColor3ivEXTDelegate;
        private delegate void glSecondaryColor3sEXT(short red, short green, short blue);
		private Delegate glSecondaryColor3sEXTDelegate;
        private delegate void glSecondaryColor3svEXT(short[] v);
		private Delegate glSecondaryColor3svEXTDelegate;
        private delegate void glSecondaryColor3ubEXT(byte red, byte green, byte blue);
		private Delegate glSecondaryColor3ubEXTDelegate;
        private delegate void glSecondaryColor3ubvEXT(byte[] v);
		private Delegate glSecondaryColor3ubvEXTDelegate;
        private delegate void glSecondaryColor3uiEXT(uint red, uint green, uint blue);
		private Delegate glSecondaryColor3uiEXTDelegate;
        private delegate void glSecondaryColor3uivEXT(uint[] v);
		private Delegate glSecondaryColor3uivEXTDelegate;
        private delegate void glSecondaryColor3usEXT(ushort red, ushort green, ushort blue);
		private Delegate glSecondaryColor3usEXTDelegate;
        private delegate void glSecondaryColor3usvEXT(ushort[] v);
		private Delegate glSecondaryColor3usvEXTDelegate;
        private delegate void glSecondaryColorPointerEXT(int size, uint type, int stride, IntPtr pointer);
		private Delegate glSecondaryColorPointerEXTDelegate;

        //  Constants        
        public const uint GL_COLOR_SUM_EXT = 0x8458;
        public const uint GL_CURRENT_SECONDARY_COLOR_EXT = 0x8459;
        public const uint GL_SECONDARY_COLOR_ARRAY_SIZE_EXT = 0x845A;
        public const uint GL_SECONDARY_COLOR_ARRAY_TYPE_EXT = 0x845B;
        public const uint GL_SECONDARY_COLOR_ARRAY_STRIDE_EXT = 0x845C;
        public const uint GL_SECONDARY_COLOR_ARRAY_POINTER_EXT = 0x845D;
        public const uint GL_SECONDARY_COLOR_ARRAY_EXT = 0x845E;

        #endregion

        #region GL_EXT_blend_func_separate

        //  Methods
        public void BlendFuncSeparateEXT(uint sfactorRGB, uint dfactorRGB, uint sfactorAlpha, uint dfactorAlpha)
        {
            getDelegateFor<glBlendFuncSeparateEXT>(ref glBlendFuncSeparateEXTDelegate)(sfactorRGB, dfactorRGB, sfactorAlpha, dfactorAlpha);
        }

        //  Delegates
        private delegate void glBlendFuncSeparateEXT(uint sfactorRGB, uint dfactorRGB, uint sfactorAlpha, uint dfactorAlpha);
		private Delegate glBlendFuncSeparateEXTDelegate;

        //  Constants
        public const uint GL_BLEND_DST_RGB_EXT = 0x80C8;
        public const uint GL_BLEND_SRC_RGB_EXT = 0x80C9;
        public const uint GL_BLEND_DST_ALPHA_EXT = 0x80CA;
        public const uint GL_BLEND_SRC_ALPHA_EXT = 0x80CB;

        #endregion

        #region GL_EXT_stencil_wrap

        //  Constants
        public const uint GL_INCR_WRAP_EXT = 0x8507;
        public const uint GL_DECR_WRAP_EXT = 0x8508;

        #endregion

        #region GL_ARB_texture_env_crossbar

        //  No methods or constants.

        #endregion

        #region GL_EXT_texture_lod_bias

        //  Constants
        public const uint GL_MAX_TEXTURE_LOD_BIAS_EXT = 0x84FD;
        public const uint GL_TEXTURE_FILTER_CONTROL_EXT = 0x8500;
        public const uint GL_TEXTURE_LOD_BIAS_EXT = 0x8501;

        #endregion

        #region GL_ARB_texture_mirrored_repeat

        //  Constants
        public const uint GL_MIRRORED_REPEAT_ARB = 0x8370;

        #endregion

        #region GL_ARB_window_pos

        //  Methods
        public void WindowPos2ARB(double x, double y)
        {
            getDelegateFor<glWindowPos2dARB>(ref glWindowPos2dARBDelegate)(x, y);
        }
        public void WindowPos2ARB(double[] v)
        {
            getDelegateFor<glWindowPos2dvARB>(ref glWindowPos2dvARBDelegate)(v);
        }
        public void WindowPos2ARB(float x, float y)
        {
            getDelegateFor<glWindowPos2fARB>(ref glWindowPos2fARBDelegate)(x, y);
        }
        public void WindowPos2ARB(float[] v)
        {
            getDelegateFor<glWindowPos2fvARB>(ref glWindowPos2fvARBDelegate)(v);
        }
        public void WindowPos2ARB(int x, int y)
        {
            getDelegateFor<glWindowPos2iARB>(ref glWindowPos2iARBDelegate)(x, y);
        }
        public void WindowPos2ARB(int[] v)
        {
            getDelegateFor<glWindowPos2ivARB>(ref glWindowPos2ivARBDelegate)(v);
        }
        public void WindowPos2ARB(short x, short y)
        {
            getDelegateFor<glWindowPos2sARB>(ref glWindowPos2sARBDelegate)(x, y);
        }
        public void WindowPos2ARB(short[] v)
        {
            getDelegateFor<glWindowPos2svARB>(ref glWindowPos2svARBDelegate)(v);
        }
        public void WindowPos3ARB(double x, double y, double z)
        {
            getDelegateFor<glWindowPos3dARB>(ref glWindowPos3dARBDelegate)(x, y, z);
        }
        public void WindowPos3ARB(double[] v)
        {
            getDelegateFor<glWindowPos3dvARB>(ref glWindowPos3dvARBDelegate)(v);
        }
        public void WindowPos3ARB(float x, float y, float z)
        {
            getDelegateFor<glWindowPos3fARB>(ref glWindowPos3fARBDelegate)(x, y, z);
        }
        public void WindowPos3ARB(float[] v)
        {
            getDelegateFor<glWindowPos3fvARB>(ref glWindowPos3fvARBDelegate)(v);
        }
        public void WindowPos3ARB(int x, int y, int z)
        {
            getDelegateFor<glWindowPos3iARB>(ref glWindowPos3iARBDelegate)(x, y, z);
        }
        public void WindowPos3ARB(int[] v)
        {
            getDelegateFor<glWindowPos3ivARB>(ref glWindowPos3ivARBDelegate)(v);
        }
        public void WindowPos3ARB(short x, short y, short z)
        {
            getDelegateFor<glWindowPos3sARB>(ref glWindowPos3sARBDelegate)(x, y, z);
        }
        public void WindowPos3ARB(short[] v)
        {
            getDelegateFor<glWindowPos3svARB>(ref glWindowPos3svARBDelegate)(v);
        }

        //  Delegates
        private delegate void glWindowPos2dARB(double x, double y);
		private Delegate glWindowPos2dARBDelegate;
        private delegate void glWindowPos2dvARB(double[] v);
		private Delegate glWindowPos2dvARBDelegate;
        private delegate void glWindowPos2fARB(float x, float y);
		private Delegate glWindowPos2fARBDelegate;
        private delegate void glWindowPos2fvARB(float[] v);
		private Delegate glWindowPos2fvARBDelegate;
        private delegate void glWindowPos2iARB(int x, int y);
		private Delegate glWindowPos2iARBDelegate;
        private delegate void glWindowPos2ivARB(int[] v);
		private Delegate glWindowPos2ivARBDelegate;
        private delegate void glWindowPos2sARB(short x, short y);
		private Delegate glWindowPos2sARBDelegate;
        private delegate void glWindowPos2svARB(short[] v);
		private Delegate glWindowPos2svARBDelegate;
        private delegate void glWindowPos3dARB(double x, double y, double z);
		private Delegate glWindowPos3dARBDelegate;
        private delegate void glWindowPos3dvARB(double[] v);
		private Delegate glWindowPos3dvARBDelegate;
        private delegate void glWindowPos3fARB(float x, float y, float z);
		private Delegate glWindowPos3fARBDelegate;
        private delegate void glWindowPos3fvARB(float[] v);
		private Delegate glWindowPos3fvARBDelegate;
        private delegate void glWindowPos3iARB(int x, int y, int z);
		private Delegate glWindowPos3iARBDelegate;
        private delegate void glWindowPos3ivARB(int[] v);
		private Delegate glWindowPos3ivARBDelegate;
        private delegate void glWindowPos3sARB(short x, short y, short z);
		private Delegate glWindowPos3sARBDelegate;
        private delegate void glWindowPos3svARB(short[] v);
		private Delegate glWindowPos3svARBDelegate;

        #endregion

        #region GL_ARB_vertex_buffer_object

        //  Methods
        public void BindBufferARB(uint target, uint buffer)
        {
            getDelegateFor<glBindBufferARB>(ref glBindBufferARBDelegate)(target, buffer);
        }
        public void DeleteBuffersARB(int n, uint[] buffers)
        {
            getDelegateFor<glDeleteBuffersARB>(ref glDeleteBuffersARBDelegate)(n, buffers);
        }
        public void GenBuffersARB(int n, uint[] buffers)
        {
            getDelegateFor<glGenBuffersARB>(ref glGenBuffersARBDelegate)(n, buffers);
        }
        public bool IsBufferARB(uint buffer)
        {
            return(bool)getDelegateFor<glIsBufferARB>(ref glIsBufferARBDelegate)(buffer);
        }
        public void BufferDataARB(uint target, uint size, IntPtr data, uint usage)
        {
            getDelegateFor<glBufferDataARB>(ref glBufferDataARBDelegate)(target, size, data, usage);
        }
        public void BufferSubDataARB(uint target, uint offset, uint size, IntPtr data)
        {
            getDelegateFor<glBufferSubDataARB>(ref glBufferSubDataARBDelegate)(target, offset, size, data);
        }
        public void GetBufferSubDataARB(uint target, uint offset, uint size, IntPtr data)
        {
            getDelegateFor<glGetBufferSubDataARB>(ref glGetBufferSubDataARBDelegate)(target, offset, size, data);
        }
        public IntPtr MapBufferARB(uint target, uint access)
        {
            return(IntPtr)getDelegateFor<glMapBufferARB>(ref glMapBufferARBDelegate)(target, access);
        }
        public bool UnmapBufferARB(uint target)
        {
            return(bool)getDelegateFor<glUnmapBufferARB>(ref glUnmapBufferARBDelegate)(target);
        }
        public void GetBufferParameterARB(uint target, uint pname, int[] parameters)
        {
            getDelegateFor<glGetBufferParameterivARB>(ref glGetBufferParameterivARBDelegate)(target, pname, parameters);
        }
        public void GetBufferPointerARB(uint target, uint pname, IntPtr parameters)
        {
            getDelegateFor<glGetBufferPointervARB>(ref glGetBufferPointervARBDelegate)(target, pname, parameters);
        }

        //  Delegates
        private delegate void glBindBufferARB(uint target, uint buffer);
		private Delegate glBindBufferARBDelegate;
        private delegate void glDeleteBuffersARB(int n, uint[] buffers);
		private Delegate glDeleteBuffersARBDelegate;
        private delegate void glGenBuffersARB(int n, uint[] buffers);
		private Delegate glGenBuffersARBDelegate;
        private delegate bool glIsBufferARB(uint buffer);
		private Delegate glIsBufferARBDelegate;
        private delegate void glBufferDataARB(uint target, uint size, IntPtr data, uint usage);
		private Delegate glBufferDataARBDelegate;
        private delegate void glBufferSubDataARB(uint target, uint offset, uint size, IntPtr data);
		private Delegate glBufferSubDataARBDelegate;
        private delegate void glGetBufferSubDataARB(uint target, uint offset, uint size, IntPtr data);
		private Delegate glGetBufferSubDataARBDelegate;
        private delegate IntPtr glMapBufferARB(uint target, uint access);
		private Delegate glMapBufferARBDelegate;
        private delegate bool glUnmapBufferARB(uint target);
		private Delegate glUnmapBufferARBDelegate;
        private delegate void glGetBufferParameterivARB(uint target, uint pname, int[] parameters);
		private Delegate glGetBufferParameterivARBDelegate;
        private delegate void glGetBufferPointervARB(uint target, uint pname, IntPtr parameters);
		private Delegate glGetBufferPointervARBDelegate;

        //  Constants
        public const uint GL_BUFFER_SIZE_ARB = 0x8764;
        public const uint GL_BUFFER_USAGE_ARB = 0x8765;
        public const uint GL_ARRAY_BUFFER_ARB = 0x8892;
        public const uint GL_ELEMENT_ARRAY_BUFFER_ARB = 0x8893;
        public const uint GL_ARRAY_BUFFER_BINDING_ARB = 0x8894;
        public const uint GL_ELEMENT_ARRAY_BUFFER_BINDING_ARB = 0x8895;
        public const uint GL_VERTEX_ARRAY_BUFFER_BINDING_ARB = 0x8896;
        public const uint GL_NORMAL_ARRAY_BUFFER_BINDING_ARB = 0x8897;
        public const uint GL_COLOR_ARRAY_BUFFER_BINDING_ARB = 0x8898;
        public const uint GL_INDEX_ARRAY_BUFFER_BINDING_ARB = 0x8899;
        public const uint GL_TEXTURE_COORD_ARRAY_BUFFER_BINDING_ARB = 0x889A;
        public const uint GL_EDGE_FLAG_ARRAY_BUFFER_BINDING_ARB = 0x889B;
        public const uint GL_SECONDARY_COLOR_ARRAY_BUFFER_BINDING_ARB = 0x889C;
        public const uint GL_FOG_COORDINATE_ARRAY_BUFFER_BINDING_ARB = 0x889D;
        public const uint GL_WEIGHT_ARRAY_BUFFER_BINDING_ARB = 0x889E;
        public const uint GL_VERTEX_ATTRIB_ARRAY_BUFFER_BINDING_ARB = 0x889F;
        public const uint GL_READ_ONLY_ARB = 0x88B8;
        public const uint GL_WRITE_ONLY_ARB = 0x88B9;
        public const uint GL_READ_WRITE_ARB = 0x88BA;
        public const uint GL_BUFFER_ACCESS_ARB = 0x88BB;
        public const uint GL_BUFFER_MAPPED_ARB = 0x88BC;
        public const uint GL_BUFFER_MAP_POINTER_ARB = 0x88BD;
        public const uint GL_STREAM_DRAW_ARB = 0x88E0;
        public const uint GL_STREAM_READ_ARB = 0x88E1;
        public const uint GL_STREAM_COPY_ARB = 0x88E2;
        public const uint GL_STATIC_DRAW_ARB = 0x88E4;
        public const uint GL_STATIC_READ_ARB = 0x88E5;
        public const uint GL_STATIC_COPY_ARB = 0x88E6;
        public const uint GL_DYNAMIC_DRAW_ARB = 0x88E8;
        public const uint GL_DYNAMIC_READ_ARB = 0x88E9;
        public const uint GL_DYNAMIC_COPY_ARB = 0x88EA;
        #endregion

        #region GL_ARB_occlusion_query

        //  Methods
        public void GenQueriesARB(int n, uint[] ids)
        {
            getDelegateFor<glGenQueriesARB>(ref glGenQueriesARBDelegate)(n, ids);
        }
        public void DeleteQueriesARB(int n, uint[] ids)
        {
            getDelegateFor<glDeleteQueriesARB>(ref glDeleteQueriesARBDelegate)(n, ids);
        }
        public bool IsQueryARB(uint id)
        {
            return(bool)getDelegateFor<glIsQueryARB>(ref glIsQueryARBDelegate)(id);
        }
        public void BeginQueryARB(uint target, uint id)
        {
            getDelegateFor<glBeginQueryARB>(ref glBeginQueryARBDelegate)(target, id);
        }
        public void EndQueryARB(uint target)
        {
            getDelegateFor<glEndQueryARB>(ref glEndQueryARBDelegate)(target);
        }
        public void GetQueryARB(uint target, uint pname, int[] parameters)
        {
            getDelegateFor<glGetQueryivARB>(ref glGetQueryivARBDelegate)(target, pname, parameters);
        }
        public void GetQueryObjectARB(uint id, uint pname, int[] parameters)
        {
            getDelegateFor<glGetQueryObjectivARB>(ref glGetQueryObjectivARBDelegate)(id, pname, parameters);
        }
        public void GetQueryObjectARB(uint id, uint pname, uint[] parameters)
        {
            getDelegateFor<glGetQueryObjectuivARB>(ref glGetQueryObjectuivARBDelegate)(id, pname, parameters);
        }

        //  Delegates
        private delegate void glGenQueriesARB(int n, uint[] ids);
		private Delegate glGenQueriesARBDelegate;
        private delegate void glDeleteQueriesARB(int n, uint[] ids);
		private Delegate glDeleteQueriesARBDelegate;
        private delegate bool glIsQueryARB(uint id);
		private Delegate glIsQueryARBDelegate;
        private delegate void glBeginQueryARB(uint target, uint id);
		private Delegate glBeginQueryARBDelegate;
        private delegate void glEndQueryARB(uint target);
		private Delegate glEndQueryARBDelegate;
        private delegate void glGetQueryivARB(uint target, uint pname, int[] parameters);
		private Delegate glGetQueryivARBDelegate;
        private delegate void glGetQueryObjectivARB(uint id, uint pname, int[] parameters);
		private Delegate glGetQueryObjectivARBDelegate;
        private delegate void glGetQueryObjectuivARB(uint id, uint pname, uint[] parameters);
		private Delegate glGetQueryObjectuivARBDelegate;

        //  Constants
        public const uint GL_QUERY_COUNTER_BITS_ARB = 0x8864;
        public const uint GL_CURRENT_QUERY_ARB = 0x8865;
        public const uint GL_QUERY_RESULT_ARB = 0x8866;
        public const uint GL_QUERY_RESULT_AVAILABLE_ARB = 0x8867;
        public const uint GL_SAMPLES_PASSED_ARB = 0x8914;
        public const uint GL_ANY_SAMPLES_PASSED = 0x8C2F;

        #endregion

        #region GL_ARB_shader_objects

        //  Methods
        public void DeleteObjectARB(uint obj)
        {
            getDelegateFor<glDeleteObjectARB>(ref glDeleteObjectARBDelegate)(obj);
        }
        public uint GetHandleARB(uint pname)
        {
            return(uint)getDelegateFor<glGetHandleARB>(ref glGetHandleARBDelegate)(pname);
        }
        public void DetachObjectARB(uint containerObj, uint attachedObj)
        {
            getDelegateFor<glDetachObjectARB>(ref glDetachObjectARBDelegate)(containerObj, attachedObj);
        }
        public uint CreateShaderObjectARB(uint shaderType)
        {
            return(uint)getDelegateFor<glCreateShaderObjectARB>(ref glCreateShaderObjectARBDelegate)(shaderType);
        }
        public void ShaderSourceARB(uint shaderObj, int count, string[] source, ref int length)
        {
            getDelegateFor<glShaderSourceARB>(ref glShaderSourceARBDelegate)(shaderObj, count, source, ref length);
        }
        public void CompileShaderARB(uint shaderObj)
        {
            getDelegateFor<glCompileShaderARB>(ref glCompileShaderARBDelegate)(shaderObj);
        }
        public uint CreateProgramObjectARB()
        {
            return(uint)getDelegateFor<glCreateProgramObjectARB>(ref glCreateProgramObjectARBDelegate)();
        }
        public void AttachObjectARB(uint containerObj, uint obj)
        {
            getDelegateFor<glAttachObjectARB>(ref glAttachObjectARBDelegate)(containerObj, obj);
        }
        public void LinkProgramARB(uint programObj)
        {
            getDelegateFor<glLinkProgramARB>(ref glLinkProgramARBDelegate)(programObj);
        }
        public void UseProgramObjectARB(uint programObj)
        {
            getDelegateFor<glUseProgramObjectARB>(ref glUseProgramObjectARBDelegate)(programObj);
        }
        public void ValidateProgramARB(uint programObj)
        {
            getDelegateFor<glValidateProgramARB>(ref glValidateProgramARBDelegate)(programObj);
        }
        public void Uniform1ARB(int location, float v0)
        {
            getDelegateFor<glUniform1fARB>(ref glUniform1fARBDelegate)(location, v0);
        }
        public void Uniform2ARB(int location, float v0, float v1)
        {
            getDelegateFor<glUniform2fARB>(ref glUniform2fARBDelegate)(location, v0, v1);
        }
        public void Uniform3ARB(int location, float v0, float v1, float v2)
        {
            getDelegateFor<glUniform3fARB>(ref glUniform3fARBDelegate)(location, v0, v1, v2);
        }
        public void Uniform4ARB(int location, float v0, float v1, float v2, float v3)
        {
            getDelegateFor<glUniform4fARB>(ref glUniform4fARBDelegate)(location, v0, v1, v2, v3);
        }
        public void Uniform1ARB(int location, int v0)
        {
            getDelegateFor<glUniform1iARB>(ref glUniform1iARBDelegate)(location, v0);
        }
        public void Uniform2ARB(int location, int v0, int v1)
        {
            getDelegateFor<glUniform2iARB>(ref glUniform2iARBDelegate)(location, v0, v1);
        }
        public void Uniform3ARB(int location, int v0, int v1, int v2)
        {
            getDelegateFor<glUniform3iARB>(ref glUniform3iARBDelegate)(location, v0, v1, v2);
        }
        public void Uniform4ARB(int location, int v0, int v1, int v2, int v3)
        {
            getDelegateFor<glUniform4iARB>(ref glUniform4iARBDelegate)(location, v0, v1, v2, v3);
        }
        public void Uniform1ARB(int location, int count, float[] value)
        {
            getDelegateFor<glUniform1fvARB>(ref glUniform1fvARBDelegate)(location, count, value);
        }
        public void Uniform2ARB(int location, int count, float[] value)
        {
            getDelegateFor<glUniform2fvARB>(ref glUniform2fvARBDelegate)(location, count, value);
        }
        public void Uniform3ARB(int location, int count, float[] value)
        {
            getDelegateFor<glUniform3fvARB>(ref glUniform3fvARBDelegate)(location, count, value);
        }
        public void Uniform4ARB(int location, int count, float[] value)
        {
            getDelegateFor<glUniform4fvARB>(ref glUniform4fvARBDelegate)(location, count, value);
        }
        public void Uniform1ARB(int location, int count, int[] value)
        {
            getDelegateFor<glUniform1ivARB>(ref glUniform1ivARBDelegate)(location, count, value);
        }
        public void Uniform2ARB(int location, int count, int[] value)
        {
            getDelegateFor<glUniform2ivARB>(ref glUniform2ivARBDelegate)(location, count, value);
        }
        public void Uniform3ARB(int location, int count, int[] value)
        {
            getDelegateFor<glUniform3ivARB>(ref glUniform3ivARBDelegate)(location, count, value);
        }
        public void Uniform4ARB(int location, int count, int[] value)
        {
            getDelegateFor<glUniform4ivARB>(ref glUniform4ivARBDelegate)(location, count, value);
        }
        public void UniformMatrix2ARB(int location, int count, bool transpose, float[] value)
        {
            getDelegateFor<glUniformMatrix2fvARB>(ref glUniformMatrix2fvARBDelegate)(location, count, transpose, value);
        }
        public void UniformMatrix3ARB(int location, int count, bool transpose, float[] value)
        {
            getDelegateFor<glUniformMatrix3fvARB>(ref glUniformMatrix3fvARBDelegate)(location, count, transpose, value);
        }
        public void UniformMatrix4ARB(int location, int count, bool transpose, float[] value)
        {
            getDelegateFor<glUniformMatrix4fvARB>(ref glUniformMatrix4fvARBDelegate)(location, count, transpose, value);
        }
        public void GetObjectParameterARB(uint obj, uint pname, float[] parameters)
        {
            getDelegateFor<glGetObjectParameterfvARB>(ref glGetObjectParameterfvARBDelegate)(obj, pname, parameters);
        }
        public void GetObjectParameterARB(uint obj, uint pname, int[] parameters)
        {
            getDelegateFor<glGetObjectParameterivARB>(ref glGetObjectParameterivARBDelegate)(obj, pname, parameters);
        }
        public void GetInfoLogARB(uint obj, int maxLength, ref int length, string infoLog)
        {
            getDelegateFor<glGetInfoLogARB>(ref glGetInfoLogARBDelegate)(obj, maxLength, ref length, infoLog);
        }
        public void GetAttachedObjectsARB(uint containerObj, int maxCount, ref int count, ref uint obj)
        {
            getDelegateFor<glGetAttachedObjectsARB>(ref glGetAttachedObjectsARBDelegate)(containerObj, maxCount, ref count, ref obj);
        }
        public int GetUniformLocationARB(uint programObj, string name)
        {
            return(int)getDelegateFor<glGetUniformLocationARB>(ref glGetUniformLocationARBDelegate)(programObj, name);
        }
        public void GetActiveUniformARB(uint programObj, uint index, int maxLength, ref int length, ref int size, ref uint type, string name)
        {
            getDelegateFor<glGetActiveUniformARB>(ref glGetActiveUniformARBDelegate)(programObj, index, maxLength, ref length, ref size, ref type, name);
        }
        public void GetUniformARB(uint programObj, int location, float[] parameters)
        {
            getDelegateFor<glGetUniformfvARB>(ref glGetUniformfvARBDelegate)(programObj, location, parameters);
        }
        public void GetUniformARB(uint programObj, int location, int[] parameters)
        {
            getDelegateFor<glGetUniformivARB>(ref glGetUniformivARBDelegate)(programObj, location, parameters);
        }
        public void GetShaderSourceARB(uint obj, int maxLength, ref int length, string source)
        {
            getDelegateFor<glGetShaderSourceARB>(ref glGetShaderSourceARBDelegate)(obj, maxLength, ref length, source);
        }

        //  Delegates
        private delegate void glDeleteObjectARB(uint obj);
		private Delegate glDeleteObjectARBDelegate;
        private delegate uint glGetHandleARB(uint pname);
		private Delegate glGetHandleARBDelegate;
        private delegate void glDetachObjectARB(uint containerObj, uint attachedObj);
		private Delegate glDetachObjectARBDelegate;
        private delegate uint glCreateShaderObjectARB(uint shaderType);
		private Delegate glCreateShaderObjectARBDelegate;
        private delegate void glShaderSourceARB(uint shaderObj, int count, string[] source, ref int length);
		private Delegate glShaderSourceARBDelegate;
        private delegate void glCompileShaderARB(uint shaderObj);
		private Delegate glCompileShaderARBDelegate;
        private delegate uint glCreateProgramObjectARB();
		private Delegate glCreateProgramObjectARBDelegate;
        private delegate void glAttachObjectARB(uint containerObj, uint obj);
		private Delegate glAttachObjectARBDelegate;
        private delegate void glLinkProgramARB(uint programObj);
		private Delegate glLinkProgramARBDelegate;
        private delegate void glUseProgramObjectARB(uint programObj);
		private Delegate glUseProgramObjectARBDelegate;
        private delegate void glValidateProgramARB(uint programObj);
		private Delegate glValidateProgramARBDelegate;
        private delegate void glUniform1fARB(int location, float v0);
		private Delegate glUniform1fARBDelegate;
        private delegate void glUniform2fARB(int location, float v0, float v1);
		private Delegate glUniform2fARBDelegate;
        private delegate void glUniform3fARB(int location, float v0, float v1, float v2);
		private Delegate glUniform3fARBDelegate;
        private delegate void glUniform4fARB(int location, float v0, float v1, float v2, float v3);
		private Delegate glUniform4fARBDelegate;
        private delegate void glUniform1iARB(int location, int v0);
		private Delegate glUniform1iARBDelegate;
        private delegate void glUniform2iARB(int location, int v0, int v1);
		private Delegate glUniform2iARBDelegate;
        private delegate void glUniform3iARB(int location, int v0, int v1, int v2);
		private Delegate glUniform3iARBDelegate;
        private delegate void glUniform4iARB(int location, int v0, int v1, int v2, int v3);
		private Delegate glUniform4iARBDelegate;
        private delegate void glUniform1fvARB(int location, int count, float[] value);
		private Delegate glUniform1fvARBDelegate;
        private delegate void glUniform2fvARB(int location, int count, float[] value);
		private Delegate glUniform2fvARBDelegate;
        private delegate void glUniform3fvARB(int location, int count, float[] value);
		private Delegate glUniform3fvARBDelegate;
        private delegate void glUniform4fvARB(int location, int count, float[] value);
		private Delegate glUniform4fvARBDelegate;
        private delegate void glUniform1ivARB(int location, int count, int[] value);
		private Delegate glUniform1ivARBDelegate;
        private delegate void glUniform2ivARB(int location, int count, int[] value);
		private Delegate glUniform2ivARBDelegate;
        private delegate void glUniform3ivARB(int location, int count, int[] value);
		private Delegate glUniform3ivARBDelegate;
        private delegate void glUniform4ivARB(int location, int count, int[] value);
		private Delegate glUniform4ivARBDelegate;
        private delegate void glUniformMatrix2fvARB(int location, int count, bool transpose, float[] value);
		private Delegate glUniformMatrix2fvARBDelegate;
        private delegate void glUniformMatrix3fvARB(int location, int count, bool transpose, float[] value);
		private Delegate glUniformMatrix3fvARBDelegate;
        private delegate void glUniformMatrix4fvARB(int location, int count, bool transpose, float[] value);
		private Delegate glUniformMatrix4fvARBDelegate;
        private delegate void glGetObjectParameterfvARB(uint obj, uint pname, float[] parameters);
		private Delegate glGetObjectParameterfvARBDelegate;
        private delegate void glGetObjectParameterivARB(uint obj, uint pname, int[] parameters);
		private Delegate glGetObjectParameterivARBDelegate;
        private delegate void glGetInfoLogARB(uint obj, int maxLength, ref int length, string infoLog);
		private Delegate glGetInfoLogARBDelegate;
        private delegate void glGetAttachedObjectsARB(uint containerObj, int maxCount, ref int count, ref uint obj);
		private Delegate glGetAttachedObjectsARBDelegate;
        private delegate int glGetUniformLocationARB(uint programObj, string name);
		private Delegate glGetUniformLocationARBDelegate;
        private delegate void glGetActiveUniformARB(uint programObj, uint index, int maxLength, ref int length, ref int size, ref uint type, string name);
		private Delegate glGetActiveUniformARBDelegate;
        private delegate void glGetUniformfvARB(uint programObj, int location, float[] parameters);
		private Delegate glGetUniformfvARBDelegate;
        private delegate void glGetUniformivARB(uint programObj, int location, int[] parameters);
		private Delegate glGetUniformivARBDelegate;
        private delegate void glGetShaderSourceARB(uint obj, int maxLength, ref int length, string source);
		private Delegate glGetShaderSourceARBDelegate;

        //  Constants
        public const uint GL_PROGRAM_OBJECT_ARB = 0x8B40;
        public const uint GL_SHADER_OBJECT_ARB = 0x8B48;
        public const uint GL_OBJECT_TYPE_ARB = 0x8B4E;
        public const uint GL_OBJECT_SUBTYPE_ARB = 0x8B4F;
        public const uint GL_FLOAT_VEC2_ARB = 0x8B50;
        public const uint GL_FLOAT_VEC3_ARB = 0x8B51;
        public const uint GL_FLOAT_VEC4_ARB = 0x8B52;
        public const uint GL_INT_VEC2_ARB = 0x8B53;
        public const uint GL_INT_VEC3_ARB = 0x8B54;
        public const uint GL_INT_VEC4_ARB = 0x8B55;
        public const uint GL_BOOL_ARB = 0x8B56;
        public const uint GL_BOOL_VEC2_ARB = 0x8B57;
        public const uint GL_BOOL_VEC3_ARB = 0x8B58;
        public const uint GL_BOOL_VEC4_ARB = 0x8B59;
        public const uint GL_FLOAT_MAT2_ARB = 0x8B5A;
        public const uint GL_FLOAT_MAT3_ARB = 0x8B5B;
        public const uint GL_FLOAT_MAT4_ARB = 0x8B5C;
        public const uint GL_SAMPLER_1D_ARB = 0x8B5D;
        public const uint GL_SAMPLER_2D_ARB = 0x8B5E;
        public const uint GL_SAMPLER_3D_ARB = 0x8B5F;
        public const uint GL_SAMPLER_CUBE_ARB = 0x8B60;
        public const uint GL_SAMPLER_1D_SHADOW_ARB = 0x8B61;
        public const uint GL_SAMPLER_2D_SHADOW_ARB = 0x8B62;
        public const uint GL_SAMPLER_2D_RECT_ARB = 0x8B63;
        public const uint GL_SAMPLER_2D_RECT_SHADOW_ARB = 0x8B64;
        public const uint GL_OBJECT_DELETE_STATUS_ARB = 0x8B80;
        public const uint GL_OBJECT_COMPILE_STATUS_ARB = 0x8B81;
        public const uint GL_OBJECT_LINK_STATUS_ARB = 0x8B82;
        public const uint GL_OBJECT_VALIDATE_STATUS_ARB = 0x8B83;
        public const uint GL_OBJECT_INFO_LOG_LENGTH_ARB = 0x8B84;
        public const uint GL_OBJECT_ATTACHED_OBJECTS_ARB = 0x8B85;
        public const uint GL_OBJECT_ACTIVE_UNIFORMS_ARB = 0x8B86;
        public const uint GL_OBJECT_ACTIVE_UNIFORM_MAX_LENGTH_ARB = 0x8B87;
        public const uint GL_OBJECT_SHADER_SOURCE_LENGTH_ARB = 0x8B88;

        #endregion

        #region GL_ARB_vertex_program

        //  Methods
        public void VertexAttrib1ARB(uint index, double x)
        {
            getDelegateFor<glVertexAttrib1dARB>(ref glVertexAttrib1dARBDelegate)(index, x);
        }
        public void VertexAttrib1ARB(uint index, double[] v)
        {
            getDelegateFor<glVertexAttrib1dvARB>(ref glVertexAttrib1dvARBDelegate)(index, v);
        }
        public void VertexAttrib1ARB(uint index, float x)
        {
            getDelegateFor<glVertexAttrib1fARB>(ref glVertexAttrib1fARBDelegate)(index, x);
        }
        public void VertexAttrib1ARB(uint index, float[] v)
        {
            getDelegateFor<glVertexAttrib1fvARB>(ref glVertexAttrib1fvARBDelegate)(index, v);
        }
        public void VertexAttrib1ARB(uint index, short x)
        {
            getDelegateFor<glVertexAttrib1sARB>(ref glVertexAttrib1sARBDelegate)(index, x);
        }
        public void VertexAttrib1ARB(uint index, short[] v)
        {
            getDelegateFor<glVertexAttrib1svARB>(ref glVertexAttrib1svARBDelegate)(index, v);
        }
        public void VertexAttrib2ARB(uint index, double x, double y)
        {
            getDelegateFor<glVertexAttrib2dARB>(ref glVertexAttrib2dARBDelegate)(index, x, y);
        }
        public void VertexAttrib2ARB(uint index, double[] v)
        {
            getDelegateFor<glVertexAttrib2dvARB>(ref glVertexAttrib2dvARBDelegate)(index, v);
        }
        public void VertexAttrib2ARB(uint index, float x, float y)
        {
            getDelegateFor<glVertexAttrib2fARB>(ref glVertexAttrib2fARBDelegate)(index, x, y);
        }
        public void VertexAttrib2ARB(uint index, float[] v)
        {
            getDelegateFor<glVertexAttrib2fvARB>(ref glVertexAttrib2fvARBDelegate)(index, v);
        }
        public void VertexAttrib2ARB(uint index, short x, short y)
        {
            getDelegateFor<glVertexAttrib2sARB>(ref glVertexAttrib2sARBDelegate)(index, x, y);
        }
        public void VertexAttrib2ARB(uint index, short[] v)
        {
            getDelegateFor<glVertexAttrib2svARB>(ref glVertexAttrib2svARBDelegate)(index, v);
        }
        public void VertexAttrib3ARB(uint index, double x, double y, double z)
        {
            getDelegateFor<glVertexAttrib3dARB>(ref glVertexAttrib3dARBDelegate)(index, x, y, z);
        }
        public void VertexAttrib3ARB(uint index, double[] v)
        {
            getDelegateFor<glVertexAttrib3dvARB>(ref glVertexAttrib3dvARBDelegate)(index, v);
        }
        public void VertexAttrib3ARB(uint index, float x, float y, float z)
        {
            getDelegateFor<glVertexAttrib3fARB>(ref glVertexAttrib3fARBDelegate)(index, x, y, z);
        }
        public void VertexAttrib3ARB(uint index, float[] v)
        {
            getDelegateFor<glVertexAttrib3fvARB>(ref glVertexAttrib3fvARBDelegate)(index, v);
        }
        public void VertexAttrib3ARB(uint index, short x, short y, short z)
        {
            getDelegateFor<glVertexAttrib3sARB>(ref glVertexAttrib3sARBDelegate)(index, x, y, z);
        }
        public void VertexAttrib3ARB(uint index, short[] v)
        {
            getDelegateFor<glVertexAttrib3svARB>(ref glVertexAttrib3svARBDelegate)(index, v);
        }
        public void VertexAttrib4NARB(uint index, sbyte[] v)
        {
            getDelegateFor<glVertexAttrib4NbvARB>(ref glVertexAttrib4NbvARBDelegate)(index, v);
        }
        public void VertexAttrib4NARB(uint index, int[] v)
        {
            getDelegateFor<glVertexAttrib4NivARB>(ref glVertexAttrib4NivARBDelegate)(index, v);
        }
        public void VertexAttrib4NARB(uint index, short[] v)
        {
            getDelegateFor<glVertexAttrib4NsvARB>(ref glVertexAttrib4NsvARBDelegate)(index, v);
        }
        public void VertexAttrib4NARB(uint index, byte x, byte y, byte z, byte w)
        {
            getDelegateFor<glVertexAttrib4NubARB>(ref glVertexAttrib4NubARBDelegate)(index, x, y, z, w);
        }
        public void VertexAttrib4NARB(uint index, byte[] v)
        {
            getDelegateFor<glVertexAttrib4NubvARB>(ref glVertexAttrib4NubvARBDelegate)(index, v);
        }
        public void VertexAttrib4NARB(uint index, uint[] v)
        {
            getDelegateFor<glVertexAttrib4NuivARB>(ref glVertexAttrib4NuivARBDelegate)(index, v);
        }
        public void VertexAttrib4NARB(uint index, ushort[] v)
        {
            getDelegateFor<glVertexAttrib4NusvARB>(ref glVertexAttrib4NusvARBDelegate)(index, v);
        }
        public void VertexAttrib4ARB(uint index, sbyte[] v)
        {
            getDelegateFor<glVertexAttrib4bvARB>(ref glVertexAttrib4bvARBDelegate)(index, v);
        }
        public void VertexAttrib4ARB(uint index, double x, double y, double z, double w)
        {
            getDelegateFor<glVertexAttrib4dARB>(ref glVertexAttrib4dARBDelegate)(index, x, y, z, w);
        }
        public void VertexAttrib4ARB(uint index, double[] v)
        {
            getDelegateFor<glVertexAttrib4dvARB>(ref glVertexAttrib4dvARBDelegate)(index, v);
        }
        public void VertexAttrib4ARB(uint index, float x, float y, float z, float w)
        {
            getDelegateFor<glVertexAttrib4fARB>(ref glVertexAttrib4fARBDelegate)(index, x, y, z, w);
        }
        public void VertexAttrib4ARB(uint index, float[] v)
        {
            getDelegateFor<glVertexAttrib4fvARB>(ref glVertexAttrib4fvARBDelegate)(index, v);
        }
        public void VertexAttrib4ARB(uint index, int[] v)
        {
            getDelegateFor<glVertexAttrib4ivARB>(ref glVertexAttrib4ivARBDelegate)(index, v);
        }
        public void VertexAttrib4ARB(uint index, short x, short y, short z, short w)
        {
            getDelegateFor<glVertexAttrib4sARB>(ref glVertexAttrib4sARBDelegate)(index, x, y, z, w);
        }
        public void VertexAttrib4ARB(uint index, short[] v)
        {
            getDelegateFor<glVertexAttrib4svARB>(ref glVertexAttrib4svARBDelegate)(index, v);
        }
        public void VertexAttrib4ARB(uint index, byte[] v)
        {
            getDelegateFor<glVertexAttrib4ubvARB>(ref glVertexAttrib4ubvARBDelegate)(index, v);
        }
        public void VertexAttrib4ARB(uint index, uint[] v)
        {
            getDelegateFor<glVertexAttrib4uivARB>(ref glVertexAttrib4uivARBDelegate)(index, v);
        }
        public void VertexAttrib4ARB(uint index, ushort[] v)
        {
            getDelegateFor<glVertexAttrib4usvARB>(ref glVertexAttrib4usvARBDelegate)(index, v);
        }
        public void VertexAttribPointerARB(uint index, int size, uint type, bool normalized, int stride, IntPtr pointer)
        {
            getDelegateFor<glVertexAttribPointerARB>(ref glVertexAttribPointerARBDelegate)(index, size, type, normalized, stride, pointer);
        }
        public void EnableVertexAttribArrayARB(uint index)
        {
            getDelegateFor<glEnableVertexAttribArrayARB>(ref glEnableVertexAttribArrayARBDelegate)(index);
        }
        public void DisableVertexAttribArrayARB(uint index)
        {
            getDelegateFor<glDisableVertexAttribArrayARB>(ref glDisableVertexAttribArrayARBDelegate)(index);
        }
        public void ProgramStringARB(uint target, uint format, int len, IntPtr str)
        {
            getDelegateFor<glProgramStringARB>(ref glProgramStringARBDelegate)(target, format, len, str);
        }
        public void BindProgramARB(uint target, uint program)
        {
            getDelegateFor<glBindProgramARB>(ref glBindProgramARBDelegate)(target, program);
        }
        public void DeleteProgramsARB(int n, uint[] programs)
        {
            getDelegateFor<glDeleteProgramsARB>(ref glDeleteProgramsARBDelegate)(n, programs);
        }
        public void GenProgramsARB(int n, uint[] programs)
        {
            getDelegateFor<glGenProgramsARB>(ref glGenProgramsARBDelegate)(n, programs);
        }
        public void ProgramEnvParameter4ARB(uint target, uint index, double x, double y, double z, double w)
        {
            getDelegateFor<glProgramEnvParameter4dARB>(ref glProgramEnvParameter4dARBDelegate)(target, index, x, y, z, w);
        }
        public void ProgramEnvParameter4ARB(uint target, uint index, double[] parameters)
        {
            getDelegateFor<glProgramEnvParameter4dvARB>(ref glProgramEnvParameter4dvARBDelegate)(target, index, parameters);
        }
        public void ProgramEnvParameter4ARB(uint target, uint index, float x, float y, float z, float w)
        {
            getDelegateFor<glProgramEnvParameter4fARB>(ref glProgramEnvParameter4fARBDelegate)(target, index, x, y, z, w);
        }
        public void ProgramEnvParameter4ARB(uint target, uint index, float[] parameters)
        {
            getDelegateFor<glProgramEnvParameter4fvARB>(ref glProgramEnvParameter4fvARBDelegate)(target, index, parameters);
        }
        public void ProgramLocalParameter4ARB(uint target, uint index, double x, double y, double z, double w)
        {
            getDelegateFor<glProgramLocalParameter4dARB>(ref glProgramLocalParameter4dARBDelegate)(target, index, x, y, z, w);
        }
        public void ProgramLocalParameter4ARB(uint target, uint index, double[] parameters)
        {
            getDelegateFor<glProgramLocalParameter4dvARB>(ref glProgramLocalParameter4dvARBDelegate)(target, index, parameters);
        }
        public void ProgramLocalParameter4ARB(uint target, uint index, float x, float y, float z, float w)
        {
            getDelegateFor<glProgramLocalParameter4fARB>(ref glProgramLocalParameter4fARBDelegate)(target, index, x, y, z, w);
        }
        public void ProgramLocalParameter4ARB(uint target, uint index, float[] parameters)
        {
            getDelegateFor<glProgramLocalParameter4fvARB>(ref glProgramLocalParameter4fvARBDelegate)(target, index, parameters);
        }
        public void GetProgramEnvParameterdARB(uint target, uint index, double[] parameters)
        {
            getDelegateFor<glGetProgramEnvParameterdvARB>(ref glGetProgramEnvParameterdvARBDelegate)(target, index, parameters);
        }
        public void GetProgramEnvParameterfARB(uint target, uint index, float[] parameters)
        {
            getDelegateFor<glGetProgramEnvParameterfvARB>(ref glGetProgramEnvParameterfvARBDelegate)(target, index, parameters);
        }
        public void GetProgramLocalParameterARB(uint target, uint index, double[] parameters)
        {
            getDelegateFor<glGetProgramLocalParameterdvARB>(ref glGetProgramLocalParameterdvARBDelegate)(target, index, parameters);
        }
        public void GetProgramLocalParameterARB(uint target, uint index, float[] parameters)
        {
            getDelegateFor<glGetProgramLocalParameterfvARB>(ref glGetProgramLocalParameterfvARBDelegate)(target, index, parameters);
        }
        public void GetProgramARB(uint target, uint pname, int[] parameters)
        {
            getDelegateFor<glGetProgramivARB>(ref glGetProgramivARBDelegate)(target, pname, parameters);
        }
        public void GetProgramStringARB(uint target, uint pname, IntPtr str)
        {
            getDelegateFor<glGetProgramStringARB>(ref glGetProgramStringARBDelegate)(target, pname, str);
        }
        public void GetVertexAttribARB(uint index, uint pname, double[] parameters)
        {
            getDelegateFor<glGetVertexAttribdvARB>(ref glGetVertexAttribdvARBDelegate)(index, pname, parameters);
        }
        public void GetVertexAttribARB(uint index, uint pname, float[] parameters)
        {
            getDelegateFor<glGetVertexAttribfvARB>(ref glGetVertexAttribfvARBDelegate)(index, pname, parameters);
        }
        public void GetVertexAttribARB(uint index, uint pname, int[] parameters)
        {
            getDelegateFor<glGetVertexAttribivARB>(ref glGetVertexAttribivARBDelegate)(index, pname, parameters);
        }
        public void GetVertexAttribPointerARB(uint index, uint pname, IntPtr pointer)
        {
            getDelegateFor<glGetVertexAttribPointervARB>(ref glGetVertexAttribPointervARBDelegate)(index, pname, pointer);
        }

        //  Delegates
        private delegate void glVertexAttrib1dARB(uint index, double x);
		private Delegate glVertexAttrib1dARBDelegate;
        private delegate void glVertexAttrib1dvARB(uint index, double[] v);
		private Delegate glVertexAttrib1dvARBDelegate;
        private delegate void glVertexAttrib1fARB(uint index, float x);
		private Delegate glVertexAttrib1fARBDelegate;
        private delegate void glVertexAttrib1fvARB(uint index, float[] v);
		private Delegate glVertexAttrib1fvARBDelegate;
        private delegate void glVertexAttrib1sARB(uint index, short x);
		private Delegate glVertexAttrib1sARBDelegate;
        private delegate void glVertexAttrib1svARB(uint index, short[] v);
		private Delegate glVertexAttrib1svARBDelegate;
        private delegate void glVertexAttrib2dARB(uint index, double x, double y);
		private Delegate glVertexAttrib2dARBDelegate;
        private delegate void glVertexAttrib2dvARB(uint index, double[] v);
		private Delegate glVertexAttrib2dvARBDelegate;
        private delegate void glVertexAttrib2fARB(uint index, float x, float y);
		private Delegate glVertexAttrib2fARBDelegate;
        private delegate void glVertexAttrib2fvARB(uint index, float[] v);
		private Delegate glVertexAttrib2fvARBDelegate;
        private delegate void glVertexAttrib2sARB(uint index, short x, short y);
		private Delegate glVertexAttrib2sARBDelegate;
        private delegate void glVertexAttrib2svARB(uint index, short[] v);
		private Delegate glVertexAttrib2svARBDelegate;
        private delegate void glVertexAttrib3dARB(uint index, double x, double y, double z);
		private Delegate glVertexAttrib3dARBDelegate;
        private delegate void glVertexAttrib3dvARB(uint index, double[] v);
		private Delegate glVertexAttrib3dvARBDelegate;
        private delegate void glVertexAttrib3fARB(uint index, float x, float y, float z);
		private Delegate glVertexAttrib3fARBDelegate;
        private delegate void glVertexAttrib3fvARB(uint index, float[] v);
		private Delegate glVertexAttrib3fvARBDelegate;
        private delegate void glVertexAttrib3sARB(uint index, short x, short y, short z);
		private Delegate glVertexAttrib3sARBDelegate;
        private delegate void glVertexAttrib3svARB(uint index, short[] v);
		private Delegate glVertexAttrib3svARBDelegate;
        private delegate void glVertexAttrib4NbvARB(uint index, sbyte[] v);
		private Delegate glVertexAttrib4NbvARBDelegate;
        private delegate void glVertexAttrib4NivARB(uint index, int[] v);
		private Delegate glVertexAttrib4NivARBDelegate;
        private delegate void glVertexAttrib4NsvARB(uint index, short[] v);
		private Delegate glVertexAttrib4NsvARBDelegate;
        private delegate void glVertexAttrib4NubARB(uint index, byte x, byte y, byte z, byte w);
		private Delegate glVertexAttrib4NubARBDelegate;
        private delegate void glVertexAttrib4NubvARB(uint index, byte[] v);
		private Delegate glVertexAttrib4NubvARBDelegate;
        private delegate void glVertexAttrib4NuivARB(uint index, uint[] v);
		private Delegate glVertexAttrib4NuivARBDelegate;
        private delegate void glVertexAttrib4NusvARB(uint index, ushort[] v);
		private Delegate glVertexAttrib4NusvARBDelegate;
        private delegate void glVertexAttrib4bvARB(uint index, sbyte[] v);
		private Delegate glVertexAttrib4bvARBDelegate;
        private delegate void glVertexAttrib4dARB(uint index, double x, double y, double z, double w);
		private Delegate glVertexAttrib4dARBDelegate;
        private delegate void glVertexAttrib4dvARB(uint index, double[] v);
		private Delegate glVertexAttrib4dvARBDelegate;
        private delegate void glVertexAttrib4fARB(uint index, float x, float y, float z, float w);
		private Delegate glVertexAttrib4fARBDelegate;
        private delegate void glVertexAttrib4fvARB(uint index, float[] v);
		private Delegate glVertexAttrib4fvARBDelegate;
        private delegate void glVertexAttrib4ivARB(uint index, int[] v);
		private Delegate glVertexAttrib4ivARBDelegate;
        private delegate void glVertexAttrib4sARB(uint index, short x, short y, short z, short w);
		private Delegate glVertexAttrib4sARBDelegate;
        private delegate void glVertexAttrib4svARB(uint index, short[] v);
		private Delegate glVertexAttrib4svARBDelegate;
        private delegate void glVertexAttrib4ubvARB(uint index, byte[] v);
		private Delegate glVertexAttrib4ubvARBDelegate;
        private delegate void glVertexAttrib4uivARB(uint index, uint[] v);
		private Delegate glVertexAttrib4uivARBDelegate;
        private delegate void glVertexAttrib4usvARB(uint index, ushort[] v);
		private Delegate glVertexAttrib4usvARBDelegate;
        private delegate void glVertexAttribPointerARB(uint index, int size, uint type, bool normalized, int stride, IntPtr pointer);
		private Delegate glVertexAttribPointerARBDelegate;
        private delegate void glEnableVertexAttribArrayARB(uint index);
		private Delegate glEnableVertexAttribArrayARBDelegate;
        private delegate void glDisableVertexAttribArrayARB(uint index);
		private Delegate glDisableVertexAttribArrayARBDelegate;
        private delegate void glProgramStringARB(uint target, uint format, int len, IntPtr str);
		private Delegate glProgramStringARBDelegate;
        private delegate void glBindProgramARB(uint target, uint program);
		private Delegate glBindProgramARBDelegate;
        private delegate void glDeleteProgramsARB(int n, uint[] programs);
		private Delegate glDeleteProgramsARBDelegate;
        private delegate void glGenProgramsARB(int n, uint[] programs);
		private Delegate glGenProgramsARBDelegate;
        private delegate void glProgramEnvParameter4dARB(uint target, uint index, double x, double y, double z, double w);
		private Delegate glProgramEnvParameter4dARBDelegate;
        private delegate void glProgramEnvParameter4dvARB(uint target, uint index, double[] parameters);
		private Delegate glProgramEnvParameter4dvARBDelegate;
        private delegate void glProgramEnvParameter4fARB(uint target, uint index, float x, float y, float z, float w);
		private Delegate glProgramEnvParameter4fARBDelegate;
        private delegate void glProgramEnvParameter4fvARB(uint target, uint index, float[] parameters);
		private Delegate glProgramEnvParameter4fvARBDelegate;
        private delegate void glProgramLocalParameter4dARB(uint target, uint index, double x, double y, double z, double w);
		private Delegate glProgramLocalParameter4dARBDelegate;
        private delegate void glProgramLocalParameter4dvARB(uint target, uint index, double[] parameters);
		private Delegate glProgramLocalParameter4dvARBDelegate;
        private delegate void glProgramLocalParameter4fARB(uint target, uint index, float x, float y, float z, float w);
		private Delegate glProgramLocalParameter4fARBDelegate;
        private delegate void glProgramLocalParameter4fvARB(uint target, uint index, float[] parameters);
		private Delegate glProgramLocalParameter4fvARBDelegate;
        private delegate void glGetProgramEnvParameterdvARB(uint target, uint index, double[] parameters);
		private Delegate glGetProgramEnvParameterdvARBDelegate;
        private delegate void glGetProgramEnvParameterfvARB(uint target, uint index, float[] parameters);
		private Delegate glGetProgramEnvParameterfvARBDelegate;
        private delegate void glGetProgramLocalParameterdvARB(uint target, uint index, double[] parameters);
		private Delegate glGetProgramLocalParameterdvARBDelegate;
        private delegate void glGetProgramLocalParameterfvARB(uint target, uint index, float[] parameters);
		private Delegate glGetProgramLocalParameterfvARBDelegate;
        private delegate void glGetProgramivARB(uint target, uint pname, int[] parameters);
		private Delegate glGetProgramivARBDelegate;
        private delegate void glGetProgramStringARB(uint target, uint pname, IntPtr str);
		private Delegate glGetProgramStringARBDelegate;
        private delegate void glGetVertexAttribdvARB(uint index, uint pname, double[] parameters);
		private Delegate glGetVertexAttribdvARBDelegate;
        private delegate void glGetVertexAttribfvARB(uint index, uint pname, float[] parameters);
		private Delegate glGetVertexAttribfvARBDelegate;
        private delegate void glGetVertexAttribivARB(uint index, uint pname, int[] parameters);
		private Delegate glGetVertexAttribivARBDelegate;
        private delegate void glGetVertexAttribPointervARB(uint index, uint pname, IntPtr pointer);
		private Delegate glGetVertexAttribPointervARBDelegate;

        //  Constants
        public const uint GL_COLOR_SUM_ARB = 0x8458;
        public const uint GL_VERTEX_PROGRAM_ARB = 0x8620;
        public const uint GL_VERTEX_ATTRIB_ARRAY_ENABLED_ARB = 0x8622;
        public const uint GL_VERTEX_ATTRIB_ARRAY_SIZE_ARB = 0x8623;
        public const uint GL_VERTEX_ATTRIB_ARRAY_STRIDE_ARB = 0x8624;
        public const uint GL_VERTEX_ATTRIB_ARRAY_TYPE_ARB = 0x8625;
        public const uint GL_CURRENT_VERTEX_ATTRIB_ARB = 0x8626;
        public const uint GL_PROGRAM_LENGTH_ARB = 0x8627;
        public const uint GL_PROGRAM_STRING_ARB = 0x8628;
        public const uint GL_MAX_PROGRAM_MATRIX_STACK_DEPTH_ARB = 0x862E;
        public const uint GL_MAX_PROGRAM_MATRICES_ARB = 0x862F;
        public const uint GL_CURRENT_MATRIX_STACK_DEPTH_ARB = 0x8640;
        public const uint GL_CURRENT_MATRIX_ARB = 0x8641;
        public const uint GL_VERTEX_PROGRAM_POINT_SIZE_ARB = 0x8642;
        public const uint GL_VERTEX_PROGRAM_TWO_SIDE_ARB = 0x8643;
        public const uint GL_VERTEX_ATTRIB_ARRAY_POINTER_ARB = 0x8645;
        public const uint GL_PROGRAM_ERROR_POSITION_ARB = 0x864B;
        public const uint GL_PROGRAM_BINDING_ARB = 0x8677;
        public const uint GL_MAX_VERTEX_ATTRIBS_ARB = 0x8869;
        public const uint GL_VERTEX_ATTRIB_ARRAY_NORMALIZED_ARB = 0x886A;
        public const uint GL_PROGRAM_ERROR_STRING_ARB = 0x8874;
        public const uint GL_PROGRAM_FORMAT_ASCII_ARB = 0x8875;
        public const uint GL_PROGRAM_FORMAT_ARB = 0x8876;
        public const uint GL_PROGRAM_INSTRUCTIONS_ARB = 0x88A0;
        public const uint GL_MAX_PROGRAM_INSTRUCTIONS_ARB = 0x88A1;
        public const uint GL_PROGRAM_NATIVE_INSTRUCTIONS_ARB = 0x88A2;
        public const uint GL_MAX_PROGRAM_NATIVE_INSTRUCTIONS_ARB = 0x88A3;
        public const uint GL_PROGRAM_TEMPORARIES_ARB = 0x88A4;
        public const uint GL_MAX_PROGRAM_TEMPORARIES_ARB = 0x88A5;
        public const uint GL_PROGRAM_NATIVE_TEMPORARIES_ARB = 0x88A6;
        public const uint GL_MAX_PROGRAM_NATIVE_TEMPORARIES_ARB = 0x88A7;
        public const uint GL_PROGRAM_PARAMETERS_ARB = 0x88A8;
        public const uint GL_MAX_PROGRAM_PARAMETERS_ARB = 0x88A9;
        public const uint GL_PROGRAM_NATIVE_PARAMETERS_ARB = 0x88AA;
        public const uint GL_MAX_PROGRAM_NATIVE_PARAMETERS_ARB = 0x88AB;
        public const uint GL_PROGRAM_ATTRIBS_ARB = 0x88AC;
        public const uint GL_MAX_PROGRAM_ATTRIBS_ARB = 0x88AD;
        public const uint GL_PROGRAM_NATIVE_ATTRIBS_ARB = 0x88AE;
        public const uint GL_MAX_PROGRAM_NATIVE_ATTRIBS_ARB = 0x88AF;
        public const uint GL_PROGRAM_ADDRESS_REGISTERS_ARB = 0x88B0;
        public const uint GL_MAX_PROGRAM_ADDRESS_REGISTERS_ARB = 0x88B1;
        public const uint GL_PROGRAM_NATIVE_ADDRESS_REGISTERS_ARB = 0x88B2;
        public const uint GL_MAX_PROGRAM_NATIVE_ADDRESS_REGISTERS_ARB = 0x88B3;
        public const uint GL_MAX_PROGRAM_LOCAL_PARAMETERS_ARB = 0x88B4;
        public const uint GL_MAX_PROGRAM_ENV_PARAMETERS_ARB = 0x88B5;
        public const uint GL_PROGRAM_UNDER_NATIVE_LIMITS_ARB = 0x88B6;
        public const uint GL_TRANSPOSE_CURRENT_MATRIX_ARB = 0x88B7;
        public const uint GL_MATRIX0_ARB = 0x88C0;
        public const uint GL_MATRIX1_ARB = 0x88C1;
        public const uint GL_MATRIX2_ARB = 0x88C2;
        public const uint GL_MATRIX3_ARB = 0x88C3;
        public const uint GL_MATRIX4_ARB = 0x88C4;
        public const uint GL_MATRIX5_ARB = 0x88C5;
        public const uint GL_MATRIX6_ARB = 0x88C6;
        public const uint GL_MATRIX7_ARB = 0x88C7;
        public const uint GL_MATRIX8_ARB = 0x88C8;
        public const uint GL_MATRIX9_ARB = 0x88C9;
        public const uint GL_MATRIX10_ARB = 0x88CA;
        public const uint GL_MATRIX11_ARB = 0x88CB;
        public const uint GL_MATRIX12_ARB = 0x88CC;
        public const uint GL_MATRIX13_ARB = 0x88CD;
        public const uint GL_MATRIX14_ARB = 0x88CE;
        public const uint GL_MATRIX15_ARB = 0x88CF;
        public const uint GL_MATRIX16_ARB = 0x88D0;
        public const uint GL_MATRIX17_ARB = 0x88D1;
        public const uint GL_MATRIX18_ARB = 0x88D2;
        public const uint GL_MATRIX19_ARB = 0x88D3;
        public const uint GL_MATRIX20_ARB = 0x88D4;
        public const uint GL_MATRIX21_ARB = 0x88D5;
        public const uint GL_MATRIX22_ARB = 0x88D6;
        public const uint GL_MATRIX23_ARB = 0x88D7;
        public const uint GL_MATRIX24_ARB = 0x88D8;
        public const uint GL_MATRIX25_ARB = 0x88D9;
        public const uint GL_MATRIX26_ARB = 0x88DA;
        public const uint GL_MATRIX27_ARB = 0x88DB;
        public const uint GL_MATRIX28_ARB = 0x88DC;
        public const uint GL_MATRIX29_ARB = 0x88DD;
        public const uint GL_MATRIX30_ARB = 0x88DE;
        public const uint GL_MATRIX31_ARB = 0x88DF;

        #endregion

        #region GL_ARB_vertex_shader

        //  Methods
        public void BindAttribLocationARB(uint programObj, uint index, string name)
        {
            getDelegateFor<glBindAttribLocationARB>(ref glBindAttribLocationARBDelegate)(programObj, index, name);
        }
        public void GetActiveAttribARB(uint programObj, uint index, int maxLength, int[] length, int[] size, uint[] type, string name)
        {
            getDelegateFor<glGetActiveAttribARB>(ref glGetActiveAttribARBDelegate)(programObj, index, maxLength, length, size, type, name);
        }
        public uint GetAttribLocationARB(uint programObj, string name)
        {
            return(uint)getDelegateFor<glGetAttribLocationARB>(ref glGetAttribLocationARBDelegate)(programObj, name);
        }

        //  Delegates
        private delegate void glBindAttribLocationARB(uint programObj, uint index, string name);
		private Delegate glBindAttribLocationARBDelegate;
        private delegate void glGetActiveAttribARB(uint programObj, uint index, int maxLength, int[] length, int[] size, uint[] type, string name);
		private Delegate glGetActiveAttribARBDelegate;
        private delegate uint glGetAttribLocationARB(uint programObj, string name);
		private Delegate glGetAttribLocationARBDelegate;

        //  Constants
        public const uint GL_VERTEX_SHADER_ARB = 0x8B31;
        public const uint GL_MAX_VERTEX_UNIFORM_COMPONENTS_ARB = 0x8B4A;
        public const uint GL_MAX_VARYING_FLOATS_ARB = 0x8B4B;
        public const uint GL_MAX_VERTEX_TEXTURE_IMAGE_UNITS_ARB = 0x8B4C;
        public const uint GL_MAX_COMBINED_TEXTURE_IMAGE_UNITS_ARB = 0x8B4D;
        public const uint GL_OBJECT_ACTIVE_ATTRIBUTES_ARB = 0x8B89;
        public const uint GL_OBJECT_ACTIVE_ATTRIBUTE_MAX_LENGTH_ARB = 0x8B8A;

        #endregion

        #region GL_ARB_fragment_shader

        public const uint GL_FRAGMENT_SHADER_ARB = 0x8B30;
        public const uint GL_MAX_FRAGMENT_UNIFORM_COMPONENTS_ARB = 0x8B49;
        public const uint GL_FRAGMENT_SHADER_DERIVATIVE_HINT_ARB = 0x8B8B;

        #endregion

        #region GL_ARB_draw_buffers

        //  Methods
        public void DrawBuffersARB(int n, uint[] bufs)
        {
            getDelegateFor<glDrawBuffersARB>(ref glDrawBuffersARBDelegate)(n, bufs);
        }

        //  Delegates
        private delegate void glDrawBuffersARB(int n, uint[] bufs);
		private Delegate glDrawBuffersARBDelegate;

        //  Constants        
        public const uint GL_MAX_DRAW_BUFFERS_ARB = 0x8824;
        public const uint GL_DRAW_BUFFER0_ARB = 0x8825;
        public const uint GL_DRAW_BUFFER1_ARB = 0x8826;
        public const uint GL_DRAW_BUFFER2_ARB = 0x8827;
        public const uint GL_DRAW_BUFFER3_ARB = 0x8828;
        public const uint GL_DRAW_BUFFER4_ARB = 0x8829;
        public const uint GL_DRAW_BUFFER5_ARB = 0x882A;
        public const uint GL_DRAW_BUFFER6_ARB = 0x882B;
        public const uint GL_DRAW_BUFFER7_ARB = 0x882C;
        public const uint GL_DRAW_BUFFER8_ARB = 0x882D;
        public const uint GL_DRAW_BUFFER9_ARB = 0x882E;
        public const uint GL_DRAW_BUFFER10_ARB = 0x882F;
        public const uint GL_DRAW_BUFFER11_ARB = 0x8830;
        public const uint GL_DRAW_BUFFER12_ARB = 0x8831;
        public const uint GL_DRAW_BUFFER13_ARB = 0x8832;
        public const uint GL_DRAW_BUFFER14_ARB = 0x8833;
        public const uint GL_DRAW_BUFFER15_ARB = 0x8834;

        #endregion

        #region GL_ARB_texture_non_power_of_two

        //  No methods or constants

        #endregion

        #region GL_ARB_texture_rectangle

        //  Constants
        public const uint GL_TEXTURE_RECTANGLE_ARB = 0x84F5;
        public const uint GL_TEXTURE_BINDING_RECTANGLE_ARB = 0x84F6;
        public const uint GL_PROXY_TEXTURE_RECTANGLE_ARB = 0x84F7;
        public const uint GL_MAX_RECTANGLE_TEXTURE_SIZE_ARB = 0x84F8;

        #endregion

        #region GL_ARB_point_sprite

        //  Constants
        public const uint GL_POINT_SPRITE_ARB = 0x8861;
        public const uint GL_COORD_REPLACE_ARB = 0x8862;

        #endregion

        #region GL_ARB_texture_float

        //  Constants
        public const uint GL_TEXTURE_RED_TYPE_ARB = 0x8C10;
        public const uint GL_TEXTURE_GREEN_TYPE_ARB = 0x8C11;
        public const uint GL_TEXTURE_BLUE_TYPE_ARB = 0x8C12;
        public const uint GL_TEXTURE_ALPHA_TYPE_ARB = 0x8C13;
        public const uint GL_TEXTURE_LUMINANCE_TYPE_ARB = 0x8C14;
        public const uint GL_TEXTURE_INTENSITY_TYPE_ARB = 0x8C15;
        public const uint GL_TEXTURE_DEPTH_TYPE_ARB = 0x8C16;
        public const uint GL_UNSIGNED_NORMALIZED_ARB = 0x8C17;
        public const uint GL_RGBA32F_ARB = 0x8814;
        public const uint GL_RGB32F_ARB = 0x8815;
        public const uint GL_ALPHA32F_ARB = 0x8816;
        public const uint GL_INTENSITY32F_ARB = 0x8817;
        public const uint GL_LUMINANCE32F_ARB = 0x8818;
        public const uint GL_LUMINANCE_ALPHA32F_ARB = 0x8819;
        public const uint GL_RGBA16F_ARB = 0x881A;
        public const uint GL_RGB16F_ARB = 0x881B;
        public const uint GL_ALPHA16F_ARB = 0x881C;
        public const uint GL_INTENSITY16F_ARB = 0x881D;
        public const uint GL_LUMINANCE16F_ARB = 0x881E;
        public const uint GL_LUMINANCE_ALPHA16F_ARB = 0x881F;

        #endregion

        #region GL_EXT_blend_equation_separate

        //  Methods
        public void BlendEquationSeparateEXT(uint modeRGB, uint modeAlpha)
        {
            getDelegateFor<glBlendEquationSeparateEXT>(ref glBlendEquationSeparateEXTDelegate)(modeRGB, modeAlpha);
        }

        //  Delegates
        private delegate void glBlendEquationSeparateEXT(uint modeRGB, uint modeAlpha);
		private Delegate glBlendEquationSeparateEXTDelegate;

        //  Constants
        public const uint GL_BLEND_EQUATION_RGB_EXT = 0x8009;
        public const uint GL_BLEND_EQUATION_ALPHA_EXT = 0x883D;

        #endregion

        #region GL_EXT_stencil_two_side

        //  Methods
        public void ActiveStencilFaceEXT(uint face)
        {
            getDelegateFor<glActiveStencilFaceEXT>(ref glActiveStencilFaceEXTDelegate)(face);
        }

        //  Delegates
        private delegate void glActiveStencilFaceEXT(uint face);
		private Delegate glActiveStencilFaceEXTDelegate;

        //  Constants
        public const uint GL_STENCIL_TEST_TWO_SIDE_EXT = 0x8009;
        public const uint GL_ACTIVE_STENCIL_FACE_EXT = 0x883D;

        #endregion

        #region GL_ARB_pixel_buffer_object

        public const uint GL_PIXEL_PACK_BUFFER_ARB = 0x88EB;
        public const uint GL_PIXEL_UNPACK_BUFFER_ARB = 0x88EC;
        public const uint GL_PIXEL_PACK_BUFFER_BINDING_ARB = 0x88ED;
        public const uint GL_PIXEL_UNPACK_BUFFER_BINDING_ARB = 0x88EF;

        #endregion

        #region GL_EXT_texture_sRGB

        public const uint GL_SRGB_EXT = 0x8C40;
        public const uint GL_SRGB8_EXT = 0x8C41;
        public const uint GL_SRGB_ALPHA_EXT = 0x8C42;
        public const uint GL_SRGB8_ALPHA8_EXT = 0x8C43;
        public const uint GL_SLUMINANCE_ALPHA_EXT = 0x8C44;
        public const uint GL_SLUMINANCE8_ALPHA8_EXT = 0x8C45;
        public const uint GL_SLUMINANCE_EXT = 0x8C46;
        public const uint GL_SLUMINANCE8_EXT = 0x8C47;
        public const uint GL_COMPRESSED_SRGB_EXT = 0x8C48;
        public const uint GL_COMPRESSED_SRGB_ALPHA_EXT = 0x8C49;
        public const uint GL_COMPRESSED_SLUMINANCE_EXT = 0x8C4A;
        public const uint GL_COMPRESSED_SLUMINANCE_ALPHA_EXT = 0x8C4B;
        public const uint GL_COMPRESSED_SRGB_S3TC_DXT1_EXT = 0x8C4C;
        public const uint GL_COMPRESSED_SRGB_ALPHA_S3TC_DXT1_EXT = 0x8C4D;
        public const uint GL_COMPRESSED_SRGB_ALPHA_S3TC_DXT3_EXT = 0x8C4E;
        public const uint GL_COMPRESSED_SRGB_ALPHA_S3TC_DXT5_EXT = 0x8C4F;

        #endregion

        #region GL_EXT_framebuffer_object

        //  Methods
        public bool IsRenderbufferEXT(uint renderbuffer)
        {
            return(bool)getDelegateFor<glIsRenderbufferEXT>(ref glIsRenderbufferEXTDelegate)(renderbuffer);
        }

        public void BindRenderbufferEXT(uint target, uint renderbuffer)
        {
            getDelegateFor<glBindRenderbufferEXT>(ref glBindRenderbufferEXTDelegate)(target, renderbuffer);
        }

        public void DeleteRenderbuffersEXT(uint n, uint[] renderbuffers)
        {
            getDelegateFor<glDeleteRenderbuffersEXT>(ref glDeleteRenderbuffersEXTDelegate)(n, renderbuffers);
        }

        public void GenRenderbuffersEXT(uint n, uint[] renderbuffers)
        {
            getDelegateFor<glGenRenderbuffersEXT>(ref glGenRenderbuffersEXTDelegate)(n, renderbuffers);
        }

        public void RenderbufferStorageEXT(uint target, uint internalformat, int width, int height)
        {
            getDelegateFor<glRenderbufferStorageEXT>(ref glRenderbufferStorageEXTDelegate)(target, internalformat, width, height);
        }

        public void GetRenderbufferParameterivEXT(uint target, uint pname, int[] parameters)
        {
            getDelegateFor<glGetRenderbufferParameterivEXT>(ref glGetRenderbufferParameterivEXTDelegate)(target, pname, parameters);
        }

        public bool IsFramebufferEXT(uint framebuffer)
        {
            return(bool)getDelegateFor<glIsFramebufferEXT>(ref glIsFramebufferEXTDelegate)(framebuffer);
        }

        public void BindFramebufferEXT(uint target, uint framebuffer)
        {
            getDelegateFor<glBindFramebufferEXT>(ref glBindFramebufferEXTDelegate)(target, framebuffer);
        }

        public void DeleteFramebuffersEXT(uint n, uint[] framebuffers)
        {
            getDelegateFor<glDeleteFramebuffersEXT>(ref glDeleteFramebuffersEXTDelegate)(n, framebuffers);
        }

        public void GenFramebuffersEXT(uint n, uint[] framebuffers)
        {
            getDelegateFor<glGenFramebuffersEXT>(ref glGenFramebuffersEXTDelegate)(n, framebuffers);
        }

        public uint CheckFramebufferStatusEXT(uint target)
        {
            return(uint)getDelegateFor<glCheckFramebufferStatusEXT>(ref glCheckFramebufferStatusEXTDelegate)(target);
        }

        public void FramebufferTexture1DEXT(uint target, uint attachment, uint textarget, uint texture, int level)
        {
            getDelegateFor<glFramebufferTexture1DEXT>(ref glFramebufferTexture1DEXTDelegate)(target, attachment, textarget, texture, level);
        }

        public void FramebufferTexture2DEXT(uint target, uint attachment, uint textarget, uint texture, int level)
        {
            getDelegateFor<glFramebufferTexture2DEXT>(ref glFramebufferTexture2DEXTDelegate)(target, attachment, textarget, texture, level);
        }

        public void FramebufferTexture3DEXT(uint target, uint attachment, uint textarget, uint texture, int level, int zoffset)
        {
            getDelegateFor<glFramebufferTexture3DEXT>(ref glFramebufferTexture3DEXTDelegate)(target, attachment, textarget, texture, level, zoffset);
        }

        public void FramebufferRenderbufferEXT(uint target, uint attachment, uint renderbuffertarget, uint renderbuffer)
        {
            getDelegateFor<glFramebufferRenderbufferEXT>(ref glFramebufferRenderbufferEXTDelegate)(target, attachment, renderbuffertarget, renderbuffer);
        }

        public void GetFramebufferAttachmentParameterivEXT(uint target, uint attachment, uint pname, int[] parameters)
        {
            getDelegateFor<glGetFramebufferAttachmentParameterivEXT>(ref glGetFramebufferAttachmentParameterivEXTDelegate)(target, attachment, pname, parameters);
        }

        public void GenerateMipmapEXT(uint target)
        {
            getDelegateFor<glGenerateMipmapEXT>(ref glGenerateMipmapEXTDelegate)(target);
        }

        //  Delegates
        private delegate bool glIsRenderbufferEXT(uint renderbuffer);
		private Delegate glIsRenderbufferEXTDelegate;
        private delegate void glBindRenderbufferEXT(uint target, uint renderbuffer);
		private Delegate glBindRenderbufferEXTDelegate;
        private delegate void glDeleteRenderbuffersEXT(uint n, uint[] renderbuffers);
		private Delegate glDeleteRenderbuffersEXTDelegate;
        private delegate void glGenRenderbuffersEXT(uint n, uint[] renderbuffers);
		private Delegate glGenRenderbuffersEXTDelegate;
        private delegate void glRenderbufferStorageEXT(uint target, uint internalformat, int width, int height);
		private Delegate glRenderbufferStorageEXTDelegate;
        private delegate void glGetRenderbufferParameterivEXT(uint target, uint pname, int[] parameters);
		private Delegate glGetRenderbufferParameterivEXTDelegate;
        private delegate bool glIsFramebufferEXT(uint framebuffer);
		private Delegate glIsFramebufferEXTDelegate;
        private delegate void glBindFramebufferEXT(uint target, uint framebuffer);
		private Delegate glBindFramebufferEXTDelegate;
        private delegate void glDeleteFramebuffersEXT(uint n, uint[] framebuffers);
		private Delegate glDeleteFramebuffersEXTDelegate;
        private delegate void glGenFramebuffersEXT(uint n, uint[] framebuffers);
		private Delegate glGenFramebuffersEXTDelegate;
        private delegate uint glCheckFramebufferStatusEXT(uint target);
		private Delegate glCheckFramebufferStatusEXTDelegate;
        private delegate void glFramebufferTexture1DEXT(uint target, uint attachment, uint textarget, uint texture, int level);
		private Delegate glFramebufferTexture1DEXTDelegate;
        private delegate void glFramebufferTexture2DEXT(uint target, uint attachment, uint textarget, uint texture, int level);
		private Delegate glFramebufferTexture2DEXTDelegate;
        private delegate void glFramebufferTexture3DEXT(uint target, uint attachment, uint textarget, uint texture, int level, int zoffset);
		private Delegate glFramebufferTexture3DEXTDelegate;
        private delegate void glFramebufferRenderbufferEXT(uint target, uint attachment, uint renderbuffertarget, uint renderbuffer);
		private Delegate glFramebufferRenderbufferEXTDelegate;
        private delegate void glGetFramebufferAttachmentParameterivEXT(uint target, uint attachment, uint pname, int[] parameters);
		private Delegate glGetFramebufferAttachmentParameterivEXTDelegate;
        private delegate void glGenerateMipmapEXT(uint target);
		private Delegate glGenerateMipmapEXTDelegate;

        //  Constants
        public const uint GL_INVALID_FRAMEBUFFER_OPERATION_EXT = 0x0506;
        public const uint GL_MAX_RENDERBUFFER_SIZE_EXT = 0x84E8;
        public const uint GL_FRAMEBUFFER_BINDING_EXT = 0x8CA6;
        public const uint GL_RENDERBUFFER_BINDING_EXT = 0x8CA7;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_OBJECT_TYPE_EXT = 0x8CD0;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_OBJECT_NAME_EXT = 0x8CD1;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_TEXTURE_LEVEL_EXT = 0x8CD2;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_TEXTURE_CUBE_MAP_FACE_EXT = 0x8CD3;
        public const uint GL_FRAMEBUFFER_ATTACHMENT_TEXTURE_3D_ZOFFSET_EXT = 0x8CD4;
        public const uint GL_FRAMEBUFFER_COMPLETE_EXT = 0x8CD5;
        public const uint GL_FRAMEBUFFER_INCOMPLETE_ATTACHMENT_EXT = 0x8CD6;
        public const uint GL_FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT_EXT = 0x8CD7;
        public const uint GL_FRAMEBUFFER_INCOMPLETE_DIMENSIONS_EXT = 0x8CD9;
        public const uint GL_FRAMEBUFFER_INCOMPLETE_FORMATS_EXT = 0x8CDA;
        public const uint GL_FRAMEBUFFER_INCOMPLETE_DRAW_BUFFER_EXT = 0x8CDB;
        public const uint GL_FRAMEBUFFER_INCOMPLETE_READ_BUFFER_EXT = 0x8CDC;
        public const uint GL_FRAMEBUFFER_UNSUPPORTED_EXT = 0x8CDD;
        public const uint GL_MAX_COLOR_ATTACHMENTS_EXT = 0x8CDF;
        public const uint GL_COLOR_ATTACHMENT0_EXT = 0x8CE0;
        public const uint GL_COLOR_ATTACHMENT1_EXT = 0x8CE1;
        public const uint GL_COLOR_ATTACHMENT2_EXT = 0x8CE2;
        public const uint GL_COLOR_ATTACHMENT3_EXT = 0x8CE3;
        public const uint GL_COLOR_ATTACHMENT4_EXT = 0x8CE4;
        public const uint GL_COLOR_ATTACHMENT5_EXT = 0x8CE5;
        public const uint GL_COLOR_ATTACHMENT6_EXT = 0x8CE6;
        public const uint GL_COLOR_ATTACHMENT7_EXT = 0x8CE7;
        public const uint GL_COLOR_ATTACHMENT8_EXT = 0x8CE8;
        public const uint GL_COLOR_ATTACHMENT9_EXT = 0x8CE9;
        public const uint GL_COLOR_ATTACHMENT10_EXT = 0x8CEA;
        public const uint GL_COLOR_ATTACHMENT11_EXT = 0x8CEB;
        public const uint GL_COLOR_ATTACHMENT12_EXT = 0x8CEC;
        public const uint GL_COLOR_ATTACHMENT13_EXT = 0x8CED;
        public const uint GL_COLOR_ATTACHMENT14_EXT = 0x8CEE;
        public const uint GL_COLOR_ATTACHMENT15_EXT = 0x8CEF;
        public const uint GL_DEPTH_ATTACHMENT_EXT = 0x8D00;
        public const uint GL_STENCIL_ATTACHMENT_EXT = 0x8D20;
        public const uint GL_FRAMEBUFFER_EXT = 0x8D40;
        public const uint GL_RENDERBUFFER_EXT = 0x8D41;
        public const uint GL_RENDERBUFFER_WIDTH_EXT = 0x8D42;
        public const uint GL_RENDERBUFFER_HEIGHT_EXT = 0x8D43;
        public const uint GL_RENDERBUFFER_INTERNAL_FORMAT_EXT = 0x8D44;
        public const uint GL_STENCIL_INDEX1_EXT = 0x8D46;
        public const uint GL_STENCIL_INDEX4_EXT = 0x8D47;
        public const uint GL_STENCIL_INDEX8_EXT = 0x8D48;
        public const uint GL_STENCIL_INDEX16_EXT = 0x8D49;
        public const uint GL_RENDERBUFFER_RED_SIZE_EXT = 0x8D50;
        public const uint GL_RENDERBUFFER_GREEN_SIZE_EXT = 0x8D51;
        public const uint GL_RENDERBUFFER_BLUE_SIZE_EXT = 0x8D52;
        public const uint GL_RENDERBUFFER_ALPHA_SIZE_EXT = 0x8D53;
        public const uint GL_RENDERBUFFER_DEPTH_SIZE_EXT = 0x8D54;
        public const uint GL_RENDERBUFFER_STENCIL_SIZE_EXT = 0x8D55;

        #endregion

        #region GL_EXT_framebuffer_blit

        //  Methods
        public void BlitFramebufferEXT(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, uint mask, uint filter)
        {
            getDelegateFor<glBlitFramebufferEXT>(ref glBlitFramebufferEXTDelegate)(srcX0, srcY0, srcX1, srcY1, dstX0, dstY0, dstX1, dstY1, mask, filter);
        }

        //  Delegates
        private delegate void glBlitFramebufferEXT(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, uint mask, uint filter);
		private Delegate glBlitFramebufferEXTDelegate;

        //  Constants
        public const uint GL_READ_FRAMEBUFFER_EXT = 0x8CA8;
        public const uint GL_DRAW_FRAMEBUFFER_EXT = 0x8CA9;

        #endregion

        #region GL_EXT_framebuffer_multisample

        //  Methods
        public void RenderbufferStorageMultisampleEXT(uint target, int samples, uint internalformat, int width, int height)
        {
            getDelegateFor<glRenderbufferStorageMultisampleEXT>(ref glRenderbufferStorageMultisampleEXTDelegate)(target, samples, internalformat, width, height);
        }

        //  Delegates
        private delegate void glRenderbufferStorageMultisampleEXT(uint target, int samples, uint internalformat, int width, int height);
		private Delegate glRenderbufferStorageMultisampleEXTDelegate;

        //  Constants
        public const uint GL_RENDERBUFFER_SAMPLES_EXT = 0x8CAB;
        public const uint GL_FRAMEBUFFER_INCOMPLETE_MULTISAMPLE_EXT = 0x8D56;
        public const uint GL_MAX_SAMPLES_EXT = 0x8D57;

        #endregion

        #region GL_EXT_draw_instanced

        //  Methods
        public void DrawArraysInstancedEXT(uint mode, int start, int count, int primcount)
        {
            getDelegateFor<glDrawArraysInstancedEXT>(ref glDrawArraysInstancedEXTDelegate)(mode, start, count, primcount);
        }
        public void DrawElementsInstancedEXT(uint mode, int count, uint type, IntPtr indices, int primcount)
        {
            getDelegateFor<glDrawElementsInstancedEXT>(ref glDrawElementsInstancedEXTDelegate)(mode, count, type, indices, primcount);
        }

        //  Delegates
        private delegate void glDrawArraysInstancedEXT(uint mode, int start, int count, int primcount);
		private Delegate glDrawArraysInstancedEXTDelegate;
        private delegate void glDrawElementsInstancedEXT(uint mode, int count, uint type, IntPtr indices, int primcount);
		private Delegate glDrawElementsInstancedEXTDelegate;

        #endregion

        #region GL_ARB_vertex_array_object

        //  Methods
        public void BindVertexArray(uint array)
        {
            getDelegateFor<glBindVertexArray>(ref glBindVertexArrayDelegate)(array);
        }
        public void DeleteVertexArrays(int n, uint[] arrays)
        {
            getDelegateFor<glDeleteVertexArrays>(ref glDeleteVertexArraysDelegate)(n, arrays);
        }
        public void GenVertexArrays(int n, uint[] arrays)
        {
            getDelegateFor<glGenVertexArrays>(ref glGenVertexArraysDelegate)(n, arrays);
        }
        public bool IsVertexArray(uint array)
        {
            return(bool)getDelegateFor<glIsVertexArray>(ref glIsVertexArrayDelegate)(array);
        }

        //  Delegates
        private delegate void glBindVertexArray(uint array);
		private Delegate glBindVertexArrayDelegate;
        private delegate void glDeleteVertexArrays(int n, uint[] arrays);
		private Delegate glDeleteVertexArraysDelegate;
        private delegate void glGenVertexArrays(int n, uint[] arrays);
		private Delegate glGenVertexArraysDelegate;
        private delegate bool glIsVertexArray(uint array);
		private Delegate glIsVertexArrayDelegate;

        //  Constants
        public const uint GL_VERTEX_ARRAY_BINDING = 0x85B5;

        #endregion

        #region GL_EXT_framebuffer_sRGB

        //  Constants
        public const uint GL_FRAMEBUFFER_SRGB_EXT = 0x8DB9;
        public const uint GL_FRAMEBUFFER_SRGB_CAPABLE_EXT = 0x8DBA;

        #endregion

        #region GL_EXT_transform_feedback

        //  Methods
        public void BeginTransformFeedbackEXT(uint primitiveMode)
        {
            getDelegateFor<glBeginTransformFeedbackEXT>(ref glBeginTransformFeedbackEXTDelegate)(primitiveMode);
        }
        public void EndTransformFeedbackEXT()
        {
            getDelegateFor<glEndTransformFeedbackEXT>(ref glEndTransformFeedbackEXTDelegate)();
        }
        public void BindBufferRangeEXT(uint target, uint index, uint buffer, int offset, int size)
        {
            getDelegateFor<glBindBufferRangeEXT>(ref glBindBufferRangeEXTDelegate)(target, index, buffer, offset, size);
        }
        public void BindBufferOffsetEXT(uint target, uint index, uint buffer, int offset)
        {
            getDelegateFor<glBindBufferOffsetEXT>(ref glBindBufferOffsetEXTDelegate)(target, index, buffer, offset);
        }
        public void BindBufferBaseEXT(uint target, uint index, uint buffer)
        {
            getDelegateFor<glBindBufferBaseEXT>(ref glBindBufferBaseEXTDelegate)(target, index, buffer);
        }
        public void TransformFeedbackVaryingsEXT(uint program, int count, string[] varyings, uint bufferMode)
        {
            getDelegateFor<glTransformFeedbackVaryingsEXT>(ref glTransformFeedbackVaryingsEXTDelegate)(program, count, varyings, bufferMode);
        }
        public void GetTransformFeedbackVaryingEXT(uint program, uint index, int bufSize, int[] length, int[] size, uint[] type, string name)
        {
            getDelegateFor<glGetTransformFeedbackVaryingEXT>(ref glGetTransformFeedbackVaryingEXTDelegate)(program, index, bufSize, length, size, type, name);
        }

        //  Delegates
        private delegate void glBeginTransformFeedbackEXT(uint primitiveMode);
		private Delegate glBeginTransformFeedbackEXTDelegate;
        private delegate void glEndTransformFeedbackEXT();
		private Delegate glEndTransformFeedbackEXTDelegate;
        private delegate void glBindBufferRangeEXT(uint target, uint index, uint buffer, int offset, int size);
		private Delegate glBindBufferRangeEXTDelegate;
        private delegate void glBindBufferOffsetEXT(uint target, uint index, uint buffer, int offset);
		private Delegate glBindBufferOffsetEXTDelegate;
        private delegate void glBindBufferBaseEXT(uint target, uint index, uint buffer);
		private Delegate glBindBufferBaseEXTDelegate;
        private delegate void glTransformFeedbackVaryingsEXT(uint program, int count, string[] varyings, uint bufferMode);
		private Delegate glTransformFeedbackVaryingsEXTDelegate;
        private delegate void glGetTransformFeedbackVaryingEXT(uint program, uint index, int bufSize, int[] length, int[] size, uint[] type, string name);
		private Delegate glGetTransformFeedbackVaryingEXTDelegate;

        //  Constants
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_EXT                       = 0x8C8E;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_START_EXT                 = 0x8C84;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_SIZE_EXT                  = 0x8C85;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_BINDING_EXT               = 0x8C8F;
        public const uint GL_INTERLEAVED_ATTRIBS_EXT                             = 0x8C8C;
        public const uint GL_SEPARATE_ATTRIBS_EXT                                = 0x8C8D;
        public const uint GL_PRIMITIVES_GENERATED_EXT                            = 0x8C87;
        public const uint GL_TRANSFORM_FEEDBACK_PRIMITIVES_WRITTEN_EXT           = 0x8C88;
        public const uint GL_RASTERIZER_DISCARD_EXT                              = 0x8C89;
        public const uint GL_MAX_TRANSFORM_FEEDBACK_INTERLEAVED_COMPONENTS_EXT   = 0x8C8A;
        public const uint GL_MAX_TRANSFORM_FEEDBACK_SEPARATE_ATTRIBS_EXT         = 0x8C8B;
        public const uint GL_MAX_TRANSFORM_FEEDBACK_SEPARATE_COMPONENTS_EXT      = 0x8C80;
        public const uint GL_TRANSFORM_FEEDBACK_VARYINGS_EXT                     = 0x8C83;
        public const uint GL_TRANSFORM_FEEDBACK_BUFFER_MODE_EXT                  = 0x8C7F;
        public const uint GL_TRANSFORM_FEEDBACK_VARYING_MAX_LENGTH_EXT           = 0x8C76;

        #endregion

        #region GL_ARB_explicit_uniform_location

        //  Constants

        /// <summary>
        /// The number of available pre-assigned uniform locations to that can default be 
        /// allocated in the default uniform block.
        /// </summary>
        public const int GL_MAX_UNIFORM_LOCATIONS = 0x826E;

        #endregion

        #region GL_ARB_clear_buffer_object

        /// <summary>
        /// Fill a buffer object's data store with a fixed value
        /// </summary>
        /// <param name="target">Specifies the target buffer object. The symbolic constant must be GL_ARRAY_BUFFER​, GL_ATOMIC_COUNTER_BUFFER​, GL_COPY_READ_BUFFER​, GL_COPY_WRITE_BUFFER​, GL_DRAW_INDIRECT_BUFFER​, GL_DISPATCH_INDIRECT_BUFFER​, GL_ELEMENT_ARRAY_BUFFER​, GL_PIXEL_PACK_BUFFER​, GL_PIXEL_UNPACK_BUFFER​, GL_QUERY_BUFFER​, GL_SHADER_STORAGE_BUFFER​, GL_TEXTURE_BUFFER​, GL_TRANSFORM_FEEDBACK_BUFFER​, or GL_UNIFORM_BUFFER​.</param>
        /// <param name="internalformat">The sized internal format with which the data will be stored in the buffer object.</param>
        /// <param name="format">Specifies the format of the pixel data. For transfers of depth, stencil, or depth/stencil data, you must use GL_DEPTH_COMPONENT​, GL_STENCIL_INDEX​, or GL_DEPTH_STENCIL​, where appropriate. For transfers of normalized integer or floating-point color image data, you must use one of the following: GL_RED​, GL_GREEN​, GL_BLUE​, GL_RG​, GL_RGB​, GL_BGR​, GL_RGBA​, and GL_BGRA​. For transfers of non-normalized integer data, you must use one of the following: GL_RED_INTEGER​, GL_GREEN_INTEGER​, GL_BLUE_INTEGER​, GL_RG_INTEGER​, GL_RGB_INTEGER​, GL_BGR_INTEGER​, GL_RGBA_INTEGER​, and GL_BGRA_INTEGER​.</param>
        /// <param name="type">Specifies the data type of the pixel data. The following symbolic values are accepted: GL_UNSIGNED_BYTE​, GL_BYTE​, GL_UNSIGNED_SHORT​, GL_SHORT​, GL_UNSIGNED_INT​, GL_INT​, GL_FLOAT​, GL_UNSIGNED_BYTE_3_3_2​, GL_UNSIGNED_BYTE_2_3_3_REV​, GL_UNSIGNED_SHORT_5_6_5​, GL_UNSIGNED_SHORT_5_6_5_REV​, GL_UNSIGNED_SHORT_4_4_4_4​, GL_UNSIGNED_SHORT_4_4_4_4_REV​, GL_UNSIGNED_SHORT_5_5_5_1​, GL_UNSIGNED_SHORT_1_5_5_5_REV​, GL_UNSIGNED_INT_8_8_8_8​, GL_UNSIGNED_INT_8_8_8_8_REV​, GL_UNSIGNED_INT_10_10_10_2​, and GL_UNSIGNED_INT_2_10_10_10_REV​.</param>
        /// <param name="data">Specifies a pointer to a single pixel of data to upload. This parameter may not be NULL.</param>
        public void ClearBufferData(uint target, uint internalformat, uint format, uint type, IntPtr data)
        {
            getDelegateFor<glClearBufferData>(ref glClearBufferDataDelegate)(target, internalformat, format, type, data);
        }

        /// <summary>
        /// Fill all or part of buffer object's data store with a fixed value
        /// </summary>
        /// <param name="target">Specifies the target buffer object. The symbolic constant must be GL_ARRAY_BUFFER​, GL_ATOMIC_COUNTER_BUFFER​, GL_COPY_READ_BUFFER​, GL_COPY_WRITE_BUFFER​, GL_DRAW_INDIRECT_BUFFER​, GL_DISPATCH_INDIRECT_BUFFER​, GL_ELEMENT_ARRAY_BUFFER​, GL_PIXEL_PACK_BUFFER​, GL_PIXEL_UNPACK_BUFFER​, GL_QUERY_BUFFER​, GL_SHADER_STORAGE_BUFFER​, GL_TEXTURE_BUFFER​, GL_TRANSFORM_FEEDBACK_BUFFER​, or GL_UNIFORM_BUFFER​.</param>
        /// <param name="internalformat">The sized internal format with which the data will be stored in the buffer object.</param>
        /// <param name="offset">The offset, in basic machine units into the buffer object's data store at which to start filling.</param>
        /// <param name="size">The size, in basic machine units of the range of the data store to fill.</param>
        /// <param name="format">Specifies the format of the pixel data. For transfers of depth, stencil, or depth/stencil data, you must use GL_DEPTH_COMPONENT​, GL_STENCIL_INDEX​, or GL_DEPTH_STENCIL​, where appropriate. For transfers of normalized integer or floating-point color image data, you must use one of the following: GL_RED​, GL_GREEN​, GL_BLUE​, GL_RG​, GL_RGB​, GL_BGR​, GL_RGBA​, and GL_BGRA​. For transfers of non-normalized integer data, you must use one of the following: GL_RED_INTEGER​, GL_GREEN_INTEGER​, GL_BLUE_INTEGER​, GL_RG_INTEGER​, GL_RGB_INTEGER​, GL_BGR_INTEGER​, GL_RGBA_INTEGER​, and GL_BGRA_INTEGER​.</param>
        /// <param name="type">Specifies the data type of the pixel data. The following symbolic values are accepted: GL_UNSIGNED_BYTE​, GL_BYTE​, GL_UNSIGNED_SHORT​, GL_SHORT​, GL_UNSIGNED_INT​, GL_INT​, GL_FLOAT​, GL_UNSIGNED_BYTE_3_3_2​, GL_UNSIGNED_BYTE_2_3_3_REV​, GL_UNSIGNED_SHORT_5_6_5​, GL_UNSIGNED_SHORT_5_6_5_REV​, GL_UNSIGNED_SHORT_4_4_4_4​, GL_UNSIGNED_SHORT_4_4_4_4_REV​, GL_UNSIGNED_SHORT_5_5_5_1​, GL_UNSIGNED_SHORT_1_5_5_5_REV​, GL_UNSIGNED_INT_8_8_8_8​, GL_UNSIGNED_INT_8_8_8_8_REV​, GL_UNSIGNED_INT_10_10_10_2​, and GL_UNSIGNED_INT_2_10_10_10_REV​.</param>
        /// <param name="data">Specifies a pointer to a single pixel of data to upload. This parameter may not be NULL.</param>
        public void ClearBufferSubData(uint target, uint internalformat, IntPtr offset, uint size, uint format, uint type, IntPtr data)
        {
            getDelegateFor<glClearBufferSubData>(ref glClearBufferSubDataDelegate)(target, internalformat, offset, size, format, type, data);
        }

        public void ClearNamedBufferDataEXT(uint buffer, uint internalformat, uint format, uint type, IntPtr data)
        {
            getDelegateFor<glClearNamedBufferDataEXT>(ref glClearNamedBufferDataEXTDelegate)(buffer, internalformat, format, type, data);
        }
        public void ClearNamedBufferSubDataEXT(uint buffer, uint internalformat, IntPtr offset, uint size, uint format, uint type, IntPtr data)
        {
            getDelegateFor<glClearNamedBufferSubDataEXT>(ref glClearNamedBufferSubDataEXTDelegate)(buffer, internalformat, offset, size, format, type, data);
        }
        
        //  Delegates
        private delegate void glClearBufferData(uint target, uint internalformat, uint format, uint type, IntPtr data);
		private Delegate glClearBufferDataDelegate;
        private delegate void glClearBufferSubData(uint target, uint internalformat, IntPtr offset, uint size, uint format, uint type, IntPtr data);
		private Delegate glClearBufferSubDataDelegate;
        private delegate void glClearNamedBufferDataEXT(uint buffer, uint internalformat, uint format, uint type, IntPtr data);
		private Delegate glClearNamedBufferDataEXTDelegate;
        private delegate void glClearNamedBufferSubDataEXT(uint buffer, uint internalformat, IntPtr offset, uint size, uint format, uint type, IntPtr data);
		private Delegate glClearNamedBufferSubDataEXTDelegate;

        #endregion

        #region GL_ARB_compute_shader

        /// <summary>
        /// Launch one or more compute work groups
        /// </summary>
        /// <param name="num_groups_x">The number of work groups to be launched in the X dimension.</param>
        /// <param name="num_groups_y">The number of work groups to be launched in the Y dimension.</param>
        /// <param name="num_groups_z">The number of work groups to be launched in the Z dimension.</param>
        public void DispatchCompute(uint num_groups_x, uint num_groups_y, uint num_groups_z)
        {
            getDelegateFor<glDispatchCompute>(ref glDispatchComputeDelegate)(num_groups_x, num_groups_y, num_groups_z);
        }

        /// <summary>
        /// Launch one or more compute work groups using parameters stored in a buffer
        /// </summary>
        /// <param name="indirect">The offset into the buffer object currently bound to the GL_DISPATCH_INDIRECT_BUFFER​ buffer target at which the dispatch parameters are stored.</param>
        public void DispatchComputeIndirect(IntPtr indirect)
        {
            getDelegateFor<glDispatchComputeIndirect>(ref glDispatchComputeIndirectDelegate)(indirect);
        }

        //  Delegates
        private delegate void glDispatchCompute(uint num_groups_x, uint num_groups_y, uint num_groups_z);
		private Delegate glDispatchComputeDelegate;
        private delegate void glDispatchComputeIndirect(IntPtr indirect);
		private Delegate glDispatchComputeIndirectDelegate;

        // Constants
        public const uint GL_COMPUTE_SHADER = 0x91B9;
        public const uint GL_MAX_COMPUTE_UNIFORM_BLOCKS = 0x91BB;
        public const uint GL_MAX_COMPUTE_TEXTURE_IMAGE_UNITS = 0x91BC;
        public const uint GL_MAX_COMPUTE_IMAGE_UNIFORMS = 0x91BD;
        public const uint GL_MAX_COMPUTE_SHARED_MEMORY_SIZE = 0x8262;
        public const uint GL_MAX_COMPUTE_UNIFORM_COMPONENTS = 0x8263;
        public const uint GL_MAX_COMPUTE_ATOMIC_COUNTER_BUFFERS = 0x8264;
        public const uint GL_MAX_COMPUTE_ATOMIC_COUNTERS = 0x8265;
        public const uint GL_MAX_COMBINED_COMPUTE_UNIFORM_COMPONENTS = 0x8266;
        public const uint GL_MAX_COMPUTE_WORK_GROUP_INVOCATIONS = 0x90EB;
        public const uint GL_MAX_COMPUTE_WORK_GROUP_COUNT = 0x91BE;
        public const uint GL_MAX_COMPUTE_WORK_GROUP_SIZE = 0x91BF;
        public const uint GL_COMPUTE_WORK_GROUP_SIZE = 0x8267;
        public const uint GL_UNIFORM_BLOCK_REFERENCED_BY_COMPUTE_SHADER = 0x90EC;
        public const uint GL_ATOMIC_COUNTER_BUFFER_REFERENCED_BY_COMPUTE_SHADER = 0x90ED;
        public const uint GL_DISPATCH_INDIRECT_BUFFER = 0x90EE;
        public const uint GL_DISPATCH_INDIRECT_BUFFER_BINDING = 0x90EF;
        public const uint GL_COMPUTE_SHADER_BIT = 0x00000020;

        #endregion

        #region GL_ARB_copy_image

        /// <summary>
        /// Perform a raw data copy between two images
        /// </summary>
        /// <param name="srcName">The name of a texture or renderbuffer object from which to copy.</param>
        /// <param name="srcTarget">The target representing the namespace of the source name srcName​.</param>
        /// <param name="srcLevel">The mipmap level to read from the source.</param>
        /// <param name="srcX">The X coordinate of the left edge of the souce region to copy.</param>
        /// <param name="srcY">The Y coordinate of the top edge of the souce region to copy.</param>
        /// <param name="srcZ">The Z coordinate of the near edge of the souce region to copy.</param>
        /// <param name="dstName">The name of a texture or renderbuffer object to which to copy.</param>
        /// <param name="dstTarget">The target representing the namespace of the destination name dstName​.</param>
        /// <param name="dstLevel">The desination mipmap level.</param>
        /// <param name="dstX">The X coordinate of the left edge of the destination region.</param>
        /// <param name="dstY">The Y coordinate of the top edge of the destination region.</param>
        /// <param name="dstZ">The Z coordinate of the near edge of the destination region.</param>
        /// <param name="srcWidth">The width of the region to be copied.</param>
        /// <param name="srcHeight">The height of the region to be copied.</param>
        /// <param name="srcDepth">The depth of the region to be copied.</param>
        public void CopyImageSubData(uint srcName, uint srcTarget, int srcLevel, int srcX, int srcY, int srcZ, uint dstName,
            uint dstTarget, int dstLevel, int dstX, int dstY, int dstZ, uint srcWidth, uint srcHeight, uint srcDepth)
        {
            getDelegateFor<glCopyImageSubData>(ref glCopyImageSubDataDelegate)(srcName, srcTarget, srcLevel, srcX, srcY, srcZ, dstName, 
            dstTarget, dstLevel, dstX, dstY, dstZ, srcWidth, srcHeight, srcDepth);
        }

        //  Delegates
        private delegate void glCopyImageSubData(uint srcName, uint srcTarget, int srcLevel, int srcX, int srcY, int srcZ, uint dstName, uint dstTarget, int dstLevel, int dstX, int dstY, int dstZ, uint srcWidth, uint srcHeight, uint srcDepth);
		private Delegate glCopyImageSubDataDelegate;

        #endregion

        #region GL_ARB_ES3_compatibility
        
        public const uint GL_COMPRESSED_RGB8_ETC2                          = 0x9274;
        public const uint GL_COMPRESSED_SRGB8_ETC2                         = 0x9275;
        public const uint GL_COMPRESSED_RGB8_PUNCHTHROUGH_ALPHA1_ETC2      = 0x9276;
        public const uint GL_COMPRESSED_SRGB8_PUNCHTHROUGH_ALPHA1_ETC2     = 0x9277;
        public const uint GL_COMPRESSED_RGBA8_ETC2_EAC                     = 0x9278;
        public const uint GL_COMPRESSED_SRGB8_ALPHA8_ETC2_EAC              = 0x9279;
        public const uint GL_COMPRESSED_R11_EAC                            = 0x9270;
        public const uint GL_COMPRESSED_SIGNED_R11_EAC                     = 0x9271;
        public const uint GL_COMPRESSED_RG11_EAC                           = 0x9272;
        public const uint GL_COMPRESSED_SIGNED_RG11_EAC                    = 0x9273;
        public const uint GL_PRIMITIVE_RESTART_FIXED_INDEX                 = 0x8D69;
        public const uint GL_ANY_SAMPLES_PASSED_CONSERVATIVE               = 0x8D6A;
        public const uint GL_MAX_ELEMENT_INDEX                             = 0x8D6B;
        public const uint GL_TEXTURE_IMMUTABLE_LEVELS                      = 0x82DF;

        #endregion

        #region GL_ARB_framebuffer_no_attachments

        //  Methods

        /// <summary>
        /// Set a named parameter of a framebuffer.
        /// </summary>
        /// <param name="target">The target of the operation, which must be GL_READ_FRAMEBUFFER​, GL_DRAW_FRAMEBUFFER​ or GL_FRAMEBUFFER​.</param>
        /// <param name="pname">A token indicating the parameter to be modified.</param>
        /// <param name="param">The new value for the parameter named pname​.</param>
        public void FramebufferParameter(uint target, uint pname, int param)
        {
            getDelegateFor<glFramebufferParameteri>(ref glFramebufferParameteriDelegate)(target, pname, param);
        }

        /// <summary>
        /// Retrieve a named parameter from a framebuffer
        /// </summary>
        /// <param name="target">The target of the operation, which must be GL_READ_FRAMEBUFFER​, GL_DRAW_FRAMEBUFFER​ or GL_FRAMEBUFFER​.</param>
        /// <param name="pname">A token indicating the parameter to be retrieved.</param>
        /// <param name="parameters">The address of a variable to receive the value of the parameter named pname​.</param>
        public void GetFramebufferParameter(uint target, uint pname, int[] parameters)
        {
            getDelegateFor<glGetFramebufferParameteriv>(ref glGetFramebufferParameterivDelegate)(target, pname, parameters);
        }

        public void NamedFramebufferParameterEXT(uint framebuffer, uint pname, int param)
        {
            getDelegateFor<glNamedFramebufferParameteriEXT>(ref glNamedFramebufferParameteriEXTDelegate)(framebuffer, pname, param);
        }

        public void GetNamedFramebufferParameterEXT(uint framebuffer, uint pname, int[] parameters)
        {
            getDelegateFor<glGetNamedFramebufferParameterivEXT>(ref glGetNamedFramebufferParameterivEXTDelegate)(framebuffer, pname, parameters);
        }

        //  Delegates
        private delegate void glFramebufferParameteri(uint target, uint pname, int param);
		private Delegate glFramebufferParameteriDelegate;
        private delegate void glGetFramebufferParameteriv(uint target, uint pname, int[] parameters);
		private Delegate glGetFramebufferParameterivDelegate;
        private delegate void glNamedFramebufferParameteriEXT(uint framebuffer, uint pname, int param);
		private Delegate glNamedFramebufferParameteriEXTDelegate;
        private delegate void glGetNamedFramebufferParameterivEXT(uint framebuffer, uint pname, int[] parameters);
		private Delegate glGetNamedFramebufferParameterivEXTDelegate;
        
        #endregion

        #region GL_ARB_internalformat_query2

        /// <summary>
        /// Retrieve information about implementation-dependent support for internal formats
        /// </summary>
        /// <param name="target">Indicates the usage of the internal format. target​ must be GL_TEXTURE_1D​, GL_TEXTURE_1D_ARRAY​, GL_TEXTURE_2D​, GL_TEXTURE_2D_ARRAY​, GL_TEXTURE_3D​, GL_TEXTURE_CUBE_MAP​, GL_TEXTURE_CUBE_MAP_ARRAY​, GL_TEXTURE_RECTANGLE​, GL_TEXTURE_BUFFER​, GL_RENDERBUFFER​, GL_TEXTURE_2D_MULTISAMPLE​ or GL_TEXTURE_2D_MULTISAMPLE_ARRAY​.</param>
        /// <param name="internalformat">Specifies the internal format about which to retrieve information.</param>
        /// <param name="pname">Specifies the type of information to query.</param>
        /// <param name="bufSize">Specifies the maximum number of basic machine units that may be written to params​ by the function.</param>
        /// <param name="parameters">Specifies the address of a variable into which to write the retrieved information.</param>
        public void GetInternalformat(uint target, uint internalformat, uint pname, uint bufSize, int[] parameters)
        {
            getDelegateFor<glGetInternalformativ>(ref glGetInternalformativDelegate)(target, internalformat, pname, bufSize, parameters);
        }

        /// <summary>
        /// Retrieve information about implementation-dependent support for internal formats
        /// </summary>
        /// <param name="target">Indicates the usage of the internal format. target​ must be GL_TEXTURE_1D​, GL_TEXTURE_1D_ARRAY​, GL_TEXTURE_2D​, GL_TEXTURE_2D_ARRAY​, GL_TEXTURE_3D​, GL_TEXTURE_CUBE_MAP​, GL_TEXTURE_CUBE_MAP_ARRAY​, GL_TEXTURE_RECTANGLE​, GL_TEXTURE_BUFFER​, GL_RENDERBUFFER​, GL_TEXTURE_2D_MULTISAMPLE​ or GL_TEXTURE_2D_MULTISAMPLE_ARRAY​.</param>
        /// <param name="internalformat">Specifies the internal format about which to retrieve information.</param>
        /// <param name="pname">Specifies the type of information to query.</param>
        /// <param name="bufSize">Specifies the maximum number of basic machine units that may be written to params​ by the function.</param>
        /// <param name="parameters">Specifies the address of a variable into which to write the retrieved information.</param>
        public void GetInternalformat(uint target, uint internalformat, uint pname, uint bufSize, Int64[] parameters)
        {
            getDelegateFor<glGetInternalformati64v>(ref glGetInternalformati64vDelegate)(target, internalformat, pname, bufSize, parameters);
        }

        //  Delegates
        private delegate void glGetInternalformativ(uint target, uint internalformat, uint pname, uint bufSize, int[] parameters);
		private Delegate glGetInternalformativDelegate;
        private delegate void glGetInternalformati64v(uint target, uint internalformat, uint pname, uint bufSize, Int64[] parameters);
		private Delegate glGetInternalformati64vDelegate;

        //  Constants
        public const uint GL_RENDERBUFFER                                  =   0x8D41;
        public const uint GL_TEXTURE_2D_MULTISAMPLE                        =   0x9100;
        public const uint GL_TEXTURE_2D_MULTISAMPLE_ARRAY                  =   0x9102;
        public const uint GL_NUM_SAMPLE_COUNTS                              =  0x9380;
        public const uint GL_INTERNALFORMAT_SUPPORTED                       =  0x826F;
        public const uint GL_INTERNALFORMAT_PREFERRED                       =  0x8270;
        public const uint GL_INTERNALFORMAT_RED_SIZE                        =  0x8271;
        public const uint GL_INTERNALFORMAT_GREEN_SIZE                      =  0x8272;
        public const uint GL_INTERNALFORMAT_BLUE_SIZE                       =  0x8273;
        public const uint GL_INTERNALFORMAT_ALPHA_SIZE                      =  0x8274;
        public const uint GL_INTERNALFORMAT_DEPTH_SIZE                      =  0x8275;
        public const uint GL_INTERNALFORMAT_STENCIL_SIZE                    =  0x8276;
        public const uint GL_INTERNALFORMAT_SHARED_SIZE                     =  0x8277;
        public const uint GL_INTERNALFORMAT_RED_TYPE                        =  0x8278;
        public const uint GL_INTERNALFORMAT_GREEN_TYPE                      =  0x8279;
        public const uint GL_INTERNALFORMAT_BLUE_TYPE                       =  0x827A;
        public const uint GL_INTERNALFORMAT_ALPHA_TYPE                      =  0x827B;
        public const uint GL_INTERNALFORMAT_DEPTH_TYPE                      =  0x827C;
        public const uint GL_INTERNALFORMAT_STENCIL_TYPE                    =  0x827D;
        public const uint GL_MAX_WIDTH                                      =  0x827E;
        public const uint GL_MAX_HEIGHT                                     =  0x827F;
        public const uint GL_MAX_DEPTH                                      =  0x8280;
        public const uint GL_MAX_LAYERS                                     =  0x8281;
        public const uint GL_MAX_COMBINED_DIMENSIONS                        =  0x8282;
        public const uint GL_COLOR_COMPONENTS                               =  0x8283;
        public const uint GL_DEPTH_COMPONENTS                               =  0x8284;
        public const uint GL_STENCIL_COMPONENTS                             =  0x8285;
        public const uint GL_COLOR_RENDERABLE                               =  0x8286;
        public const uint GL_DEPTH_RENDERABLE                               =  0x8287;
        public const uint GL_STENCIL_RENDERABLE                             =  0x8288;
        public const uint GL_FRAMEBUFFER_RENDERABLE                         =  0x8289;
        public const uint GL_FRAMEBUFFER_RENDERABLE_LAYERED                 =  0x828A;
        public const uint GL_FRAMEBUFFER_BLEND                              =  0x828B;
        public const uint GL_READ_PIXELS                                    =  0x828C;
        public const uint GL_READ_PIXELS_FORMAT                             =  0x828D;
        public const uint GL_READ_PIXELS_TYPE                               =  0x828E;
        public const uint GL_TEXTURE_IMAGE_FORMAT                           =  0x828F;
        public const uint GL_TEXTURE_IMAGE_TYPE                             =  0x8290;
        public const uint GL_GET_TEXTURE_IMAGE_FORMAT                       =  0x8291;
        public const uint GL_GET_TEXTURE_IMAGE_TYPE                         =  0x8292;
        public const uint GL_MIPMAP                                         =  0x8293;
        public const uint GL_MANUAL_GENERATE_MIPMAP                         =  0x8294;
        public const uint GL_AUTO_GENERATE_MIPMAP                           =  0x8295;
        public const uint GL_COLOR_ENCODING                                 =  0x8296;
        public const uint GL_SRGB_READ                                      =  0x8297;
        public const uint GL_SRGB_WRITE                                     =  0x8298;
        public const uint GL_SRGB_DECODE_ARB                                =  0x8299;
        public const uint GL_FILTER                                         =  0x829A;
        public const uint GL_VERTEX_TEXTURE                                 =  0x829B;
        public const uint GL_TESS_CONTROL_TEXTURE                           =  0x829C;
        public const uint GL_TESS_EVALUATION_TEXTURE                        =  0x829D;
        public const uint GL_GEOMETRY_TEXTURE                               =  0x829E;
        public const uint GL_FRAGMENT_TEXTURE                               =  0x829F;
        public const uint GL_COMPUTE_TEXTURE                                =  0x82A0;
        public const uint GL_TEXTURE_SHADOW                                 =  0x82A1;
        public const uint GL_TEXTURE_GATHER                                 =  0x82A2;
        public const uint GL_TEXTURE_GATHER_SHADOW                          =  0x82A3;
        public const uint GL_SHADER_IMAGE_LOAD                              =  0x82A4;
        public const uint GL_SHADER_IMAGE_STORE                             =  0x82A5;
        public const uint GL_SHADER_IMAGE_ATOMIC                            =  0x82A6;
        public const uint GL_IMAGE_TEXEL_SIZE                               =  0x82A7;
        public const uint GL_IMAGE_COMPATIBILITY_CLASS                      =  0x82A8;
        public const uint GL_IMAGE_PIXEL_FORMAT                             =  0x82A9;
        public const uint GL_IMAGE_PIXEL_TYPE                               =  0x82AA;
        public const uint GL_IMAGE_FORMAT_COMPATIBILITY_TYPE                =  0x90C7;
        public const uint GL_SIMULTANEOUS_TEXTURE_AND_DEPTH_TEST            =  0x82AC;
        public const uint GL_SIMULTANEOUS_TEXTURE_AND_STENCIL_TEST          =  0x82AD;
        public const uint GL_SIMULTANEOUS_TEXTURE_AND_DEPTH_WRITE           =  0x82AE;
        public const uint GL_SIMULTANEOUS_TEXTURE_AND_STENCIL_WRITE         =  0x82AF;
        public const uint GL_TEXTURE_COMPRESSED_BLOCK_WIDTH                 =  0x82B1;
        public const uint GL_TEXTURE_COMPRESSED_BLOCK_HEIGHT                =  0x82B2;
        public const uint GL_TEXTURE_COMPRESSED_BLOCK_SIZE                  =  0x82B3;
        public const uint GL_CLEAR_BUFFER                                   =  0x82B4;
        public const uint GL_TEXTURE_VIEW                                   =  0x82B5;
        public const uint GL_VIEW_COMPATIBILITY_CLASS                       =  0x82B6;
        public const uint GL_FULL_SUPPORT                                   =  0x82B7;
        public const uint GL_CAVEAT_SUPPORT                                 =  0x82B8;
        public const uint GL_IMAGE_CLASS_4_X_32                             =  0x82B9;
        public const uint GL_IMAGE_CLASS_2_X_32                             =  0x82BA;
        public const uint GL_IMAGE_CLASS_1_X_32                             =  0x82BB;
        public const uint GL_IMAGE_CLASS_4_X_16                             =  0x82BC;
        public const uint GL_IMAGE_CLASS_2_X_16                             =  0x82BD;
        public const uint GL_IMAGE_CLASS_1_X_16                             =  0x82BE;
        public const uint GL_IMAGE_CLASS_4_X_8                              =  0x82BF;
        public const uint GL_IMAGE_CLASS_2_X_8                              =  0x82C0;
        public const uint GL_IMAGE_CLASS_1_X_8                              =  0x82C1;
        public const uint GL_IMAGE_CLASS_11_11_10                           =  0x82C2;
        public const uint GL_IMAGE_CLASS_10_10_10_2                         =  0x82C3;
        public const uint GL_VIEW_CLASS_128_BITS                            =  0x82C4;
        public const uint GL_VIEW_CLASS_96_BITS                             =  0x82C5;
        public const uint GL_VIEW_CLASS_64_BITS                             =  0x82C6;
        public const uint GL_VIEW_CLASS_48_BITS                             =  0x82C7;
        public const uint GL_VIEW_CLASS_32_BITS                             =  0x82C8;
        public const uint GL_VIEW_CLASS_24_BITS                             =  0x82C9;
        public const uint GL_VIEW_CLASS_16_BITS                             =  0x82CA;
        public const uint GL_VIEW_CLASS_8_BITS                              =  0x82CB;
        public const uint GL_VIEW_CLASS_S3TC_DXT1_RGB                       =  0x82CC;
        public const uint GL_VIEW_CLASS_S3TC_DXT1_RGBA                      =  0x82CD;
        public const uint GL_VIEW_CLASS_S3TC_DXT3_RGBA                      =  0x82CE;
        public const uint GL_VIEW_CLASS_S3TC_DXT5_RGBA                      =  0x82CF;
        public const uint GL_VIEW_CLASS_RGTC1_RED                           =  0x82D0;
        public const uint GL_VIEW_CLASS_RGTC2_RG                            =  0x82D1;
        public const uint GL_VIEW_CLASS_BPTC_UNORM                          =  0x82D2;
        public const uint GL_VIEW_CLASS_BPTC_FLOAT                          =  0x82D3;
        
        #endregion

        #region GL_ARB_invalidate_subdata

        /// <summary>
        /// Invalidate a region of a texture image
        /// </summary>
        /// <param name="texture">The name of a texture object a subregion of which to invalidate.</param>
        /// <param name="level">The level of detail of the texture object within which the region resides.</param>
        /// <param name="xoffset">The X offset of the region to be invalidated.</param>
        /// <param name="yoffset">The Y offset of the region to be invalidated.</param>
        /// <param name="zoffset">The Z offset of the region to be invalidated.</param>
        /// <param name="width">The width of the region to be invalidated.</param>
        /// <param name="height">The height of the region to be invalidated.</param>
        /// <param name="depth">The depth of the region to be invalidated.</param>
        public void InvalidateTexSubImage(uint texture, int level, int xoffset, int yoffset, int zoffset,
            uint width, uint height, uint depth)
        {
            getDelegateFor<glInvalidateTexSubImage>(ref glInvalidateTexSubImageDelegate)(texture, level, xoffset, yoffset, zoffset, width, height, depth);
        }

        /// <summary>
        /// Invalidate the entirety a texture image
        /// </summary>
        /// <param name="texture">The name of a texture object to invalidate.</param>
        /// <param name="level">The level of detail of the texture object to invalidate.</param>
        public void InvalidateTexImage(uint texture, int level)
        {
            getDelegateFor<glInvalidateTexImage>(ref glInvalidateTexImageDelegate)(texture, level);
        }

        /// <summary>
        /// Invalidate a region of a buffer object's data store
        /// </summary>
        /// <param name="buffer">The name of a buffer object, a subrange of whose data store to invalidate.</param>
        /// <param name="offset">The offset within the buffer's data store of the start of the range to be invalidated.</param>
        /// <param name="length">The length of the range within the buffer's data store to be invalidated.</param>
        public void InvalidateBufferSubData(uint buffer, IntPtr offset, IntPtr length)
        {
            getDelegateFor<glInvalidateBufferSubData>(ref glInvalidateBufferSubDataDelegate)(buffer, offset, length);
        }

        /// <summary>
        /// Invalidate the content of a buffer object's data store
        /// </summary>
        /// <param name="buffer">The name of a buffer object whose data store to invalidate.</param>
        public void InvalidateBufferData(uint buffer)
        {
            getDelegateFor<glInvalidateBufferData>(ref glInvalidateBufferDataDelegate)(buffer);
        }

        /// <summary>
        /// Invalidate the content some or all of a framebuffer object's attachments
        /// </summary>
        /// <param name="target">The target to which the framebuffer is attached. target​ must be GL_FRAMEBUFFER​, GL_DRAW_FRAMEBUFFER​, or GL_READ_FRAMEBUFFER​.</param>
        /// <param name="numAttachments">The number of entries in the attachments​ array.</param>
        /// <param name="attachments">The address of an array identifying the attachments to be invalidated.</param>
        public void InvalidateFramebuffer(uint target, uint numAttachments, uint[] attachments)
        {
            getDelegateFor<glInvalidateFramebuffer>(ref glInvalidateFramebufferDelegate)(target, numAttachments, attachments);
        }

        /// <summary>
        /// Invalidate the content of a region of some or all of a framebuffer object's attachments
        /// </summary>
        /// <param name="target">The target to which the framebuffer is attached. target​ must be GL_FRAMEBUFFER​, GL_DRAW_FRAMEBUFFER​, or GL_READ_FRAMEBUFFER​.</param>
        /// <param name="numAttachments">The number of entries in the attachments​ array.</param>
        /// <param name="attachments">The address of an array identifying the attachments to be invalidated.</param>
        /// <param name="x">The X offset of the region to be invalidated.</param>
        /// <param name="y">The Y offset of the region to be invalidated.</param>
        /// <param name="width">The width of the region to be invalidated.</param>
        /// <param name="height">The height of the region to be invalidated.</param>
        public void InvalidateSubFramebuffer(uint target, uint numAttachments, uint[] attachments,
            int x, int y, uint width, uint height)
        {
            getDelegateFor<glInvalidateSubFramebuffer>(ref glInvalidateSubFramebufferDelegate)(target, numAttachments, attachments, x, y, width, height);
        }

        //  Delegates
        private delegate void glInvalidateTexSubImage(uint texture, int level, int xoffset, int yoffset, int zoffset, uint width, uint height, uint depth);
		private Delegate glInvalidateTexSubImageDelegate;
        private delegate void glInvalidateTexImage(uint texture, int level);
		private Delegate glInvalidateTexImageDelegate;
        private delegate void glInvalidateBufferSubData(uint buffer, IntPtr offset, IntPtr length);
		private Delegate glInvalidateBufferSubDataDelegate;
        private delegate void glInvalidateBufferData(uint buffer);
		private Delegate glInvalidateBufferDataDelegate;
        private delegate void glInvalidateFramebuffer(uint target, uint numAttachments, uint[] attachments);
		private Delegate glInvalidateFramebufferDelegate;
        private delegate void glInvalidateSubFramebuffer(uint target, uint numAttachments, uint[] attachments, int x, int y, uint width, uint height);
		private Delegate glInvalidateSubFramebufferDelegate;

        #endregion

        #region GL_ARB_multi_draw_indirect

        /// <summary>
        /// Render multiple sets of primitives from array data, taking parameters from memory
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render. Symbolic constants GL_POINTS​, GL_LINE_STRIP​, GL_LINE_LOOP​, GL_LINES​, GL_LINE_STRIP_ADJACENCY​, GL_LINES_ADJACENCY​, GL_TRIANGLE_STRIP​, GL_TRIANGLE_FAN​, GL_TRIANGLES​, GL_TRIANGLE_STRIP_ADJACENCY​, GL_TRIANGLES_ADJACENCY​, and GL_PATCHES​ are accepted.</param>
        /// <param name="indirect">Specifies the address of an array of structures containing the draw parameters.</param>
        /// <param name="primcount">Specifies the the number of elements in the array of draw parameter structures.</param>
        /// <param name="stride">Specifies the distance in basic machine units between elements of the draw parameter array.</param>
        public void MultiDrawArraysIndirect(uint mode, IntPtr indirect, uint primcount, uint stride)
        {
            getDelegateFor<glMultiDrawArraysIndirect>(ref glMultiDrawArraysIndirectDelegate)(mode, indirect, primcount, stride);
        }

        /// <summary>
        /// Render indexed primitives from array data, taking parameters from memory
        /// </summary>
        /// <param name="mode">Specifies what kind of primitives to render. Symbolic constants GL_POINTS​, GL_LINE_STRIP​, GL_LINE_LOOP​, GL_LINES​, GL_LINE_STRIP_ADJACENCY​, GL_LINES_ADJACENCY​, GL_TRIANGLE_STRIP​, GL_TRIANGLE_FAN​, GL_TRIANGLES​, GL_TRIANGLE_STRIP_ADJACENCY​, GL_TRIANGLES_ADJACENCY​, and GL_PATCHES​ are accepted.</param>
        /// <param name="type">Specifies the type of data in the buffer bound to the GL_ELEMENT_ARRAY_BUFFER​ binding.</param>
        /// <param name="indirect">Specifies a byte offset(cast to a pointer type) into the buffer bound to GL_DRAW_INDIRECT_BUFFER​, which designates the starting point of the structure containing the draw parameters.</param>
        /// <param name="primcount">Specifies the number of elements in the array addressed by indirect​.</param>
        /// <param name="stride">Specifies the distance in basic machine units between elements of the draw parameter array.</param>
        public void MultiDrawElementsIndirect(uint mode, uint type, IntPtr indirect, uint primcount, uint stride)
        {
            getDelegateFor<glMultiDrawElementsIndirect>(ref glMultiDrawElementsIndirectDelegate)(mode, type, indirect, primcount, stride);
        }

        private delegate void glMultiDrawArraysIndirect(uint mode, IntPtr indirect, uint primcount, uint stride);
		private Delegate glMultiDrawArraysIndirectDelegate;
        private delegate void glMultiDrawElementsIndirect(uint mode, uint type, IntPtr indirect, uint primcount, uint stride);
		private Delegate glMultiDrawElementsIndirectDelegate;

        #endregion

        #region GL_ARB_program_interface_query

        /// <summary>
        /// Query a property of an interface in a program
        /// </summary>
        /// <param name="program">The name of a program object whose interface to query.</param>
        /// <param name="programInterface">A token identifying the interface within program​ to query.</param>
        /// <param name="pname">The name of the parameter within programInterface​ to query.</param>
        /// <param name="parameters">The address of a variable to retrieve the value of pname​ for the program interface..</param>
        public void GetProgramInterface(uint program, uint programInterface, uint pname, int[] parameters)
        {
            getDelegateFor<glGetProgramInterfaceiv>(ref glGetProgramInterfaceivDelegate)(program, programInterface, pname, parameters);
        }

        /// <summary>
        /// Query the index of a named resource within a program
        /// </summary>
        /// <param name="program">The name of a program object whose resources to query.</param>
        /// <param name="programInterface">A token identifying the interface within program​ containing the resource named name​.</param>
        /// <param name="name">The name of the resource to query the index of.</param>
        public void GetProgramResourceIndex(uint program, uint programInterface, string name)
        {
            getDelegateFor<glGetProgramResourceIndex>(ref glGetProgramResourceIndexDelegate)(program, programInterface, name);
        }

        /// <summary>
        /// Query the name of an indexed resource within a program
        /// </summary>
        /// <param name="program">The name of a program object whose resources to query.</param>
        /// <param name="programInterface">A token identifying the interface within program​ containing the indexed resource.</param>
        /// <param name="index">The index of the resource within programInterface​ of program​.</param>
        /// <param name="bufSize">The size of the character array whose address is given by name​.</param>
        /// <param name="length">The address of a variable which will receive the length of the resource name.</param>
        /// <param name="name">The address of a character array into which will be written the name of the resource.</param>
        public void GetProgramResourceName(uint program, uint programInterface, uint index, uint bufSize, out uint length, out string name)
        {
            var lengthParameter = new uint[1];
            var nameParameter = new string[1];
            getDelegateFor<glGetProgramResourceName>(ref glGetProgramResourceNameDelegate)(program, programInterface, index, bufSize, lengthParameter, nameParameter);
            length = lengthParameter[0];
            name = nameParameter[0];
        }

        /// <summary>
        /// Retrieve values for multiple properties of a single active resource within a program object
        /// </summary>
        /// <param name="program">The name of a program object whose resources to query.</param>
        /// <param name="programInterface">A token identifying the interface within program​ containing the resource named name​.</param>
        /// <param name="index">The index within the programInterface​ to query information about.</param>
        /// <param name="propCount">The number of properties being queried.</param>
        /// <param name="props">An array of properties of length propCount​ to query.</param>
        /// <param name="bufSize">The number of GLint values in the params​ array.</param>
        /// <param name="length">If not NULL, then this value will be filled in with the number of actual parameters written to params​.</param>
        /// <param name="parameters">The output array of parameters to write.</param>
        public void GetProgramResource(uint program, uint programInterface, uint index, uint propCount, uint[] props, uint bufSize, out uint length, out int[] parameters)
        {
            var lengthParameter = new uint[1];
            var parametersParameter = new int[bufSize];

            getDelegateFor<glGetProgramResourceiv>(ref glGetProgramResourceivDelegate)(program, programInterface, index, propCount, props, bufSize, lengthParameter, parametersParameter);
            length = lengthParameter[0];
            parameters = parametersParameter;
        }

        /// <summary>
        /// Query the location of a named resource within a program.
        /// </summary>
        /// <param name="program">The name of a program object whose resources to query.</param>
        /// <param name="programInterface">A token identifying the interface within program​ containing the resource named name​.</param>
        /// <param name="name">The name of the resource to query the location of.</param>
        public void GetProgramResourceLocation(uint program, uint programInterface, string name)
        {
            getDelegateFor<glGetProgramResourceLocation>(ref glGetProgramResourceLocationDelegate)(program, programInterface, name);
        }

        /// <summary>
        /// Query the fragment color index of a named variable within a program.
        /// </summary>
        /// <param name="program">The name of a program object whose resources to query.</param>
        /// <param name="programInterface">A token identifying the interface within program​ containing the resource named name​.</param>
        /// <param name="name">The name of the resource to query the location of.</param>
        public void GetProgramResourceLocationIndex(uint program, uint programInterface, string name)
        {
            getDelegateFor<glGetProgramResourceLocationIndex>(ref glGetProgramResourceLocationIndexDelegate)(program, programInterface, name);
        }

        private delegate void glGetProgramInterfaceiv(uint program, uint programInterface, uint pname, int[] parameters);
		private Delegate glGetProgramInterfaceivDelegate;
        private delegate uint glGetProgramResourceIndex(uint program, uint programInterface, string name);
		private Delegate glGetProgramResourceIndexDelegate;
        private delegate void glGetProgramResourceName(uint program, uint programInterface, uint index, uint bufSize, uint[] length, string[] name);
		private Delegate glGetProgramResourceNameDelegate;
        private delegate void glGetProgramResourceiv(uint program, uint programInterface, uint index, uint propCount, uint[] props, uint bufSize, uint[] length, int[] parameters);
		private Delegate glGetProgramResourceivDelegate;
        private delegate int glGetProgramResourceLocation(uint program, uint programInterface, string name);
		private Delegate glGetProgramResourceLocationDelegate;
        private delegate int glGetProgramResourceLocationIndex(uint program, uint programInterface, string name);
		private Delegate glGetProgramResourceLocationIndexDelegate;

        #endregion

        #region GL_ARB_shader_storage_buffer_object

        /// <summary>
        /// Change an active shader storage block binding.
        /// </summary>
        /// <param name="program">The name of the program containing the block whose binding to change.</param>
        /// <param name="storageBlockIndex">The index storage block within the program.</param>
        /// <param name="storageBlockBinding">The index storage block binding to associate with the specified storage block.</param>
        public void ShaderStorageBlockBinding(uint program, uint storageBlockIndex, uint storageBlockBinding)
        {
            getDelegateFor<glShaderStorageBlockBinding>(ref glShaderStorageBlockBindingDelegate)(program, storageBlockIndex, storageBlockBinding);
        }

        private delegate void glShaderStorageBlockBinding(uint program, uint storageBlockIndex, uint storageBlockBinding);
		private Delegate glShaderStorageBlockBindingDelegate;

        //  Constants
        public const uint GL_SHADER_STORAGE_BUFFER                         = 0x90D2;
        public const uint GL_SHADER_STORAGE_BUFFER_BINDING                 = 0x90D3;
        public const uint GL_SHADER_STORAGE_BUFFER_START                   = 0x90D4;
        public const uint GL_SHADER_STORAGE_BUFFER_SIZE                    = 0x90D5;
        public const uint GL_MAX_VERTEX_SHADER_STORAGE_BLOCKS              = 0x90D6;
        public const uint GL_MAX_GEOMETRY_SHADER_STORAGE_BLOCKS            = 0x90D7;
        public const uint GL_MAX_TESS_CONTROL_SHADER_STORAGE_BLOCKS        = 0x90D8;
        public const uint GL_MAX_TESS_EVALUATION_SHADER_STORAGE_BLOCKS     = 0x90D9;
        public const uint GL_MAX_FRAGMENT_SHADER_STORAGE_BLOCKS            = 0x90DA;
        public const uint GL_MAX_COMPUTE_SHADER_STORAGE_BLOCKS             = 0x90DB;
        public const uint GL_MAX_COMBINED_SHADER_STORAGE_BLOCKS            = 0x90DC;
        public const uint GL_MAX_SHADER_STORAGE_BUFFER_BINDINGS            = 0x90DD;
        public const uint GL_MAX_SHADER_STORAGE_BLOCK_SIZE                 = 0x90DE;
        public const uint GL_SHADER_STORAGE_BUFFER_OFFSET_ALIGNMENT        = 0x90DF;
        public const uint GL_SHADER_STORAGE_BARRIER_BIT                    = 0x2000;       
        public const uint GL_MAX_COMBINED_SHADER_OUTPUT_RESOURCES          = 0x8F39;

        #endregion

        #region GL_ARB_stencil_texturing

        //  Constants
        public const uint GL_DEPTH_STENCIL_TEXTURE_MODE = 0x90EA;

        #endregion

        #region GL_ARB_texture_buffer_range

        /// <summary>
        /// Bind a range of a buffer's data store to a buffer texture
        /// </summary>
        /// <param name="target">Specifies the target of the operation and must be GL_TEXTURE_BUFFER​.</param>
        /// <param name="internalformat">Specifies the internal format of the data in the store belonging to buffer​.</param>
        /// <param name="buffer">Specifies the name of the buffer object whose storage to attach to the active buffer texture.</param>
        /// <param name="offset">Specifies the offset of the start of the range of the buffer's data store to attach.</param>
        /// <param name="size">Specifies the size of the range of the buffer's data store to attach.</param>
        public void TexBufferRange(uint target, uint internalformat, uint buffer, IntPtr offset, IntPtr size)
        {
            getDelegateFor<glTexBufferRange>(ref glTexBufferRangeDelegate)(target, internalformat, buffer, offset, size);
        }

        /// <summary>
        /// Bind a range of a buffer's data store to a buffer texture
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="target">Specifies the target of the operation and must be GL_TEXTURE_BUFFER​.</param>
        /// <param name="internalformat">Specifies the internal format of the data in the store belonging to buffer​.</param>
        /// <param name="buffer">Specifies the name of the buffer object whose storage to attach to the active buffer texture.</param>
        /// <param name="offset">Specifies the offset of the start of the range of the buffer's data store to attach.</param>
        /// <param name="size">Specifies the size of the range of the buffer's data store to attach.</param>
        public void TextureBufferRangeEXT(uint texture, uint target, uint internalformat, uint buffer, IntPtr offset, IntPtr size)
        {
            getDelegateFor<glTextureBufferRangeEXT>(ref glTextureBufferRangeEXTDelegate)(texture, target, internalformat, buffer, offset, size);
        }

        private delegate void glTexBufferRange(uint target, uint internalformat, uint buffer, IntPtr offset, IntPtr size);
		private Delegate glTexBufferRangeDelegate;
        private delegate void glTextureBufferRangeEXT(uint texture, uint target, uint internalformat, uint buffer, IntPtr offset, IntPtr size);
		private Delegate glTextureBufferRangeEXTDelegate;

        #endregion

        #region GL_ARB_texture_storage_multisample

        /// <summary>
        /// Specify storage for a two-dimensional multisample texture.
        /// </summary>
        /// <param name="target">Specify the target of the operation. target​ must be GL_TEXTURE_2D_MULTISAMPLE​ or GL_PROXY_TEXTURE_2D_MULTISAMPLE​.</param>
        /// <param name="samples">Specify the number of samples in the texture.</param>
        /// <param name="internalformat">Specifies the sized internal format to be used to store texture image data.</param>
        /// <param name="width">Specifies the width of the texture, in texels.</param>
        /// <param name="height">Specifies the height of the texture, in texels.</param>
        /// <param name="fixedsamplelocations">Specifies whether the image will use identical sample locations and the same number of samples for all texels in the image, and the sample locations will not depend on the internal format or size of the image.</param>
        public void TexStorage2DMultisample(uint target, uint samples, uint internalformat, uint width, uint height, bool fixedsamplelocations)
        {
            getDelegateFor<glTexStorage2DMultisample>(ref glTexStorage2DMultisampleDelegate)(target, samples, internalformat, width, height, fixedsamplelocations);
        }

        /// <summary>
        /// Specify storage for a three-dimensional multisample array texture
        /// </summary>
        /// <param name="target">Specify the target of the operation. target​ must be GL_TEXTURE_3D_MULTISAMPLE_ARRAY​ or GL_PROXY_TEXTURE_3D_MULTISAMPLE_ARRAY​.</param>
        /// <param name="samples">Specify the number of samples in the texture.</param>
        /// <param name="internalformat">Specifies the sized internal format to be used to store texture image data.</param>
        /// <param name="width">Specifies the width of the texture, in texels.</param>
        /// <param name="height">Specifies the height of the texture, in texels.</param>
        /// <param name="depth">Specifies the depth of the texture, in layers.</param>
        /// <param name="fixedsamplelocations">Specifies the depth of the texture, in layers.</param>
        public void TexStorage3DMultisample(uint target, uint samples, uint internalformat, uint width, uint height, uint depth, bool fixedsamplelocations)
        {
            getDelegateFor<glTexStorage3DMultisample>(ref glTexStorage3DMultisampleDelegate)(target, samples, internalformat, width, height, depth, fixedsamplelocations);
        }

        /// <summary>
        /// Specify storage for a two-dimensional multisample texture.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="target">Specify the target of the operation. target​ must be GL_TEXTURE_2D_MULTISAMPLE​ or GL_PROXY_TEXTURE_2D_MULTISAMPLE​.</param>
        /// <param name="samples">Specify the number of samples in the texture.</param>
        /// <param name="internalformat">Specifies the sized internal format to be used to store texture image data.</param>
        /// <param name="width">Specifies the width of the texture, in texels.</param>
        /// <param name="height">Specifies the height of the texture, in texels.</param>
        /// <param name="fixedsamplelocations">Specifies whether the image will use identical sample locations and the same number of samples for all texels in the image, and the sample locations will not depend on the internal format or size of the image.</param>
        public void TexStorage2DMultisampleEXT(uint texture, uint target, uint samples, uint internalformat, uint width, uint height, bool fixedsamplelocations)
        {
            getDelegateFor<glTexStorage2DMultisampleEXT>(ref glTexStorage2DMultisampleEXTDelegate)(texture, target, samples, internalformat, width, height, fixedsamplelocations);
        }

        /// <summary>
        /// Specify storage for a three-dimensional multisample array texture
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="target">Specify the target of the operation. target​ must be GL_TEXTURE_3D_MULTISAMPLE_ARRAY​ or GL_PROXY_TEXTURE_3D_MULTISAMPLE_ARRAY​.</param>
        /// <param name="samples">Specify the number of samples in the texture.</param>
        /// <param name="internalformat">Specifies the sized internal format to be used to store texture image data.</param>
        /// <param name="width">Specifies the width of the texture, in texels.</param>
        /// <param name="height">Specifies the height of the texture, in texels.</param>
        /// <param name="depth">Specifies the depth of the texture, in layers.</param>
        /// <param name="fixedsamplelocations">Specifies the depth of the texture, in layers.</param>
        public void TexStorage3DMultisampleEXT(uint texture, uint target, uint samples, uint internalformat, uint width, uint height, uint depth, bool fixedsamplelocations)
        {
            getDelegateFor<glTexStorage3DMultisampleEXT>(ref glTexStorage3DMultisampleEXTDelegate)(texture, target, samples, internalformat, width, height, depth, fixedsamplelocations);
        }

        //  Delegates
        private delegate void glTexStorage2DMultisample(uint target, uint samples, uint internalformat, uint width, uint height, bool fixedsamplelocations);
		private Delegate glTexStorage2DMultisampleDelegate;
        private delegate void glTexStorage3DMultisample(uint target, uint samples, uint internalformat, uint width, uint height, uint depth, bool fixedsamplelocations);
		private Delegate glTexStorage3DMultisampleDelegate;
        private delegate void glTexStorage2DMultisampleEXT(uint texture, uint target, uint samples, uint internalformat, uint width, uint height, bool fixedsamplelocations);
		private Delegate glTexStorage2DMultisampleEXTDelegate;
        private delegate void glTexStorage3DMultisampleEXT(uint texture, uint target, uint samples, uint internalformat, uint width, uint height, uint depth, bool fixedsamplelocations);
		private Delegate glTexStorage3DMultisampleEXTDelegate;

        #endregion

        #region GL_ARB_texture_view

        /// <summary>
        /// Initialize a texture as a data alias of another texture's data store.
        /// </summary>
        /// <param name="texture">Specifies the texture object to be initialized as a view.</param>
        /// <param name="target">Specifies the target to be used for the newly initialized texture.</param>
        /// <param name="origtexture">Specifies the name of a texture object of which to make a view.</param>
        /// <param name="internalformat">Specifies the internal format for the newly created view.</param>
        /// <param name="minlevel">Specifies lowest level of detail of the view.</param>
        /// <param name="numlevels">Specifies the number of levels of detail to include in the view.</param>
        /// <param name="minlayer">Specifies the index of the first layer to include in the view.</param>
        /// <param name="numlayers">Specifies the number of layers to include in the view.</param>
        public void TextureView(uint texture, uint target, uint origtexture, uint internalformat, uint minlevel, uint numlevels, uint minlayer, uint numlayers)
        {
            getDelegateFor<glTextureView>(ref glTextureViewDelegate)(texture, target, origtexture, internalformat, minlevel, numlevels, minlayer, numlayers);
        }

        //  Delegates
        private delegate void glTextureView(uint texture, uint target, uint origtexture, uint internalformat, uint minlevel, uint numlevels, uint minlayer, uint numlayers);
		private Delegate glTextureViewDelegate;

        //  Constants
        public const uint GL_TEXTURE_VIEW_MIN_LEVEL = 0x82DB;
        public const uint GL_TEXTURE_VIEW_NUM_LEVELS = 0x82DC;
        public const uint GL_TEXTURE_VIEW_MIN_LAYER = 0x82DD;
        public const uint GL_TEXTURE_VIEW_NUM_LAYERS = 0x82DE;

        #endregion

        #region GL_ARB_vertex_attrib_binding

        /// <summary>
        /// Bind a buffer to a vertex buffer bind point.
        /// </summary>
        /// <param name="bindingindex">The index of the vertex buffer binding point to which to bind the buffer.</param>
        /// <param name="buffer">The name of an existing buffer to bind to the vertex buffer binding point.</param>
        /// <param name="offset">The offset of the first element of the buffer.</param>
        /// <param name="stride">The distance between elements within the buffer.</param>
        public void BindVertexBuffer(uint bindingindex, uint buffer, IntPtr offset, uint stride)
        {
            getDelegateFor<glBindVertexBuffer>(ref glBindVertexBufferDelegate)(bindingindex, buffer, offset, stride);
        }
        
        /// <summary>
        /// Specify the organization of vertex arrays.
        /// </summary>
        /// <param name="attribindex">The generic vertex attribute array being described.</param>
        /// <param name="size">The number of values per vertex that are stored in the array.</param>
        /// <param name="type">The type of the data stored in the array.</param>
        /// <param name="normalized">GL_TRUE​ if the parameter represents a normalized integer(type​ must be an integer type). GL_FALSE​ otherwise.</param>
        /// <param name="relativeoffset">The offset, measured in basic machine units of the first element relative to the start of the vertex buffer binding this attribute fetches from.</param>
        public void VertexAttribFormat(uint attribindex, int size, uint type, bool normalized, uint relativeoffset)
        {
            getDelegateFor<glVertexAttribFormat>(ref glVertexAttribFormatDelegate)(attribindex, size, type, normalized, relativeoffset);
        }

        /// <summary>
        /// Specify the organization of vertex arrays.
        /// </summary>
        /// <param name="attribindex">The generic vertex attribute array being described.</param>
        /// <param name="size">The number of values per vertex that are stored in the array.</param>
        /// <param name="type">The type of the data stored in the array.</param>
        /// <param name="relativeoffset">The offset, measured in basic machine units of the first element relative to the start of the vertex buffer binding this attribute fetches from.</param>
        public void VertexAttribIFormat(uint attribindex, int size, uint type, uint relativeoffset)
        {
            getDelegateFor<glVertexAttribIFormat>(ref glVertexAttribIFormatDelegate)(attribindex, size, type, relativeoffset);
        }

        /// <summary>
        /// Specify the organization of vertex arrays.
        /// </summary>
        /// <param name="attribindex">The generic vertex attribute array being described.</param>
        /// <param name="size">The number of values per vertex that are stored in the array.</param>
        /// <param name="type">The type of the data stored in the array.</param>
        /// <param name="relativeoffset">The offset, measured in basic machine units of the first element relative to the start of the vertex buffer binding this attribute fetches from.</param>
        public void VertexAttribLFormat(uint attribindex, int size, uint type, uint relativeoffset)
        {
            getDelegateFor<glVertexAttribLFormat>(ref glVertexAttribLFormatDelegate)(attribindex, size, type, relativeoffset);
        }
        
        /// <summary>
        /// Associate a vertex attribute and a vertex buffer binding.
        /// </summary>
        /// <param name="attribindex">The index of the attribute to associate with a vertex buffer binding.</param>
        /// <param name="bindingindex">The index of the vertex buffer binding with which to associate the generic vertex attribute.</param>
        public void VertexAttribBinding(uint attribindex, uint bindingindex)
        {
            getDelegateFor<glVertexAttribBinding>(ref glVertexAttribBindingDelegate)(attribindex, bindingindex);
        }
        
        /// <summary>
        /// Modify the rate at which generic vertex attributes advance.
        /// </summary>
        /// <param name="bindingindex">The index of the binding whose divisor to modify.</param>
        /// <param name="divisor">The new value for the instance step rate to apply.</param>
        public void VertexBindingDivisor(uint bindingindex, uint divisor)
        {
            getDelegateFor<glVertexBindingDivisor>(ref glVertexBindingDivisorDelegate)(bindingindex, divisor);
        }

        /// <summary>
        /// Bind a buffer to a vertex buffer bind point.
        /// Available only when When EXT_direct_state_access is present.
        /// </summary>
        /// <param name="vaobj">The vertex array object.</param>
        /// <param name="bindingindex">The index of the vertex buffer binding point to which to bind the buffer.</param>
        /// <param name="buffer">The name of an existing buffer to bind to the vertex buffer binding point.</param>
        /// <param name="offset">The offset of the first element of the buffer.</param>
        /// <param name="stride">The distance between elements within the buffer.</param>
        public void VertexArrayBindVertexBufferEXT(uint vaobj, uint bindingindex, uint buffer, IntPtr offset, uint stride)
        {
            getDelegateFor<glVertexArrayBindVertexBufferEXT>(ref glVertexArrayBindVertexBufferEXTDelegate)(vaobj, bindingindex, buffer, offset, stride);
        }

        /// <summary>
        /// Specify the organization of vertex arrays.
        /// Available only when When EXT_direct_state_access is present.
        /// </summary>
        /// <param name="vaobj">The vertex array object.</param>
        /// <param name="attribindex">The generic vertex attribute array being described.</param>
        /// <param name="size">The number of values per vertex that are stored in the array.</param>
        /// <param name="type">The type of the data stored in the array.</param>
        /// <param name="normalized">GL_TRUE​ if the parameter represents a normalized integer(type​ must be an integer type). GL_FALSE​ otherwise.</param>
        /// <param name="relativeoffset">The offset, measured in basic machine units of the first element relative to the start of the vertex buffer binding this attribute fetches from.</param>
        public void VertexArrayVertexAttribFormatEXT(uint vaobj, uint attribindex, int size, uint type, bool normalized, uint relativeoffset)
        {
            getDelegateFor<glVertexArrayVertexAttribFormatEXT>(ref glVertexArrayVertexAttribFormatEXTDelegate)(vaobj, attribindex, size, type, normalized, relativeoffset);
        }

        /// <summary>
        /// Specify the organization of vertex arrays.
        /// Available only when When EXT_direct_state_access is present.
        /// </summary>
        /// <param name="vaobj">The vertex array object.</param>
        /// <param name="attribindex">The generic vertex attribute array being described.</param>
        /// <param name="size">The number of values per vertex that are stored in the array.</param>
        /// <param name="type">The type of the data stored in the array.</param>
        /// <param name="relativeoffset">The offset, measured in basic machine units of the first element relative to the start of the vertex buffer binding this attribute fetches from.</param>
        public void VertexArrayVertexAttribIFormatEXT(uint vaobj, uint attribindex, int size, uint type, uint relativeoffset)
        {
            getDelegateFor<glVertexArrayVertexAttribIFormatEXT>(ref glVertexArrayVertexAttribIFormatEXTDelegate)(vaobj, attribindex, size, type, relativeoffset);
        }

        /// <summary>
        /// Specify the organization of vertex arrays.
        /// Available only when When EXT_direct_state_access is present.
        /// </summary>
        /// <param name="vaobj">The vertex array object.</param>
        /// <param name="attribindex">The generic vertex attribute array being described.</param>
        /// <param name="size">The number of values per vertex that are stored in the array.</param>
        /// <param name="type">The type of the data stored in the array.</param>
        /// <param name="relativeoffset">The offset, measured in basic machine units of the first element relative to the start of the vertex buffer binding this attribute fetches from.</param>
        public void VertexArrayVertexAttribLFormatEXT(uint vaobj, uint attribindex, int size, uint type, uint relativeoffset)
        {
            getDelegateFor<glVertexArrayVertexAttribLFormatEXT>(ref glVertexArrayVertexAttribLFormatEXTDelegate)(vaobj, attribindex, size, type, relativeoffset);
        }

        /// <summary>
        /// Associate a vertex attribute and a vertex buffer binding.
        /// Available only when When EXT_direct_state_access is present.
        /// </summary>
        /// <param name="vaobj">The vertex array object.</param>
        /// <param name="attribindex">The index of the attribute to associate with a vertex buffer binding.</param>
        /// <param name="bindingindex">The index of the vertex buffer binding with which to associate the generic vertex attribute.</param>
        public void VertexArrayVertexAttribBindingEXT(uint vaobj, uint attribindex, uint bindingindex)
        {
            getDelegateFor<glVertexArrayVertexAttribBindingEXT>(ref glVertexArrayVertexAttribBindingEXTDelegate)(vaobj, attribindex, bindingindex);
        }

        /// <summary>
        /// Modify the rate at which generic vertex attributes advance.
        /// Available only when When EXT_direct_state_access is present.
        /// </summary>
        /// <param name="vaobj">The vertex array object.</param>
        /// <param name="bindingindex">The index of the binding whose divisor to modify.</param>
        /// <param name="divisor">The new value for the instance step rate to apply.</param>
        public void VertexArrayVertexBindingDivisorEXT(uint vaobj, uint bindingindex, uint divisor)
        {
            getDelegateFor<glVertexArrayVertexBindingDivisorEXT>(ref glVertexArrayVertexBindingDivisorEXTDelegate)(vaobj, bindingindex, divisor);
        }

        //  Delegates
        private delegate void glBindVertexBuffer(uint bindingindex, uint buffer, IntPtr offset, uint stride);
		private Delegate glBindVertexBufferDelegate;
        private delegate void glVertexAttribFormat(uint attribindex, int size, uint type, bool normalized, uint relativeoffset);
		private Delegate glVertexAttribFormatDelegate;
        private delegate void glVertexAttribIFormat(uint attribindex, int size, uint type, uint relativeoffset);
		private Delegate glVertexAttribIFormatDelegate;
        private delegate void glVertexAttribLFormat(uint attribindex, int size, uint type, uint relativeoffset);
		private Delegate glVertexAttribLFormatDelegate;
        private delegate void glVertexAttribBinding(uint attribindex, uint bindingindex);
		private Delegate glVertexAttribBindingDelegate;
        private delegate void glVertexBindingDivisor(uint bindingindex, uint divisor);
		private Delegate glVertexBindingDivisorDelegate;
        private delegate void glVertexArrayBindVertexBufferEXT(uint vaobj, uint bindingindex, uint buffer, IntPtr offset, uint stride);
		private Delegate glVertexArrayBindVertexBufferEXTDelegate;
        private delegate void glVertexArrayVertexAttribFormatEXT(uint vaobj, uint attribindex, int size, uint type, bool normalized, uint relativeoffset);
		private Delegate glVertexArrayVertexAttribFormatEXTDelegate;
        private delegate void glVertexArrayVertexAttribIFormatEXT(uint vaobj, uint attribindex, int size, uint type, uint relativeoffset);
		private Delegate glVertexArrayVertexAttribIFormatEXTDelegate;
        private delegate void glVertexArrayVertexAttribLFormatEXT(uint vaobj, uint attribindex, int size, uint type, uint relativeoffset);
		private Delegate glVertexArrayVertexAttribLFormatEXTDelegate;
        private delegate void glVertexArrayVertexAttribBindingEXT(uint vaobj, uint attribindex, uint bindingindex);
		private Delegate glVertexArrayVertexAttribBindingEXTDelegate;
        private delegate void glVertexArrayVertexBindingDivisorEXT(uint vaobj, uint bindingindex, uint divisor);
		private Delegate glVertexArrayVertexBindingDivisorEXTDelegate;

        //  Constants
        public const uint GL_VERTEX_ATTRIB_BINDING                          = 0x82D4;  
        public const uint GL_VERTEX_ATTRIB_RELATIVE_OFFSET                  = 0x82D5;  
        public const uint GL_VERTEX_BINDING_DIVISOR                         = 0x82D6;  
        public const uint GL_VERTEX_BINDING_OFFSET                          = 0x82D7;  
        public const uint GL_VERTEX_BINDING_STRIDE                          = 0x82D8;  
        public const uint GL_VERTEX_BINDING_BUFFER                          = 0x8F4F;
        public const uint GL_MAX_VERTEX_ATTRIB_RELATIVE_OFFSET              = 0x82D9;  
        public const uint GL_MAX_VERTEX_ATTRIB_BINDINGS                     = 0x82DA;  

        #endregion

        #region WGL_ARB_extensions_string

        /// <summary>
        /// Gets the ARB extensions string.
        /// </summary>
        public string GetExtensionsStringARB(IntPtr hdc)
        {
            return getDelegateFor<wglGetExtensionsStringARB>(ref wglGetExtensionsStringARBDelegate)(hdc);
        }

        //  Delegates
        private delegate string wglGetExtensionsStringARB(IntPtr hdc);
        private Delegate wglGetExtensionsStringARBDelegate;

        #endregion

        #region WGL_ARB_create_context

        //  Methods

        /// <summary>
        /// Creates a render context with the specified attributes.
        /// </summary>
        /// <param name="hdc">The device context handle</param>
        /// <param name="hShareContext">
        /// If is not null, then all shareable data (excluding
        /// OpenGL texture objects named 0) will be shared by hShareContext,
        /// all other contexts hShareContext already shares with, and the
        /// newly created context. An arbitrary number of contexts can share
        /// data in this fashion.</param>
        /// <param name="attribList">
        /// specifies a list of attributes for the context. The
        /// list consists of a sequence of (name,value) pairs terminated by the
        /// value 0. If an attribute is not specified in attribList, then the
        /// default value specified below is used instead. If an attribute is
        /// specified more than once, then the last value specified is used.
        /// </param>
        public IntPtr CreateContextAttribsARB(IntPtr hdc, IntPtr hShareContext, int[] attribList)
        {
            return getDelegateFor<wglCreateContextAttribsARB>(ref wglCreateContextAttribsARBDelegate)(hdc, hShareContext, attribList);
        }

        //  Delegates
        private delegate IntPtr wglCreateContextAttribsARB(IntPtr hDC, IntPtr hShareContext, int[] attribList);
        private Delegate wglCreateContextAttribsARBDelegate;

        //  Constants
        public const int WGL_CONTEXT_MAJOR_VERSION_ARB = 0x2091;
        public const int WGL_CONTEXT_MINOR_VERSION_ARB = 0x2092;
        public const int WGL_CONTEXT_LAYER_PLANE_ARB = 0x2093;
        public const int WGL_CONTEXT_FLAGS_ARB = 0x2094;
        public const int WGL_CONTEXT_PROFILE_MASK_ARB = 0x9126;
        public const int WGL_CONTEXT_DEBUG_BIT_ARB = 0x0001;
        public const int WGL_CONTEXT_FORWARD_COMPATIBLE_BIT_ARB = 0x0002;
        public const int WGL_CONTEXT_CORE_PROFILE_BIT_ARB = 0x00000001;
        public const int WGL_CONTEXT_COMPATIBILITY_PROFILE_BIT_ARB = 0x00000002;
        public const int ERROR_INVALID_VERSION_ARB = 0x2095;
        public const int ERROR_INVALID_PROFILE_ARB = 0x2096;

        #endregion


        #region WGL_NV_DX_interop / WGL_NV_DX_interop2

        public bool DXSetResourceShareHandleNV(IntPtr dxObject, IntPtr shareHandle)
        {
            return getDelegateFor<wglDXSetResourceShareHandleNV>(ref wglDXSetResourceShareHandleNVDelegate)(dxObject, shareHandle);
        }

        public IntPtr DXOpenDeviceNV(IntPtr dxDevice)
        {
            return getDelegateFor<wglDXOpenDeviceNV>(ref wglDXOpenDeviceNVDelegate)(dxDevice);
        }

        public bool DXCloseDeviceNV(IntPtr hDevice)
        {
            return getDelegateFor<wglDXCloseDeviceNV>(ref wglDXCloseDeviceNVDelegate)(hDevice);
        }

        public IntPtr DXRegisterObjectNV(IntPtr hDevice, IntPtr dxObject, uint name, uint type, uint access)
        {
            return getDelegateFor<wglDXRegisterObjectNV>(ref wglDXRegisterObjectNVDelegate)(hDevice, dxObject, name, type, access);
        }

        public bool DXUnregisterObjectNV(IntPtr hDevice, IntPtr hObject)
        {
            return getDelegateFor<wglDXUnregisterObjectNV>(ref wglDXUnregisterObjectNVDelegate)(hDevice, hObject);
        }

        public bool DXObjectAccessNV(IntPtr hObject, uint access)
        {
            return getDelegateFor<wglDXObjectAccessNV>(ref wglDXObjectAccessNVDelegate)(hObject, access);
        }

        public bool DXLockObjectsNV(IntPtr hDevice, IntPtr[] hObjects)
        {
            unsafe
            {
                void** objects = stackalloc void*[hObjects.Length];

                for (int i = 0; i < hObjects.Length; i++)
                {
                    objects[i] = hObjects[i].ToPointer();
                }

                return getDelegateFor<wglDXLockObjectsNV>(ref wglDXLockObjectsNVDelegate)(hDevice, hObjects.Length, objects);
            }
        }

        public bool DXUnlockObjectsNV(IntPtr hDevice, IntPtr[] hObjects)
        {
            unsafe
            {
                void** objects = stackalloc void*[hObjects.Length];

                for (int i = 0; i < hObjects.Length; i++)
                {
                    objects[i] = hObjects[i].ToPointer();
                }

                return getDelegateFor<wglDXUnlockObjectsNV>(ref wglDXUnlockObjectsNVDelegate)(hDevice, hObjects.Length, objects);
            }
        }

        private delegate bool wglDXSetResourceShareHandleNV(IntPtr dxObject, IntPtr shareHandle);
        private Delegate wglDXSetResourceShareHandleNVDelegate;
        private delegate IntPtr wglDXOpenDeviceNV(IntPtr dxDevice);
        private Delegate wglDXOpenDeviceNVDelegate;
        private delegate bool wglDXCloseDeviceNV(IntPtr hDevice);
        private Delegate wglDXCloseDeviceNVDelegate;
        private delegate IntPtr wglDXRegisterObjectNV(IntPtr hDevice, IntPtr dxObject, uint name, uint type, uint access);
        private Delegate wglDXRegisterObjectNVDelegate;
        private delegate bool wglDXUnregisterObjectNV(IntPtr hDevice, IntPtr hObject);
        private Delegate wglDXUnregisterObjectNVDelegate;
        private delegate bool wglDXObjectAccessNV(IntPtr hObject, uint access);
        private Delegate wglDXObjectAccessNVDelegate;
        private unsafe delegate bool wglDXLockObjectsNV(IntPtr hDevice, int count, void** hObjects);
        private Delegate wglDXLockObjectsNVDelegate;
        private unsafe delegate bool wglDXUnlockObjectsNV(IntPtr hDevice, int count, void** hObjects);
        private Delegate wglDXUnlockObjectsNVDelegate;

        public const uint WGL_ACCESS_READ_ONLY_NV = 0x0000;
        public const uint WGL_ACCESS_READ_WRITE_NV = 0x0001;
        public const uint WGL_ACCESS_WRITE_DISCARD_NV = 0x0002;

        #endregion

        #region WGL_ARB_pixel_format

        public bool GetPixelFormatAttribivARB(IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, int[] piAttributes, int[] piValues)
        {
            return getDelegateFor<wglGetPixelFormatAttribivARB>(ref wglGetPixelFormatAttribivARBDelegate)(hdc, iPixelFormat, iLayerPlane, nAttributes, piAttributes, piValues);
        }

        public bool GetPixelFormatAttribfvARB(IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, int[] piAttributes, float[] pfValues)
        {
            return getDelegateFor<wglGetPixelFormatAttribfvARB>(ref wglGetPixelFormatAttribfvARBDelegate)(hdc, iPixelFormat, iLayerPlane, nAttributes, piAttributes, pfValues);
        }

        public bool ChoosePixelFormatARB(IntPtr hdc, int[] piAttribIList, float[] pfAttribFList, uint nMaxFormats, int[] piFormats, uint[] nNumFormats)
        {
            return getDelegateFor<wglChoosePixelFormatARB>(ref wglChoosePixelFormatARBDelegate)(hdc, piAttribIList, pfAttribFList, nMaxFormats, piFormats, nNumFormats);
        }

        private delegate bool wglGetPixelFormatAttribivARB(IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, int[] piAttributes, int[] piValues);
        private Delegate wglGetPixelFormatAttribivARBDelegate;

        private delegate bool wglGetPixelFormatAttribfvARB(IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, int[] piAttributes, float[] pfValues);
        private Delegate wglGetPixelFormatAttribfvARBDelegate;

        private delegate bool wglChoosePixelFormatARB(IntPtr hdc, int[] piAttribIList, float[] pfAttribFList, uint nMaxFormats, int[] piFormats, uint[] nNumFormats);
        private Delegate wglChoosePixelFormatARBDelegate;

        public const uint WGL_NUMBER_PIXEL_FORMATS_ARB = 0x2000;
        public const uint WGL_DRAW_TO_WINDOW_ARB = 0x2001;
        public const uint WGL_DRAW_TO_BITMAP_ARB = 0x2002;
        public const uint WGL_ACCELERATION_ARB = 0x2003;
        public const uint WGL_NEED_PALETTE_ARB = 0x2004;
        public const uint WGL_NEED_SYSTEM_PALETTE_ARB = 0x2005;
        public const uint WGL_SWAP_LAYER_BUFFERS_ARB = 0x2006;
        public const uint WGL_SWAP_METHOD_ARB = 0x2007;
        public const uint WGL_NUMBER_OVERLAYS_ARB = 0x2008;
        public const uint WGL_NUMBER_UNDERLAYS_ARB = 0x2009;
        public const uint WGL_TRANSPARENT_ARB = 0x200A;
        public const uint WGL_TRANSPARENT_RED_VALUE_ARB = 0x2037;
        public const uint WGL_TRANSPARENT_GREEN_VALUE_ARB = 0x2038;
        public const uint WGL_TRANSPARENT_BLUE_VALUE_ARB = 0x2039;
        public const uint WGL_TRANSPARENT_ALPHA_VALUE_ARB = 0x203A;
        public const uint WGL_TRANSPARENT_INDEX_VALUE_ARB = 0x203B;
        public const uint WGL_SHARE_DEPTH_ARB = 0x200C;
        public const uint WGL_SHARE_STENCIL_ARB = 0x200D;
        public const uint WGL_SHARE_ACCUM_ARB = 0x200E;
        public const uint WGL_SUPPORT_GDI_ARB = 0x200F;
        public const uint WGL_SUPPORT_OPENGL_ARB = 0x2010;
        public const uint WGL_DOUBLE_BUFFER_ARB = 0x2011;
        public const uint WGL_STEREO_ARB = 0x2012;
        public const uint WGL_PIXEL_TYPE_ARB = 0x2013;
        public const uint WGL_COLOR_BITS_ARB = 0x2014;
        public const uint WGL_RED_BITS_ARB = 0x2015;
        public const uint WGL_RED_SHIFT_ARB = 0x2016;
        public const uint WGL_GREEN_BITS_ARB = 0x2017;
        public const uint WGL_GREEN_SHIFT_ARB = 0x2018;
        public const uint WGL_BLUE_BITS_ARB = 0x2019;
        public const uint WGL_BLUE_SHIFT_ARB = 0x201A;
        public const uint WGL_ALPHA_BITS_ARB = 0x201B;
        public const uint WGL_ALPHA_SHIFT_ARB = 0x201C;
        public const uint WGL_ACCUM_BITS_ARB = 0x201D;
        public const uint WGL_ACCUM_RED_BITS_ARB = 0x201E;
        public const uint WGL_ACCUM_GREEN_BITS_ARB = 0x201F;
        public const uint WGL_ACCUM_BLUE_BITS_ARB = 0x2020;
        public const uint WGL_ACCUM_ALPHA_BITS_ARB = 0x2021;
        public const uint WGL_DEPTH_BITS_ARB = 0x2022;
        public const uint WGL_STENCIL_BITS_ARB = 0x2023;
        public const uint WGL_AUX_BUFFERS_ARB = 0x2024;

        public const uint WGL_NO_ACCELERATION_ARB = 0x2025;
        public const uint WGL_GENERIC_ACCELERATION_ARB = 0x2026;
        public const uint WGL_FULL_ACCELERATION_ARB = 0x2027;

        public const uint WGL_SWAP_EXCHANGE_ARB = 0x2028;
        public const uint WGL_SWAP_COPY_ARB = 0x2029;
        public const uint WGL_SWAP_UNDEFINED_ARB = 0x202A;

        public const uint WGL_TYPE_RGBA_ARB = 0x202B;
        public const uint WGL_TYPE_COLORINDEX_ARB = 0x202C;

        #endregion

        #region WGL_ARB_multisample

        public const uint WGL_SAMPLE_BUFFERS_ARB = 0x2041;
        public const uint WGL_SAMPLES_ARB = 0x2042;

        #endregion

        #region WGL_EXT_swap_control

        public int SwapIntervalEXT(int interval)
        {
            return getDelegateFor<wglSwapIntervalEXT>(ref wglSwapIntervalEXTDelegate)(interval);
        }

        public int GetSwapIntervalEXT()
        {
            return getDelegateFor<wglGetSwapIntervalEXT>(ref wglGetSwapIntervalEXTDelegate)();
        }

        private delegate int wglSwapIntervalEXT(int interval);
        private Delegate wglSwapIntervalEXTDelegate;

        private delegate int wglGetSwapIntervalEXT();
        private Delegate wglGetSwapIntervalEXTDelegate;

        #endregion
    }
}
